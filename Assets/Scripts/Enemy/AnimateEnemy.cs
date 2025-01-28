using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemy : MonoBehaviour
{
    public Animator animator;
    public EnemyChase enemyChase;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("enemychase.isWalking= " + enemyChase.isWalking);
        //if (enemyChase.isWalking)
        //{
        //    animator.SetBool("isWalking", true);

        //    Debug.Log("animate script");
        //}
        //else if (!enemyChase.isWalking)
        //{
        //    animator.SetBool("isWalking", false);
        //}
    }
}
