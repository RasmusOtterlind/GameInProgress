using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBullet : MonoBehaviour
{
    public float power = 400f;
    public Vector3 hitpoint;
    public float lifeTimer = 3f;
   
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce((hitpoint - transform.position).normalized * power);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
