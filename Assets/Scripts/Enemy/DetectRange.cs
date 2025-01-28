using UnityEngine;
using UnityEngine.UIElements;

public class DetectRange : MonoBehaviour
{
    public EnemyChase enemyChase;
    public LayerMask wallLayer;
    public GameObject player;
    private Vector3 directionToPlayer;
    private float distanceToPlayer;

    private void Start()
    {
        enemyChase = GetComponentInParent<EnemyChase>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
       
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            directionToPlayer = (player.transform.position - transform.position).normalized;
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
           // Debug.Log(directionToPlayer + ", " + distanceToPlayer);
            //Debug.DrawLine(transform.position, player.transform.position * distanceToPlayer, Color.blue);
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallLayer))
            {

                Debug.Log(other.gameObject);
                Debug.Log("other.gameObject.layer = " + other.gameObject.layer);
                enemyChase.hasDetected = true;
                Debug.Log("Detected player with collision box");
            }

            else if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallLayer))
            {
                Debug.Log("didnt see player"); return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyChase.hasDetected = false;
            Debug.Log("player exited collision box");
        }
    }
}
