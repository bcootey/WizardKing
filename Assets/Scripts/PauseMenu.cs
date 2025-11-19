using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public PlayerController playerMovement;
    public GameObject savingMenu;
    private bool menuOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPauseMenu();
        }
    }

    public void ResumeButton()
    {
        ClosePauseMenu();
    }
    private void ClosePauseMenu()
    {
        menuOpen = false;
        Pause.instance.ResumeGame();
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
        Pause.instance.PauseGame();
        PauseMenuUI.SetActive(true);
    }
}
