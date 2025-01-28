using UnityEngine;

public class LampLight : MonoBehaviour
{
    public Light lamp;
    public float startTime, reduceTimer, reductionSpeed, maxIntensity;


    // Start is called before the first frame update
    void Start()
    {
        lamp.intensity = maxIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (lamp.intensity > 0)
        {
            lamp.intensity -= Time.deltaTime * reductionSpeed;
        }
    }



}
