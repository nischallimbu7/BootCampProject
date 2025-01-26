using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFootsteps : MonoBehaviour
{
    public EnemyChase enemyChase;
    public AudioSource footstepsAudio;
    public AudioClip[] clip;

    //public float timeBetweenClips = 5;
    
    private int audioIndex=0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        footstepsAudio.clip = clip[audioIndex];
        
    }

    void GetNewClip()
    {
       
       // audioSource.PlayOneShot(clip[audioIndex]);
        audioIndex = Random.Range(0, clip.Length);
       // Debug.Log("new footstep audio: " + footstepsAudio.clip);
        //footstepsAudio.Play();
        
    }

    public void PlayFootstep()
    {
        //footstepsAudio.enabled = true;
        //Debug.Log("footstepsAudio.enabled= " + footstepsAudio.enabled);
        footstepsAudio.PlayOneShot(clip[audioIndex]);
    }

   

}
