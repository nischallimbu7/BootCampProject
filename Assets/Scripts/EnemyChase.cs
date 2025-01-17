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
    public float sightDistance, catchDistance, stopDistance;
    public float chaseSpeed, walkSpeed;
    public Material material;
    public bool isSwiping = false, isDistracted = false, isWalking, isHittingPlayer;
    

    public Distract distract;
    public Animator animator;
    public string DeathScene;
    Vector3 direction;
    Vector3 rayDirection;
    public Rigidbody rb;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.isStopped = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        #region variables
        direction = (player.position - transform.position).normalized;
        rayDirection = (transform.forward).normalized;

        RaycastHit hit;

        Debug.DrawLine(transform.position, transform.position + rayDirection * sightDistance, Color.red);

        // find distance between enemy and player
        float playerDistance = Vector3.Distance(transform.position, player.position);
        // distance between enemy and random location
        distanceToPatrol = Vector3.Distance(transform.position, patrolPoint[patrolIndex].position);
        //animator.SetFloat("Walk", agent.speed);

        bool hasSeenPlayer = Physics.Raycast(transform.position, rayDirection, out hit, sightDistance);
        #endregion

       // Debug.Log("remainingDistance = " + agent.remainingDistance + "isStopped = " + agent.isStopped);
        Debug.Log("agent.remainingDistance < stopDistance && !agent.isStopped = " + (agent.remainingDistance < stopDistance && !agent.isStopped));
        

        if (playerDistance > detectRange && !agent.hasPath) //if player is out of range
        {
            SetNewState(State.Patrol);

            Patrol();
        
        }
        else if (hasSeenPlayer && hit.collider.gameObject.tag != "Untagged" || playerDistance < detectRange) // if enemy sees player
        {
            Debug.Log("saw player");
            Chase();

            if (playerDistance <= catchDistance && !isSwiping) // if enemy catches player
            {
                SetNewState(State.Attack);
                
                animator.SetTrigger("Swiping");
                isSwiping = true;
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isSwiping &&isHittingPlayer)
        {
            Debug.Log("touched player");
            StartCoroutine("KillPlayer");
            isSwiping = false;
            
        }
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(2);
        if (isHittingPlayer && isSwiping)
        player.gameObject.SetActive(false);
    }
   public void Patrol()
    {

        Debug.Log("Patrol called");
        agent.SetDestination(patrolPoint[patrolIndex].position);
        Debug.Log("agent.isStopped= " + agent.isStopped);
        
       if (agent.remainingDistance < stopDistance && !agent.isStopped) //if enemy reaches its patrol point
        {
            Debug.Log("Enemy reached destination");
            
            agent.isStopped = true;
            agent.speed = 0;
            
            //isWalking = false;
            animator.SetBool("isWalking", false);
           // Debug.Log("isWalking = " + isWalking);
            
            StartCoroutine("GetNewPatrolPoint");
        }

       else if (isDistracted)
        {
            agent.SetDestination(distract.transform.position);
            Debug.Log("Enemy distracted");
            if (agent.remainingDistance < stopDistance)
            {
                isDistracted = false;
            }
        }
    }
    IEnumerator GetNewPatrolPoint()
    {
            
            yield return new WaitForSeconds(1);
            
            patrolIndex = Random.Range(0, patrolPoint.Length);
            agent.isStopped = false;
            //Debug.Log("agent.remainingDistance= " + agent.remainingDistance);
            //Debug.Log("distanceToPatrol= " + distanceToPatrol);
            StartCoroutine("WalkToNextPoint");
        
    }

    IEnumerator WalkToNextPoint()
    {
        yield return new WaitForSeconds(3);
        
        //Debug.Log("new target");
        agent.speed = walkSpeed;
       // isWalking = true;
        animator.SetBool("isWalking", true);
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.DrawWireSphere(transform.position, catchDistance);
        //Debug.DrawRay(transform.position + rayCastOffset, direction);

    }

}
