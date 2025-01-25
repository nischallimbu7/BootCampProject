using UnityEngine;

public class Distract : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyChase enemyChase;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
