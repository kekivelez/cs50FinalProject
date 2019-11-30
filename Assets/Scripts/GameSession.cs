using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    #region Fields
    private int score;



    #endregion

    #region Properties
    public int Score
    {
        get { return score; }
        private set { this.score = value; }
    }

    #endregion
    private void Awake()
    {
        CreateSingleton();
    }

    #region Private Members

    private void CreateSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    #region Public Members
    /// <summary>
    /// Adds to the score
    /// </summary>
    /// <param name="scoreValue">The amount of points to add to the score</param>
    public void AddScore(int scoreValue)
    {
        Score += scoreValue;
    }
    
    /// <summary>
    /// Reset the score
    /// </summary>
    public void ResetGame()
    {
        Destroy(gameObject);
    }
    #endregion
}
