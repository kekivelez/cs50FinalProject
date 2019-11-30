using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private int _damage = 100;

    #endregion

    #region Public Members
    public int Damage
    {
        get { return _damage; }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
    #endregion
}
