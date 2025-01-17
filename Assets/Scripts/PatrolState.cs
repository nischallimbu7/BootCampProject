using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour
{/*
    private NavMeshAgent agent;

    public Transform[] patrolPoints;
    public int patrolIndex = 0;
    void Setup() { }

    private void Update()
    {
        if (agent.remainingDistance < 0.5f)
            Patrol();
    }

    void Cleanup()
    {
    }

    void Patrol()
    {
        agent.SetDestination(patrolPoint[patrolIndex].position);
        //animator.SetBool("isWalking", true);
        if (distanceToPatrol < 1) //if enemy reaches its patrol point
        {
            StartCoroutine(GetNewPatrolPoint());
            // tell enemy to go to another random location

            //idle for a while
            //walkSpeed = 0;

        }
    }

    IEnumerator DelayedSetPatrol()
    {
        yield return new WaitForSeconds(2);
        Patrol();
        //GetNewPatrolPoint();

        GetNewPatrolPoint();
        agent.speed = walkSpeed;
        agent.SetDestination(patrolPoint[patrolIndex].position);
        transitioning = false;
        
    }

     IEnumerator Idle()
     {
         yield return new WaitForSeconds(2);
        // animator.SetFloat("Idle", true);
         GetNewPatrolPoint();

     }
    
    IEnumerator GetNewPatrolPoint()
    {
        yield return new WaitForSeconds(3);
        patrolIndex = Random.Range(0, patrolPoint.Length);
        Debug.Log("new patrol index is " + patrolIndex);
    }
    */
}
