
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public EnemyChase enemyChase;
    // Start is called before the first frame update
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyChase.isHittingPlayer = true;
            Debug.Log("enemyChase.isHittingPlayer =" + enemyChase.isHittingPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyChase.isHittingPlayer = false;
            Debug.Log("enemyChase.isHittingPlayer =" + enemyChase.isHittingPlayer);
<<<<<<< Updated upstream
=======
            
>>>>>>> Stashed changes
        }
    }


}
