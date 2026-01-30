public interface IInteractable
{
    string GetPrompt();          //what text should show ("Open Door", "Save Game", etc.)
    void Interact();             //what happens when the player interacts
}