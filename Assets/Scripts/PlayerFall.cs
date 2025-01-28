using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerFall : MonoBehaviour
{

    public EnemyChase enemyChase;
    public RigidbodyFirstPersonController rigidbodyFirstPersonController;
    public Vector3 newrotation;
    public Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    public Vector3 target = new Vector3(-90, 0, 0);
    public Camera enemyCam;
    public Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        //  enemyCam=enemyChase.gameObject.GetComponentInChildren<Camera>();
        playerCam = GetComponentInChildren<Camera>();
        enemyChase = enemyCam.gameObject.GetComponentInParent<EnemyChase>();

    }

    // Update is called once per frame
    void Update()
    {

        newrotation = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.3f);
        if (enemyChase.isFalling)
        {
            rigidbodyFirstPersonController.enabled = false;
            transform.rotation = Quaternion.Euler(newrotation);
            // rigidbodyFirstPersonController.gameObject.GetComponent<Rigidbody>().isKinematic=true;
            enemyChase.agent.speed = 0;
            enemyChase.animator.SetBool("isWalking", false);
            rb.AddForce(Vector3.back, ForceMode.Impulse);
            rb.drag = 11;
            StartCoroutine(KillCam());
        }

        IEnumerator KillCam()
        {
            yield return new WaitForSeconds(4);
            playerCam.enabled = false;
            enemyCam.gameObject.SetActive(true);

        }




    }
}
