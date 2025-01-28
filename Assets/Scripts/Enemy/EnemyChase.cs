using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    #region properties
    public enum State
    {
        Patrol,
        Chase,
        Attack
    }

    public State currentState;

    public Transform player;
    public NavMeshAgent agent;
    public Transform[] patrolPoint;
    public float detectRange = 5f;
     int patrolIndex;
    [SerializeField] float distanceToPatrol;
    
    public float sightDistance, catchDistance, stopDistance, attackTime;
    public float chaseSpeed, walkSpeed;
    
    public bool isSwiping = false, isDistracted = false, isWalking, hasDetected=false, isHittingPlayer, isFalling=false;
    public Transform rayCastOrigin;
    public Transform distractLocation;
    public Vector3 rayCastOffset;
    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 rayDirection;
    public LayerMask playerMask;


    public Distract distract;
    public Animator animator;
    public Rigidbody rb;
    //public Material material;

    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.isStopped = false;
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("player").transform;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        

        #region calculated variables
        direction = (player.position - transform.position).normalized;
        rayDirection = (transform.forward).normalized;

        RaycastHit hit;
        
        // find distance between enemy and player
        float playerDistance = Vector3.Distance(transform.position, player.position);
        // distance between enemy and target location
        distanceToPatrol = Vector3.Distance(transform.position, patrolPoint[patrolIndex].position);
        //animator.SetFloat("Walk", agent.speed);

        bool hasSeenPlayer = Physics.Raycast(rayCastOrigin.position, rayDirection, out hit, sightDistance, playerMask);
        Debug.Log("hasSeenPlayer= " + hasSeenPlayer);
        Debug.Log(hasSeenPlayer && hit.collider.gameObject.CompareTag("Player"));
        Debug.DrawLine(rayCastOrigin.position, rayCastOrigin.position + rayDirection * sightDistance, Color.red);
        #endregion

        // Debug.Log("remainingDistance = " + agent.remainingDistance + "isStopped = " + agent.isStopped);
        // Debug.Log("agent.remainingDistance < stopDistance && !agent.isStopped = " + (agent.remainingDistance < stopDistance && !agent.isStopped));


        if (playerDistance > detectRange && !agent.hasPath) //if player is out of range
        {
            Patrol();
        }
        else if (hasSeenPlayer && hit.collider.gameObject.tag != "Untagged" && hit.collider.gameObject.tag != "Door" || playerDistance < detectRange || hasDetected) // if enemy sees player
        {
            

            Debug.Log("saw player");
            Chase();

            if (playerDistance <= catchDistance && !isSwiping) // if enemy catches player
            {
               // SetNewState(State.Attack);
                agent.ResetPath();
                animator.SetTrigger("Swiping");
                isSwiping = true;
                StartCoroutine(ResetAttack());
            }
            else if (playerDistance <= catchDistance &&isSwiping)
            {
                agent.ResetPath();
            }
        }
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1);
        isSwiping = false;
        Debug.Log("attack reset");
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isSwiping && isHittingPlayer)
        {
            Debug.Log("touched player");
            StartCoroutine("KillPlayer");
            isSwiping = false;
        }
    }
    IEnumerator KillPlayer()
    {
         
        yield return new WaitForSeconds(attackTime);
        if (isSwiping && isHittingPlayer)
        {
            Debug.Log("hit player");
            isFalling=true;
        }
    }
    public void Patrol()
    {
        //SetNewState(State.Patrol);
       // Debug.Log("Patrol called");
        agent.SetDestination(patrolPoint[patrolIndex].position);
        //Debug.Log("agent.isStopped= " + agent.isStopped);

        if (agent.remainingDistance < stopDistance && !agent.isStopped) //if enemy reaches its patrol point
        {
         //   Debug.Log("Enemy reached destination");

            agent.isStopped = true; // causes this if statement to be exited
            agent.speed = 0;

            //isWalking = false;
            animator.SetBool("isWalking", false);
           
            // Debug.Log("isWalking = " + isWalking);

            StartCoroutine("GetNewPatrolPoint");
        }

        else if (isDistracted)
        {
            agent.SetDestination(distractLocation.position);
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
        //audioSource.Play();
    }
    void Chase()

    {
        // enemy chases player
        //SetNewState(State.Chase);

        agent.SetDestination(player.position);
        agent.speed = chaseSpeed;
    }
    //private void SetNewState(State newState)
    //{
    //    currentState = newState;

    //    switch (currentState)
    //    {
    //        case State.Patrol:
    //            material.color = Color.yellow;
    //            break;
    //        case State.Chase:
    //            material.color = Color.blue;
    //            break;
    //        case State.Attack:
    //            material.color = Color.red;
    //            break;
    //    }
    //}

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.DrawWireSphere(transform.position, catchDistance);
        
        //Debug.DrawRay(transform.position + rayCastOffset, direction);

    }
}
