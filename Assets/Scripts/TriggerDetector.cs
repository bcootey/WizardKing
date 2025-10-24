using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    private bool hasEntered = false;
    private bool hasExited = false;

    /// <summary>
    /// Returns true if OnTriggerEnter was triggered since last reset.
    /// </summary>
    public bool WasEntered()
    {
        return hasEntered;
    }

    /// <summary>
    /// Returns true if OnTriggerExit was triggered since last reset.
    /// </summary>
    public bool WasExited()
    {
        return hasExited;
    }

    /// <summary>
    /// Resets both enter and exit flags.
    /// </summary>
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
