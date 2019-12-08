using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    #region Fields
    [SerializeField] private int damage = 100;

    #endregion

    #region Properties
    public int Damage
    {
        get { return damage; }
    }
    #endregion

    /// <summary>
    /// Destroy the Game object that was hit by this Damage Dealer
    /// </summary>
    public void Hit()
    {
        Destroy(gameObject);
    }
}
