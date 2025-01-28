using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public EnemyChase enemyChase;
    public WinPoint winPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyChase.isFalling)
        {
            StartCoroutine("EndGame");

        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);
        winPoint.EndGame();
        gameOverText.text = "You have been killed";

    }
}
