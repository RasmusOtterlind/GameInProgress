using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    //Movement
    public Transform muzzle;
    public Transform[] checkpoints;
    public float moveSpeed = 4f;
    private int currentCheckpoint = 0;
    private Transform targetPostion;
    private Vector3 velocity;

    //Layers for overlapsphere
    public LayerMask enemyLayers;
    public LayerMask coverLayers;



    private Rigidbody rigidBody;

    //Pip stats
    public float firingRange = 20f;



    private float distanceToEnemy;
    public bool enemyInRange = false;

    private bool shouldMove = true;

    //CoverLogic
    public float coverRange = 30f;
    private bool lookingForCover = false;
    private bool inCover = false;
    private float distanceToCover = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPostion = checkpoints[currentCheckpoint];
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleOverlapLogic();
        HandleTargetPosition();

    }
    private void FixedUpdate()
    {
        HandleMovement();
        
    }
    private void HandleTargetPosition()
    {
        //Ifall magnituden på vectorn är låg så har vi nåt fram.
        if ((transform.position - targetPostion.position).magnitude < 5f)
        {

            if(lookingForCover)
            {
                Debug.Log("inCover = true");
                inCover = true;
            }
            else if (currentCheckpoint < checkpoints.Length - 1 )
            {
                currentCheckpoint++;
            }
            else
            {
                shouldMove = false;
            }

        }
        if (!lookingForCover)
        {
            targetPostion = checkpoints[currentCheckpoint];
        }
    }
    private void HandleMovement()
    {
        if (!inCover)
        {
            velocity = (transform.position - targetPostion.position).normalized * -1f;
           
            rigidBody.velocity = velocity * 8f;
        }
    }

    private void HandleOverlapLogic()
    {
        Collider[] overlappingEnemies = Physics.OverlapSphere(transform.position, firingRange, enemyLayers);
      
        foreach (var enemyCollider in overlappingEnemies)
        {
           
            enemyInRange = true;
            lookingForCover = true;
            distanceToEnemy = (enemyCollider.transform.position - transform.position).magnitude;

        }
        //Ifall fiender är nära så letar vi efter skydd

        if (lookingForCover && !inCover)
        {
            Collider[] overlappingCover = Physics.OverlapSphere(transform.position, coverRange, coverLayers);
            foreach (var coverCollider in overlappingCover)
            {

                float tempDistance = (transform.position + coverCollider.transform.position).magnitude;
                distanceToCover = tempDistance;
                targetPostion.position = (coverCollider.transform.position);
               
            }
        }

        //Ingen nära behöver inte skydd
        if (overlappingEnemies.Length == 0)
        {
            distanceToEnemy = 100;
            enemyInRange = false;
            lookingForCover = false;
            distanceToCover = coverRange + 1;
        }

    }

    public void coverDestroyed()
    {
        targetPostion = checkpoints[currentCheckpoint];
    }
}
