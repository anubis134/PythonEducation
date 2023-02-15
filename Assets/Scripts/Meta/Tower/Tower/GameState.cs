using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    internal static GameState Instance;
    [SerializeField]
    private GameObject _completeScreen;
    [SerializeField]
    private float _timeToShowCompleteScreen = 2F;

    private void Start()
    {
        Instance = this;
    }

    internal void ShowCompleteScreen() 
    {
        StartCoroutine(ShowCompleteScreenRoutine());
    }

    private IEnumerator ShowCompleteScreenRoutine() 
    {
        yield return new WaitForSeconds(_timeToShowCompleteScreen);
            //_completeScreen.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel(int levelNumber) 
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LoadNextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadNextLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
