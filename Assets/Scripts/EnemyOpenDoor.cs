using UnityEngine;

public class EnemyOpenDoor : MonoBehaviour
{
    
    public Door door;

    private void Update()
    {
        Debug.Log("!door.open= " + door.open);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Door"))
        {
            door = other.gameObject.GetComponent<Door>();
            Debug.Log("touched door");
            if (!door.open)
         {
                door.OpenDoor();
         }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        door.OpenDoor();
    }

}
