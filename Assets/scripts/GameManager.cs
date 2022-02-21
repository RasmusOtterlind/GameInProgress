using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject basicSoldier;
    public Transform blueBase;
    public Transform redBase;

    public Material blueMaterial;
    public Material redMaterial;

    public LayerMask blueTeam;
    public LayerMask blueTeamCover;
    public LayerMask redTeam;
    public LayerMask redTeamCover;

    public float blueMoney = 0f;
    public float redMoney = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSoldierBlue()
    {
        Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
        Vector3 randomSpawnOffset = new Vector3(Random.Range(0, 5), -3, Random.Range(-19, 19));
        GameObject tempobjekt = Instantiate(basicSoldier, blueBase.position + randomSpawnOffset, offsetRot);
        fixBlueObject(tempobjekt);
    }
    private void fixBlueObject(GameObject bluePawn)
    {
        bluePawn.layer = 8;
        bluePawn.GetComponent<AiNavMesh>().enemyBaseTransform = redBase;
        bluePawn.GetComponent<AiNavMesh>().enemyLayers = redTeam;
        bluePawn.GetComponent<AiNavMesh>().friendlyCover = blueTeamCover;
        bluePawn.GetComponent<MeshRenderer>().material = blueMaterial;
    }

    public void SpawnSoldierRed()
    {
        Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
        Vector3 randomSpawnOffset = new Vector3(Random.Range(0, 5), -3, Random.Range(-19, 19));
        GameObject tempobjekt = Instantiate(basicSoldier, redBase.position + randomSpawnOffset, offsetRot);
        fixRedObject(tempobjekt);
    }
    private void fixRedObject(GameObject pawn)
    {
        pawn.layer = 9;
        pawn.GetComponent<AiNavMesh>().enemyBaseTransform = blueBase;
        pawn.GetComponent<AiNavMesh>().enemyLayers = blueTeam;
        pawn.GetComponent<AiNavMesh>().friendlyCover = redTeamCover;
        pawn.GetComponent<MeshRenderer>().material = redMaterial;
    }
}
