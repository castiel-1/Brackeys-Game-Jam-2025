using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _currentLevel;

    private PlayerMovement _playerMovement;
    private Enemy[] _enemies;

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
    }

    public void ResumeGame()
    {
        // debugging
        Debug.Log("resumed game");

        Time.timeScale = 1f; 
    }

    public void PauseMovement()
    {
        _playerMovement = FindFirstObjectByType<PlayerMovement>();
        _enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        if(_playerMovement) _playerMovement.enabled = false;
        
        foreach(Enemy enemy in _enemies)
        {
            enemy.GetComponentInChildren<WaypointMover>().enabled = false;
            enemy.GetComponentInChildren<VisionCone>().enabled = false;
        }
    }

    public void ResumeMovement()
    {
        _playerMovement = FindFirstObjectByType<PlayerMovement>();
        _enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        if(_playerMovement) _playerMovement.enabled = true;

        foreach(Enemy enemy in _enemies)
        {
            enemy.GetComponentInChildren<WaypointMover>().enabled = true;
            enemy.GetComponentInChildren<VisionCone>().enabled = true;
        }
    }

    public void LoadNextLevel()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
        _currentLevel = 0;
    }

    public void GameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_currentLevel);
    }

    public void QuitGame()
    {
        Application.Quit();

        //debugging
        Debug.Log("game quit");
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

}
