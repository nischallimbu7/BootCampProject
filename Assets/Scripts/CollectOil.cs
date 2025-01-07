using UnityEngine;

public class CollectOil : MonoBehaviour
{
    public LampLight lampLight;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            Destroy(other.gameObject);
            lampLight.lamp.intensity = lampLight.maxIntensity;
        }
    }
}
