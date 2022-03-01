using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    [SerializeField] GameObject BluePlayer;
    [SerializeField] GameObject RedPlayer;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount % 2 == 1)
        {
            BluePlayer.SetActive(true);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount % 2 == 0)
        {
            RedPlayer.SetActive(true);
            gameManager.ToggleIncomeGain();
        }





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
