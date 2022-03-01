using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{


    private float leftRight = 0f;
    private float backForward = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftRight = Input.GetAxisRaw("Horizontal");
        backForward = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Jump"))
        {
            transform.Translate(transform.right * Time.deltaTime * leftRight * 60);
            transform.Translate(transform.forward * Time.deltaTime * backForward * 60);
        }
        else
        {
            transform.Translate(transform.right * Time.deltaTime * leftRight * 30);
            transform.Translate(transform.forward * Time.deltaTime * backForward * 30);
        }
        

        
    }
}
