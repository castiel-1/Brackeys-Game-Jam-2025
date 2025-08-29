using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _currentLevel = 0;
    private int _storyIndex = 0;
    private bool _isPaused = false;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void PauseGame()
    {
        // debugging
        Debug.Log("paused game");

        Time.timeScale = 0f;  
        _isPaused = true;
    }

    public void ResumeGame()
    {
        // debugging
        Debug.Log("resumed game");

        Time.timeScale = 1f; 
        _isPaused = false;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + _currentLevel);
        _currentLevel++;
        _storyIndex++;
    }

    // loads the next story slide
    public void LoadNextStory()
    {
        SceneManager.LoadScene("Story " + _storyIndex);
        _storyIndex++;
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene("Level " + index);
        _currentLevel = index;
        _storyIndex = index + 1;
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
        _currentLevel = 0;
        _storyIndex = 0;
    }

    public void GameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level " + _currentLevel);
    }

    public void QuitGame()
    {
        Application.Quit();

        //debugging
        Debug.Log("game quit");
    }

}
