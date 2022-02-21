using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiNavMesh : MonoBehaviour
{

    public Transform enemyBaseTransform;


    private NavMeshAgent navMeshAgent;

    private bool enemyInRange = false;
    public float firingRange = 15f;

    public LayerMask enemyLayers;
    public LayerMask friendlyCover;

    public List<Collider> colliders = new List<Collider>();
    public float coverRange = 15f;

    public bool inCover = false;
    public bool lookForCover = true;

    public float desiredDistance = 15f;
    private float currentDistance = 15f;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleOverlapLogic();
    }

    private void HandleOverlapLogic()
    {
        Collider[] overlappingEnemies = Physics.OverlapSphere(transform.position, firingRange, enemyLayers);

        if (overlappingEnemies.Length > 0)
        {
            int index = ClosestCollider(overlappingEnemies);
            enemyInRange = true;
            GetComponent<PawnShoot>().enemy = overlappingEnemies[index].transform;
            transform.LookAt(overlappingEnemies[index].transform.position);
            currentDistance = (overlappingEnemies[index].transform.position - transform.position).magnitude;
        }
      

         

        
        //Ifall fiender är nära så letar vi efter skydd

        if (overlappingEnemies.Length == 0)
        {
            enemyInRange = false;
            navMeshAgent.destination = enemyBaseTransform.position;

        }


        if (enemyInRange && lookForCover)
        {
            if (!inCover) 
            {
               
                Collider[] overlappingCover = Physics.OverlapSphere(transform.position, coverRange, friendlyCover);
                for (int i = 0; i< overlappingCover.Length; i++)
                {
                    if (!overlappingCover[i].gameObject.GetComponent<CoverOccupier>().occupied)
                    {
                        colliders.Add(overlappingCover[i]);
                    }
                }
                float coverDistance = coverRange + 1;
                Vector3 ChosenPosition;
                if (desiredDistance < currentDistance)
                {
                   ChosenPosition = enemyBaseTransform.position;
                }
                else
                {
                    ChosenPosition = transform.position;
                }
               
                foreach(Collider collider in colliders)
                {
                    if(collider != null)
                    {
                        float tempDistance = (collider.transform.position - transform.position).magnitude;
                        if (tempDistance < coverDistance)
                        {
                            coverDistance = tempDistance;
                            ChosenPosition = collider.transform.position;
                        }
                    }
                    
                }
                navMeshAgent.destination = ChosenPosition;
                colliders.Clear();
            }
            else
            {

                Collider[] overlappingCover = Physics.OverlapSphere(transform.position, coverRange, friendlyCover);
                if(overlappingCover.Length > 0)
                {
                    int indexCover = ClosestCollider(overlappingCover);

                    navMeshAgent.destination = overlappingCover[indexCover].transform.position;
                }
               
              
                    //navMeshAgent.destination = enemyBaseTransform.position;
              
                
            }


        }
        GetComponent<PawnShoot>().enemyinRange = enemyInRange;
    }

    private int ClosestCollider(Collider[] collidersToCalculate)
    {
        int index = 0;
        float distance = 2000f;
        for (int i = 0; i < collidersToCalculate.Length; i++)
        {
            float tempDistance = (collidersToCalculate[i].transform.position - transform.position).magnitude;
            if(distance > tempDistance)
            {
                distance = tempDistance;
                index = i;
            }
        }



        return index;
    }
}
