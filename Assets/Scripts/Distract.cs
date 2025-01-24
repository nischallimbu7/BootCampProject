using UnityEngine;

public class Distract : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyChase enemyChase;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touched distraction");
            enemyChase.isDistracted = true;
            enemyChase.distractLocation = transform;
        }
    }
}
