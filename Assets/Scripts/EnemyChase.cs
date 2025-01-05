using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    public Transform[] patrolPoint;
    float detectRange = 5f;
    public int patrolIndex = 0;
    float distanceToPatrol;
    public Vector3 rayCastOffset;
    public float sightDistance, catchDistance;
    float chaseSpeed;
    public bool walking;
    public Animator animator;
    public string DeathScene;
    Vector3 direction;
 
    void Start()
    {
        walking = true;
        agent = GetComponent<NavMeshAgent>();
     }

    // Update is called once per frame
    void Update()
    {

        direction = (player.position - transform.position).normalized;

        RaycastHit hit;
        
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))

        {
            if (hit.collider.gameObject.tag == "Player")

            {
                Debug.Log("player spotted by raycast");
                // walking = false;
                StopCoroutine("Wait");
            }
        }

        // find distance between enemy and player
        float playerDistance = Vector3.Distance(transform.position, player.position);
        // distance between enemy and random location
        distanceToPatrol = Vector3.Distance(transform.position, patrolPoint[patrolIndex].position);
        
        if (playerDistance>detectRange) //if player is out of range
        {
            StartCoroutine("Wait");
            new WaitForSeconds(2);
            StopCoroutine("Wait");
            //enemy goes to its patrol point
            Patrol();
        }
        else if ( playerDistance<detectRange ) // if enemy sees player
        {
            // enemy chases player
            //Debug.Log("player spotted");
            agent.SetDestination(player.position);
            //StopCoroutine("Wait");
            
            //agent.speed = chaseSpeed;
            //if (playerDistance <= catchDistance) // if enemy catches player
            //{
            //    //player is dead
            //    Debug.Log("player dead");
            //    player.gameObject.SetActive(false);
            //    animator.SetBool("Attack", true);
            //    StartCoroutine("DeathSequence");
            //}
        }
    }
    void Patrol()
    {
        agent.SetDestination(patrolPoint[patrolIndex].position);
        if (distanceToPatrol < 1) //if enemy reaches its patrol point
        {
            // tell enemy to go to another random location
            GetNewPatrolPoint();
        }
    }
    void GetNewPatrolPoint()
    {
        patrolIndex = Random.Range(0, patrolPoint.Length);
        Debug.Log("new patrol index is " + patrolIndex);
    }

    IEnumerator Wait()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(1);
        agent.SetDestination(transform.position);
        walking = true;
       // GetNewPatrolPoint();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.DrawRay(transform.position + rayCastOffset, direction);
        
    }

    //IEnumerator DeathSequence()
    //{
    //    Debug.Log("Death sequence started");
    //    yield return new WaitForSeconds (1);
    //    SceneManager.LoadScene("DeathScene");
    //}
}
