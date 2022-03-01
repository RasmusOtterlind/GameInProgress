using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    // Basic Soldiers
    [SerializeField] private GameObject basicBlueSoldier; 
    [SerializeField] private GameObject basicRedSoldier;
    [SerializeField] private GameObject sniperBlue;
    [SerializeField] private GameObject sniperRed;



    public Transform blueBase;
    public Transform redBase;

    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material redMaterial;

    public LayerMask blueTeam;
    public LayerMask blueTeamCover;
    public LayerMask redTeam;
    public LayerMask redTeamCover;

    public float blueMoney = 30f;
    public float redMoney = 30f;

    [SerializeField] private float basicSoldierCost = 15f;
    [SerializeField] private float sniperCost = 40f;

    [SerializeField] private GameObject redMoneyTag;
    [SerializeField] private GameObject blueMoneyTag;

    
    private PhotonView photonView;

    private bool incomeGain = false;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (incomeGain)
        {
            Income();
        }
        
    }

    public void SpawnSoldierBlue()
    {
        if (blueMoney > basicSoldierCost)
        {
            Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
            Vector3 randomSpawnOffset = new Vector3(Random.Range(0, 5), -3, Random.Range(-19, 19));
            GameObject tempobjekt = PhotonNetwork.Instantiate(basicBlueSoldier.name, blueBase.position + randomSpawnOffset, offsetRot);
            photonView.RPC("ChangeBlueMoney", RpcTarget.All, -basicSoldierCost);
        }
       
    }
    public void SpawnSniperBlue()
    {
        if (blueMoney > sniperCost)
        {
            Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
            Vector3 randomSpawnOffset = new Vector3(Random.Range(0, 5), -3, Random.Range(-19, 19));
            GameObject tempobjekt = PhotonNetwork.Instantiate(sniperBlue.name, blueBase.position + randomSpawnOffset, offsetRot);
            photonView.RPC("ChangeBlueMoney", RpcTarget.All, -sniperCost);
        }

    }

    public void SpawnSoldierRed()
    {
        if (redMoney > basicSoldierCost)
        {
            Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
            Vector3 randomSpawnOffset = new Vector3(Random.Range(0, 5), -3, Random.Range(-19, 19));
            GameObject tempobjekt = PhotonNetwork.Instantiate(basicRedSoldier.name, redBase.position + randomSpawnOffset, offsetRot);
            photonView.RPC("ChangeRedMoney", RpcTarget.All, -basicSoldierCost);
        }

    }

    public void SpawnSniperRed()
    {
        if(redMoney > sniperCost)
        {
            Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
            Vector3 randomSpawnOffset = new Vector3(Random.Range(0, 5), -3, Random.Range(-19, 19));
            GameObject tempobjekt = PhotonNetwork.Instantiate(sniperRed.name, redBase.position + randomSpawnOffset, offsetRot);
            photonView.RPC("ChangeRedMoney", RpcTarget.All, -sniperCost);
        }
        
    }

    private void Income()
    {
        redMoney += Time.deltaTime;
        blueMoney += Time.deltaTime;

        redMoneyTag.GetComponent<TextMeshProUGUI>().SetText("Red Money: " + Mathf.RoundToInt(redMoney));
        blueMoneyTag.GetComponent<TextMeshProUGUI>().SetText("Blue Money: " + Mathf.RoundToInt(blueMoney));
    }

    [PunRPC]
    private void ChangeBlueMoney(float change)
    {
        blueMoney += change;
    }

    [PunRPC]
    private void ChangeRedMoney(float change)
    {
        redMoney += change;
    }

    public void ToggleIncomeGain()
    {
        photonView.RPC("ToogleIncome", RpcTarget.AllBuffered);
    }
    
    [PunRPC] 
    private void ToogleIncome()
    {
        incomeGain = !incomeGain;
    }
}
