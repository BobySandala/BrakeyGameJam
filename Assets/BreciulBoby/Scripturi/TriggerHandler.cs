using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " exited the trigger.");
    }

    public void HandleExit(Collider other)
    {
        OnTriggerExit(other); // Manually call the exit function
    }
}
