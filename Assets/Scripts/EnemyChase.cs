using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Attack
    }

    public State currentState;

    public Transform player;
    NavMeshAgent agent;
    public Transform[] patrolPoint;
    public float detectRange = 5f;
    public int patrolIndex = 0;
    float distanceToPatrol;
    public Vector3 rayCastOffset;
    public float sightDistance, catchDistance;
    public float chaseSpeed, walkSpeed;
    public Material material;


    public Animator animator;
    public string DeathScene;
    Vector3 direction;

    private bool transitioning = false;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        direction = (player.position - transform.position).normalized;

        RaycastHit hit;

        Debug.DrawLine(transform.position, transform.position + direction * sightDistance, Color.cyan);

        // find distance between enemy and player
        float playerDistance = Vector3.Distance(transform.position, player.position);
        // distance between enemy and random location
        distanceToPatrol = Vector3.Distance(transform.position, patrolPoint[patrolIndex].position);
        animator.SetFloat("Walk", agent.speed);



        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))

        {
            if (hit.collider.gameObject.tag == "Player")

            {
                Chase();
            }
        }

        else if (playerDistance > detectRange && !transitioning) //if player is out of range
        {
            SetNewState(State.Patrol);

            //enemy goes to its patrol point after a delay
            //Invoke("Patrol", 5); 
            StartCoroutine(DelayedSetPatrol());
            transitioning = true;
            //
        }
        else if (playerDistance < detectRange) // if enemy sees player
        {
            Chase();


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
        //animator.SetBool("isWalking", true);
        if (distanceToPatrol < 1) //if enemy reaches its patrol point
        {
            // tell enemy to go to another random location
            GetNewPatrolPoint();
            //idle for a while
            //walkSpeed = 0;

        }
    }

    IEnumerator DelayedSetPatrol()
    {
        yield return new WaitForSeconds(2);

        GetNewPatrolPoint();
        agent.speed = walkSpeed;
        agent.SetDestination(patrolPoint[patrolIndex].position);
        transitioning = false;
    }
    void GetNewPatrolPoint()
    {
        patrolIndex = Random.Range(0, patrolPoint.Length);
        Debug.Log("new patrol index is " + patrolIndex);
    }

    void Chase()

    {

        // enemy chases player
        SetNewState(State.Chase);

        agent.SetDestination(player.position);
        agent.speed = chaseSpeed;

    }

    private void SetNewState(State newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case State.Patrol:
                material.color = Color.yellow;
                break;
            case State.Chase:
                material.color = Color.blue;
                break;
            case State.Attack:
                material.color = Color.red;
                break;
        }
    }

    //IEnumerator Wait()
    //{
    //    Debug.Log("waiting");
    //    yield return new WaitForSeconds(1);
    //    agent.SetDestination(transform.position);
    //    walking = true;
    //   // GetNewPatrolPoint();
    //}


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Debug.DrawRay(transform.position + rayCastOffset, direction);

    }

    //IEnumerator DeathSequence()
    //{
    //    Debug.Log("Death sequence started");
    //    yield return new WaitForSeconds (1);
    //    SceneManager.LoadScene("DeathScene");
    //}
}
