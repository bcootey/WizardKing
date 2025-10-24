using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerInteractor : MonoBehaviour
{
    public static PlayerInteractor Instance { get; private set; }

    [Header("Interaction Settings")]
    public float interactRange = 3f;
    public KeyCode interactKey = KeyCode.E;
    public Camera playerCamera;
    public TextMeshProUGUI promptText;
    public float rayPauseDuration = 1f; // Default time to pause ray

    private IInteractable currentInteractable;
    private bool canCheckRay = true;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // Skip raycast checks if disabled or paused
        if (!canCheckRay || Pause.instance.isPaused)
        {
            ClearPrompt();
            return;
        }

        CheckForInteractable();

        if (currentInteractable != null && Input.GetKeyDown(interactKey))
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            currentInteractable = hit.collider.GetComponent<IInteractable>();

            if (currentInteractable != null)
            {
                promptText.text = $"[E] {currentInteractable.GetPrompt()}";
                promptText.enabled = true;
                return;
            }
        }

        currentInteractable = null;
        promptText.enabled = false;
    }

    public void ClearPrompt()
    {
        promptText.enabled = false;
        currentInteractable = null;
    }
    
    public void PauseRayForSeconds(float duration)
    {
        StartCoroutine(PauseRayRoutine(duration));
    }

    private IEnumerator PauseRayRoutine(float duration)
    {
        canCheckRay = false;
        ClearPrompt();
        yield return new WaitForSecondsRealtime(duration); // unaffected by Time.timeScale
        canCheckRay = true;
    }
}
