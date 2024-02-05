using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] goldMines;
    private Transform distraction; // Distraction variable

    public bool isDistracted = false;

    [SerializeField] float targetOffset = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goldMines = GameObject.FindGameObjectsWithTag("GoldMine");

        InvokeRepeating("SetClosestTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set the closest target to the enemy
    private void SetClosestTarget()
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject goldMine in goldMines)
        {
            float distance = Vector3.Distance(transform.position, goldMine.transform.position);
            if (distance < closestDistance)
            {
                closestTarget = goldMine;
                closestDistance = distance;
            }
        }

        // If the enemy is not distracted, move towards the closest gold mine
        if (closestTarget != null && !isDistracted)
        {
            agent.SetDestination(closestTarget.transform.position + new Vector3(0, 0, targetOffset));
        }
        // If the enemy is distracted, move towards the distraction
        else if (isDistracted)
        {
            FindDistractionTarget();
        }
    }

    private void FindDistractionTarget(){
        //Finds all distractions in the scene
        GameObject[] distractions = GameObject.FindGameObjectsWithTag("Distraction");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestDistraction = null;

        //Finds the closest distraction to the enemy
        foreach (GameObject distraction in distractions)
        {
            float distanceToDistraction = Vector3.Distance(transform.position, distraction.transform.position);
            if (distanceToDistraction < shortestDistance)
            {
                shortestDistance = distanceToDistraction;
                nearestDistraction = distraction;
            }
        }

        //If there is a distraction, the enemy will move towards it
        if (nearestDistraction != null)
        {
            distraction = nearestDistraction.transform;
            agent.SetDestination(distraction.position + new Vector3(0, 0, targetOffset));
            isDistracted = true;
        }
        //If there is no distraction, the enemy will continue to move towards the gold mine
        else
        {
            distraction = null;
            isDistracted = false;
        }
    }
}
