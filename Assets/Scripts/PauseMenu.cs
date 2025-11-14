using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public PlayerController playerMovement;
    public GameObject savingMenu;
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
        Pause.instance.ResumeGame();
        PauseMenuUI.SetActive(false);
        playerMovement.SetSensitivity();
    }
    private void OpenPauseMenu()
    {
        Pause.instance.PauseGame();
        PauseMenuUI.SetActive(true);
    }
}
