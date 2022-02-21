using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{


    private float leftRight = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftRight = Input.GetAxisRaw("Horizontal");
        //transform.Translate(transform.right);
        
    }
}
