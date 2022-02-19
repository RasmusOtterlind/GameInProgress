using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEntity : MonoBehaviour
{

    public float hitpoints;
    private bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hitpoints <= 0)
        {
            destroy = true;
        }
    }

    private void LateUpdate()
    {
        if (destroy)
        {
           
            Destroy(gameObject);
        }
    }
    

    public void TakeDamage(float damage)
    {
        hitpoints -= damage;

    }
}
