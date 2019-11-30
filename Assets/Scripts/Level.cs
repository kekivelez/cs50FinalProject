using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    #region Fields
    [SerializeField] private float delayInSeconds = 1f;
    #endregion

    public void LoadStartMenu()
    {
        // TODO: Refactor to not use string reference
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayGameOver());
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #region Private Members
    /// <summary>
    /// Coroutine for delaying the game over screen after death
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    #endregion

}
