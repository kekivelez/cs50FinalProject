using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Player : MonoBehaviour
{
    // Config Parameters
    [SerializeField] public float MoveSpeed = 10f;
    [SerializeField] public float Padding = 1f;
    [SerializeField] public float ProjectileSpeed = 10f;
    [SerializeField] public GameObject LaserPrefab;

    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;


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

    /// <summary>
    /// Defines how the player is going to move
    /// </summary>
    private void Move()
    {
        // Calculate the position of the player in the x axis
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;

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
        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + Padding;

        // Determine the x point at the (right) edge of the map
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - Padding;

        // Determine the y point at the (bottom) edge of the map
        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + Padding;

        // Determine the y point at the (top) edge of the map
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Padding;

    }

    /// <summary>
    /// Defines how the player is going to fire
    /// </summary>
    private void Fire()
    {
        // If the key associated with the input setting labeled Fire1 is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // Create an instance of the laser prefab object (as a game object) and set its position
            var laser = Instantiate(LaserPrefab, 
                                    transform.position, // Offset
                                    Quaternion.identity); // No rotation

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(x: 0, y: ProjectileSpeed);

        }
    }
}
