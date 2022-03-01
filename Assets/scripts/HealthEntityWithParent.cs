using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEntityWithParent : MonoBehaviour
{

    public float hitpoints = 200;
    private bool destroy = false;
    public GameObject parent;
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
           
            Destroy(parent);
        }
    }
    

    public void TakeDamage(float damage)
    {
        hitpoints -= damage;

    }
}
