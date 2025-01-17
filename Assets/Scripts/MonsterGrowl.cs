using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGrowl : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float timeBetweenClips=5;
    public bool isGrowling=false;
    // Start is called before the first frame update
    void Start()
    {
        clip = GetComponent<AudioClip>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine("Growl");
        if (isGrowling)
        {
            audioSource.PlayOneShot(clip);
            isGrowling = false;
        }
    }

    IEnumerator Growl()
    {
        yield return new WaitForSeconds(timeBetweenClips) ;
        isGrowling =true;
        Debug.Log("isGrowling= " + isGrowling);
    }
}
