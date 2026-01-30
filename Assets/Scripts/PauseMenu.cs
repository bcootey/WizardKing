using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public PlayerController playerMovement;
    public GameObject savingMenu;
    
    private bool menuOpen = false;
    void Update()
    {
        if (GameStateManager.instance != null &&
            GameStateManager.instance.State == GameState.Loading)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
            {
                ClosePauseMenu();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    public void ResumeButton()
    {
        ClosePauseMenu();
    }
    private void ClosePauseMenu()
    {
        if (!menuOpen) return;
        
        menuOpen = false;
        GameStateManager.instance.RemovePauseLock();
        PauseMenuUI.SetActive(false);
        playerMovement.SetSensitivity();
    }
    private void OpenPauseMenu()
    {
        if (menuOpen)
        {
            return;
        }
        menuOpen = true;
        GameStateManager.instance.AddPauseLock();
        PauseMenuUI.SetActive(true);
    }
}
