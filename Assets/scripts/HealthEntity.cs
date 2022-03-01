using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        if (GetComponent<PhotonView>().IsMine)
        {
            if (hitpoints <= 0)
            {
                destroy = true;
            }
        }

        if (destroy && GetComponent<PhotonView>().IsMine)
        {

            PhotonNetwork.Destroy(gameObject);
        }

    }

    private void LateUpdate()
    {
       
    }
   
    public void TakeDamage(float damage)
    {

        GetComponent<PhotonView>().RPC("LowerHealth", RpcTarget.All, damage);
        

    }

    [PunRPC]
    private void LowerHealth(float damage)
    {
        hitpoints -= damage;
    }
}
