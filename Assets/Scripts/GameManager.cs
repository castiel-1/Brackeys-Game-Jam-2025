using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _currentLevel = 0;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!(scene.name == "GameOverScreen"))
        {
            _currentLevel = scene.buildIndex;

            // debugging
            Debug.Log("current index: " + _currentLevel);
        }
    }

    public void PauseMovement()
    {
        _playerMovement = FindFirstObjectByType<PlayerMovement>();
        _enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        if (_playerMovement)
        {
            _playerMovement.SetPlayerSpeedZero();
            _playerMovement.enabled = false;
        }
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
        SceneManager.LoadScene(_currentLevel + 1);
    }

    public void LoadStartScreen()
    {
        // debugging
        Debug.Log("loading start screen");

        SceneManager.LoadScene("StartScreen");
    }

    public void GameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public void RestartLevel()
    {
        // debugging
        Debug.Log("restarting level");

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
