using UnityEngine;

public class Distract : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyChase enemyChase;
    public float noiseRange; //how far the noise of the object reaches/ yellow sphere
    float distanceToTarget;
    public DialogueManager manager;

    public AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }
    private void FixedUpdate()
    {
        distanceToTarget = Vector3.Distance(transform.position, enemyChase.transform.position); //white sphere
    }
    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Player") && distanceToTarget < noiseRange) // if white sphere is smaller than yellow sphere
        {
            Debug.Log("touched distraction, monster is close enough to hear");
            enemyChase.isDistracted = true;
            enemyChase.distractLocation = transform;
            //rb.AddForce(Vector3.forward *100,ForceMode.Impulse);
            audioSource.PlayOneShot(clip);

        }
        else if (collision.gameObject.CompareTag("Player") && distanceToTarget > noiseRange)
            Debug.Log("touched distraction but monster is too far away to hear");
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, noiseRange); //yellow sphere
    //    Gizmos.color = Color.white;
    //    Gizmos.DrawWireSphere(transform.position, distanceToTarget); //white sphere
    //}
}
