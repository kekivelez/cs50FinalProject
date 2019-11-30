using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Player : MonoBehaviour
{
    #region Config Parameters
    [Header("Player")]
    [SerializeField] private int _health = 200;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _padding = 1f;

    [Header("Projectile")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _rateOfFire = 0.05f;
    #endregion


    #region Instance Variables
    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;
    #endregion

    #region Co-routines
    private Coroutine _firingCoroutine;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        DefineMoveBoundaries();
    }

    // Update is called once per frame

    void Update()
    {
        Move();
        Fire();
    }


    #region Private Members
    /// <summary>
    /// Defines how the player is going to move
    /// </summary>
    private void Move()
    {
        // Calculate the position of the player in the x axis
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    /// <summary>
    /// Defines the boundaries of the game world
    /// </summary>
    private void DefineMoveBoundaries()
    {
        // Define the game camera
        var gameCamera = Camera.main;

        // Determine the x point at the (left) edge of the map
        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _padding;

        // Determine the x point at the (right) edge of the map
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _padding;

        // Determine the y point at the (bottom) edge of the map
        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _padding;

        // Determine the y point at the (top) edge of the map
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _padding;

    }

    /// <summary>
    /// Defines how the player is going to fire
    /// </summary>
    private void Fire()
    {
        // If the key associated with the input setting labeled Fire1 is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            _firingCoroutine = StartCoroutine(ContinuousFire());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCoroutine);
        }
    }

    /// <summary>
    /// Co-routine that enables continuous fire
    /// </summary>
    /// <returns></returns>
    private IEnumerator ContinuousFire()
    {
        while (true)
        {
            // Create an instance of the laser prefab object (as a game object) and set its position
            var laser = Instantiate(original: _laserPrefab,
                position: transform.position, // Offset
                rotation: Quaternion.identity); // No rotation

            // Set the speed at which the projectile travels
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(x: 0, y: _projectileSpeed);

            // Yield execution of this call for RateOfFire seconds
            yield return new WaitForSeconds(_rateOfFire);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        // Null Check
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

    #endregion
}
