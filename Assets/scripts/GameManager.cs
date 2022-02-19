using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject basicSoldier;
    public Transform blueBase;
    public Transform redBase;

    public LayerMask blueTeam;
    public LayerMask blueTeamCover;
    public LayerMask redTeam;
    public LayerMask redTeamCover;

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
}
}
