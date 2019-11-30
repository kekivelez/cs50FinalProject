using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _shotTimer;
    [SerializeField] private float _minTimeBetweenShots = 0.2f;
    [SerializeField] private float _maxTimeBetweenShots = 3f;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private GameObject _projectile;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Set our timer to a random value between a range
        _shotTimer = UnityEngine.Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateShotTime();
    }


    #region Private Members
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        //Null Check
        if (!damageDealer) return;

        CalculateDamage(damageDealer);
    }

    private void CalculateDamage(DamageDealer damageDealer)
    {
        _health -= damageDealer.Damage;
        damageDealer.Hit();

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Calculate When the enemy should fire
    /// </summary>
    private void CalculateShotTime()
    {
        // Decrease the counter by a frame
        _shotTimer -= Time.deltaTime;

        if (_shotTimer <= 0)
        {
            Fire();

            // Reset timer
            _shotTimer = UnityEngine.Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

    /// <summary>
    /// Enemy fires
    /// </summary>
    private void Fire()
    {
        GameObject firedObject = Instantiate(original: _projectile,
                                             position: transform.position,
                                             rotation: Quaternion.identity) as GameObject;

        firedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_projectileSpeed);
    }


    #endregion
}
