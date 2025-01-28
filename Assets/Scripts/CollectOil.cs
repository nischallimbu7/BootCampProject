using UnityEngine;

public class CollectOil : MonoBehaviour
{
    public LampLight lampLight;
    private bool isTouchingFuel = false, collectedFuel = false;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            isTouchingFuel = true;

            if (collectedFuel)
            {
                Destroy(other.gameObject);
                lampLight.lamp.intensity = lampLight.maxIntensity;
                isTouchingFuel = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            isTouchingFuel = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isTouchingFuel)
        {
            collectedFuel = true;
        }

    }
    void OnGUI()
    {
        if (isTouchingFuel)
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 200, 30), "Press 'F' to refill your lantern");

    }
}
