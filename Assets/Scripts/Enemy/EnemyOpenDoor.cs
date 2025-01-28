using System.Collections;
using UnityEngine;

public class EnemyOpenDoor : MonoBehaviour
{

    public Door door;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Door"))
        {
            door = other.gameObject.GetComponent<Door>();
            Debug.Log("touched door");
            if (!door.open)
            {
                door.RotateDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (door.open)
            {
                StartCoroutine(CloseDoor());
            }
        }
    }
    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(3);
        door.RotateDoor();
    }

}
