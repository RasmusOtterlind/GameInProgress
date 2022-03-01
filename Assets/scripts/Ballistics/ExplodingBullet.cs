using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{

    public float power = 100;
    public Vector3 hitpoint;
    public float lifeTimer = 3f;
    public float explosionDamage = 100f;
    public float explosionRadius = 3f;
    public LayerMask layerMask;
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
        Collider[] overlappingEnemies = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);
        GameObject tempGameObject = collision.gameObject;
        foreach(Collider collider in overlappingEnemies)
        {
            if(collider.gameObject.GetComponent<HealthEntity>() != null)
            {
                collider.gameObject.GetComponent<HealthEntity>().TakeDamage(explosionDamage);
            }
            else if(collider.gameObject.GetComponent<HealthEntityWithParent>() != null)
            {
                collider.gameObject.GetComponent<HealthEntityWithParent>().TakeDamage(explosionDamage);
            }
        }
       
        
        Destroy(gameObject);
    }
}
