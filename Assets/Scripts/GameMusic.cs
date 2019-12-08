using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusic : MonoBehaviour
{
    
    void Awake()
    {
        CreateSingleton();
    }


    #region Private Members

    /// <summary>
    /// Creates a Singleton of the game music object so that it doesn't restart playing
    /// every new scene
    /// </summary>
    private void CreateSingleton()
    { 
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion
}
