using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform enemy;
    public Transform muzzleTransform;
    public GameObject bullet;
    public float firespeed = 2f;
    private float cooldown = 2f;

    public float missRadius = 2f;


    public bool enemyinRange = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;

        if(cooldown > firespeed && enemyinRange)
        {
            FireBullet();
            cooldown = 0;
        }
        
    }


    private void FireBullet()
    {
        
        Vector3 missOffset = new Vector3(Random.Range(-missRadius, missRadius), Random.Range(-missRadius, missRadius),
            Random.Range(-missRadius, missRadius)) * 0.1f * (transform.position - enemy.position).magnitude;
       
        Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
        GameObject tempBullet = Instantiate(bullet, muzzleTransform.position, offsetRot);
        tempBullet.GetComponent<SimpleBullet>().hitpoint = enemy.position + missOffset;
    }
}
