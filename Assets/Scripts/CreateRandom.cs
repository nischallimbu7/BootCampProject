using System.Collections.Generic;
using UnityEngine;

public class CreateRandom : MonoBehaviour
{
    public List<GameObject> Spawn = new List<GameObject>();

    void Start()
    {
        GameObject GoSpawn = Spawn[Random.Range(0, Spawn.Count)];
        Instantiate(GoSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
