using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    #region Fields
    [Header("Enemy Attributes")]
    [SerializeField] private float health = 100f;
    [SerializeField] private int scoreValue = 150;

    [Header("Shooting Atributes")]
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private AudioClip laserSound;
    [SerializeField] [Range(0, 1)] private float laserSoundVolume = 0.5f;


    [Header("Death Attributes")]
    [SerializeField] private GameObject deathVfx;
    [SerializeField] private float explosionDuration = 1f;
    [SerializeField] [Range(0,1)] private float deathSoundVolume = 0.7f;
    [SerializeField] private AudioClip deathSound;

    private float shotTimer;

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

        // Laser Sound
        AudioSource.PlayClipAtPoint(clip: laserSound,
                                position: Camera.main.transform.position,
                                  volume: laserSoundVolume);

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
        //Increment Score
        FindObjectOfType<GameSession>().AddScore(this.scoreValue);

        // Destroy the unit
        Destroy(gameObject);

        // Instantiate the death animation as a GameObject
        GameObject explosion = Instantiate(original: deathVfx,
                                           position: transform.position,
                                           rotation: transform.rotation);

        // Destroy the explosion game object after some time has elapsed
        Destroy(explosion, explosionDuration);

        // Play death sound
        AudioSource.PlayClipAtPoint(clip: deathSound,
                                position: Camera.main.transform.position,
                                  volume: deathSoundVolume);
    }

    #endregion
}
