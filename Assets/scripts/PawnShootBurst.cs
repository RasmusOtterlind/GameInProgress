using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PawnShootBurst : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform enemy;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject dummyBullet;
    [SerializeField] private float firespeed = 2f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private int burstCount = 3;
    [SerializeField] private float cooldownBetweenShots = 0.15f;
    private float burstCooldown = 0f;
    private int shotCount = 0;
    [SerializeField] private AudioClip audioClip;


    private float cooldown = 2f;

    public float missRadius = 2f;
    public int shotByLayer = 0;


    public bool enemyinRange = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            cooldown += Time.fixedDeltaTime;
            burstCooldown += Time.fixedDeltaTime;
            if (cooldown > firespeed && enemyinRange && burstCooldown >= cooldownBetweenShots)
            {
                burstCooldown = 0;
                shotCount++;
                FireBullet();
                if (shotCount >= burstCount)
                {
                    cooldown = 0;
                    shotCount = 0;
                }

            }
        }


    }


    private void FireBullet()
    {
        
        Vector3 missOffset = new Vector3(Random.Range(-missRadius, missRadius), Random.Range(-missRadius, missRadius),
        Random.Range(-missRadius, missRadius)) * 0.1f * (transform.position - enemy.position).magnitude;
       
        Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
        GameObject tempBullet = Instantiate(bullet, muzzleTransform.position, Quaternion.identity);
        

        if (tempBullet.GetComponent<SimpleBullet>() != null)
        {
            tempBullet.GetComponent<SimpleBullet>().hitpoint = enemy.position + missOffset;
        }
        else if (tempBullet.GetComponent<ExplodingBullet>())
        {
            tempBullet.GetComponent<ExplodingBullet>().hitpoint = enemy.position + missOffset;
        }
        Vector3 hitpoint = enemy.position + missOffset;
        GetComponent<PhotonView>().RPC("FireDummyBullet", RpcTarget.Others, hitpoint.x, hitpoint.y, hitpoint.z);
        audioSource.PlayOneShot(audioClip);


        tempBullet.layer = shotByLayer;
    }
    
    [PunRPC]
    void FireDummyBullet(float x , float y, float z)
    {
        GameObject temp = Instantiate(dummyBullet, muzzleTransform.position,Quaternion.identity);

        temp.GetComponent<DummyBullet>().hitpoint = new Vector3(x, y, z);
    }
    
}
