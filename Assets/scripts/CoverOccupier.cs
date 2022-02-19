using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverOccupier : MonoBehaviour
{
    // Start is called before the first frame update
    public int layer;
    public bool occupied = false;
    public bool coverDestroyed = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == layer)
        {
            occupied = true;
            other.gameObject.GetComponent<AiNavMesh>().inCover = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layer)
        {
            occupied = false;
            other.gameObject.GetComponent<AiNavMesh>().inCover = false;
        }
    }

}
