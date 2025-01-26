using UnityEngine;

public class DetectRange : MonoBehaviour
{
    public EnemyChase enemyChase;

    private void Start()
    {
        enemyChase = GetComponentInParent<EnemyChase>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            enemyChase.hasDetected = true;
            Debug.Log("Detected player with collision box");
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
