using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    #region Fields
    private Text scoreText;
    private GameSession gameSession;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // Reference to the text component
        scoreText = GetComponent<Text>();

        // Reference to the game session
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the score display text
        scoreText.text = gameSession.Score.ToString();
    }
}
