using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] private float _padding = 1f;

    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;


    // Start is called before the first frame update
    void Start()
    {
        DefineMoveBoundaries();
    }

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

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // calculate the position of the player in the x axis
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
}
