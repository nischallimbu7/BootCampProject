using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPoint : MonoBehaviour
{
    public EnemyChase enemyChase;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
           EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("player won");
        enemyChase.enabled = false; // all enemies stop moving
        // player cant move
        // load win UI
        //SceneManager.LoadScene(0);
    }
}



