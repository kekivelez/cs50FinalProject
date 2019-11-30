using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private float health = 100f;
    [SerializeField] private float shotTimer;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float explosionDuration = 1f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject deathVfx;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Set our timer to a random value between a range
        shotTimer = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateShotTime();
    }


    #region Private Members

    /// <summary>
    /// Calculate When the enemy should fire
    /// </summary>
    private void CalculateShotTime()
    {
        // Decrease the counter by a frame
        shotTimer -= Time.deltaTime;

        if (shotTimer <= 0)
        {
            Fire();

            // Reset timer
            shotTimer = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    /// <summary>
    /// Enemy fires
    /// </summary>
    private void Fire()
    {
        GameObject firedObject = Instantiate(original: projectile,
                                             position: transform.position,
                                             rotation: Quaternion.identity) as GameObject;

        firedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    /// <summary>
    /// Collision Event Handler
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        //Null Check
        if (!damageDealer) return;

        CalculateDamage(damageDealer);
    }

    /// <summary>
    /// Calculate the damage taken by the enemy
    /// </summary>
    private void CalculateDamage(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage;
        damageDealer.Hit();

        if (health <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// Destroys game object and spawns VFX
    /// </summary>
    private void Death()
    {
        // Destroy the unit
        Destroy(gameObject);

        // Instantiate the death animation as a GameObject
        GameObject explosion = Instantiate(original: deathVfx,
                                           position: transform.position,
                                           rotation: transform.rotation);

        Destroy(explosion, explosionDuration);
    }

    #endregion
}
