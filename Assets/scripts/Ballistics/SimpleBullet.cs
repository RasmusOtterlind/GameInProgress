using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        lifeTimer -= Time.deltaTime;
        if(lifeTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject tempGameObject = collision.gameObject;
        if(collision.gameObject.GetComponent<HealthEntity>() != null)
        {
            collision.gameObject.GetComponent<HealthEntity>().TakeDamage(damage);
        }
        
        //Destroy(gameObject);
    }
}
