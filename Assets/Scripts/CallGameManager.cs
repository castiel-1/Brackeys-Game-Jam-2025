using UnityEngine;

public class CallGameManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void RestartLevel()
    {
        gameManager.RestartLevel();
    }

    public void LoadNextLevel()
    {
        gameManager.LoadNextLevel();
    }

    public void LoadMainMenu()
    {
        gameManager.LoadStartScreen();
    }

    public void Quit()
    {
        gameManager.QuitGame();
    }
}
