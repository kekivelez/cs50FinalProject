﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] public WaveConfig WaveConfig;
    [SerializeField] public List<Transform> Waypoints;
    [SerializeField] public float Speed = 2f;
    #endregion

    #region Instance Variables
    private int _waypointIndex = 0;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        Waypoints = WaveConfig.GetWaypoints();
        //Start at waypoint position 0
        this.transform.position = Waypoints[_waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    #region Private Members
    private void Move()
    {
        if (_waypointIndex < Waypoints.Count)
        {
            // Target waypoint
            var targetPosition = Waypoints[_waypointIndex].transform.position;

            // How much has been/can be moved this frame
            var travelDistance = Speed * Time.deltaTime;

            // Move
            this.transform.position = Vector2.MoveTowards(transform.position, targetPosition, travelDistance);

            // If we reach waypoint change target
            if (this.transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            //We are at the end of the waypoints
            Destroy(gameObject);
        }
    }
    #endregion
}
