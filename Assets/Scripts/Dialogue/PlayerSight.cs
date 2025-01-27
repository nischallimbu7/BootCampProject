using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    public float sightDistance;
    public GameObject origin;
    public DialogueManager manager;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 rayDirection = origin.transform.forward;
        bool hasSeenSomething = Physics.Raycast(origin.transform.position, rayDirection, out hit, sightDistance);
        
        Debug.DrawLine(origin.transform.position, (origin.transform.position + rayDirection) *sightDistance, Color.blue);

        if (hasSeenSomething && hit.collider.tag == "Interactable")
        {
            manager.narrator = hit.collider.GetComponent<Narrations>();
            Debug.Log(manager.narrator);
            Debug.Log("player saw the door");
            manager.ShowDialogue();

        }
        else
        {
            manager.CloseDialogue();
        }
    }
}
