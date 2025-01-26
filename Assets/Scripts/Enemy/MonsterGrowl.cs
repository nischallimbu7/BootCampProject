using System.Collections;
using UnityEngine;

public class MonsterGrowl : MonoBehaviour
{
    public AudioSource growlingAudio;
    public AudioClip[] clip;
    private float timeBetweenClips=4;
    bool isGrowling;
    private int audioIndex;
    public EnemyChase enemyChase;
    //public EnemyChase enemyChase;
    // Start is called before the first frame update
    void Start()
    {
        //clip = GetComponent<AudioClip>();
        //  audioSource = GetComponent<AudioSource>();
        //isGrowling = true;

        // audioSource.Play();
        InvokeRepeating("GetNewClip", 1, 10);

    }

    // Update is called once per frame
    void Update()
    {
      //  growlingAudio.clip = clip[audioIndex];
        // StartCoroutine("Growl");
        
            //growlingAudio.enabled = true;
            
           // StartCoroutine(GetNewClip());
       
        
        
    }

  
   void  GetNewClip()
    {
        //yield return new WaitForSeconds(timeBetweenClips);
        audioIndex=Random.Range(0, clip.Length);
       // isGrowling = true;
        //Debug.Log("new audio played: " + growlingAudio.clip);
        //audioSource.Play();
        growlingAudio.PlayOneShot(clip[audioIndex]);
    }
}
