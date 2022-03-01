using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SimpleBullet : MonoBehaviour
{

    public float power = 100;
    public Vector3 hitpoint;
    public float lifeTimer = 3f;
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce((hitpoint - transform.position).normalized * power);
    }

    // Update is called once per frame
    void Update()
    {
        //if (GetComponent<PhotonView>().IsMine)
        //{
            lifeTimer -= Time.deltaTime;
            if (lifeTimer < 0)
            {
              Destroy(gameObject);
            }
        //}
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (GetComponent<PhotonView>().IsMine)
        //{
            GameObject tempGameObject = collision.gameObject;
            if (tempGameObject.GetComponent<HealthEntity>() != null)
            {
                tempGameObject.GetComponent<HealthEntity>().TakeDamage(damage);
            }
            else if (collision.gameObject.GetComponent<HealthEntityWithParent>() != null)
            {
                collision.gameObject.GetComponent<HealthEntityWithParent>().TakeDamage(damage);
            }
           Destroy(gameObject);
        //}
        

        

    }
}
