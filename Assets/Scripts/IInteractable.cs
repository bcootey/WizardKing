public interface IInteractable
{
    string GetPrompt();          // What text should show ("Open Door", "Save Game", etc.)
    void Interact();             // What happens when the player interacts
}