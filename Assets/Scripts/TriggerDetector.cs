using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    private bool hasEntered = false;
    private bool hasExited = false;
    
    public bool WasEntered()
    {
        return hasEntered;
    }
    
    public bool WasExited()
    {
        return hasExited;
    }


    public void ResetTriggers()
    {
        hasEntered = false;
        hasExited = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ResetTriggers();
            hasEntered = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ResetTriggers();
            hasExited = true;

        }
    }
}
