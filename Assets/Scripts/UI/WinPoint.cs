using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class WinPoint : MonoBehaviour
{
    public GameObject gameOverCanvas;

    public EnemyChase enemyChase;
    public RigidbodyFirstPersonController rigidbodyFirstPersonController;
    public GameObject player;
    public Rigidbody lanternPhysics;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyFirstPersonController = player.GetComponent<RigidbodyFirstPersonController>();
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

    public void EndGame()
    {

        Debug.Log("game ended");
        enemyChase.enabled = false; // all enemies stop moving

        rigidbodyFirstPersonController.enabled = false;// player cant move
        gameOverCanvas.SetActive(true);// load end game UI
        lanternPhysics.isKinematic = false;
        return;

    }
}



