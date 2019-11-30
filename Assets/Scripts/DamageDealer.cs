using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private int damage = 100;

    #endregion

    #region Properties
    public int Damage
    {
        get { return damage; }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
    #endregion
}
