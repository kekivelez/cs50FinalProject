using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Upadates the Player health information visible on the screen
/// </summary>
public class HealthDisplay : MonoBehaviour
{

    #region Fields
    private Text healthText;
    private Player player;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.Health.ToString();
    }
}
