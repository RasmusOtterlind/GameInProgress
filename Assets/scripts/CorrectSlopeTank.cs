using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectSlopeTank : MonoBehaviour
{
    public float slopeRotationSpeed;
    public Vector3 originOffset;
    public float maxRayDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the object's position
        Transform objTrans = transform;
        Vector3 origin = objTrans.position;

        //Only register raycast consided as Hill(Can be any layer name)
        int hillLayerIndex = LayerMask.NameToLayer("Default");
        //Calculate layermask to Raycast to. 
        int layerMask = (1 << hillLayerIndex);


        RaycastHit slopeHit;

        //Perform raycast from the object's position downwards
        if (Physics.Raycast(origin + originOffset + transform.forward*3, Vector3.down, out slopeHit, maxRayDist, layerMask))
        {
            //Drawline to show the hit point
            Debug.DrawLine(origin + originOffset + transform.forward * 3, slopeHit.point, Color.red);

            //Get slope angle from the raycast hit normal then calcuate new pos of the object
            Quaternion newRot = Quaternion.FromToRotation(objTrans.up, slopeHit.normal)
                * objTrans.rotation;

            //Apply the rotation 
            objTrans.rotation = Quaternion.Lerp(objTrans.rotation, newRot,
                Time.deltaTime * slopeRotationSpeed);

        }

    }
}
