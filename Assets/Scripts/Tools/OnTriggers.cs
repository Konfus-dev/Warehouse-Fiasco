using UnityEngine;

public class OnTriggers : MonoBehaviour
{
    public bool isTriggered;
    public Collider selectedObject;

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        selectedObject = other;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        selectedObject = null;
    }
}
