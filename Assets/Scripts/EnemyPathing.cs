using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    #region Fields
    private int waypointIndex = 0;
    private List<Transform> waypoints;
    #endregion

    #region Properties
    public WaveConfig WaveConfig { private get; set; }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        waypoints = WaveConfig.GetWaypoints();
        //Start at waypoint position 0
        this.transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    #region Private Members
    /// <summary>
    /// Determines how to travel from one waypoint to the next and destroys when finished traveling
    /// </summary>
    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            // Target waypoint
            var targetPosition = waypoints[waypointIndex].transform.position;

            // How much has been/can be moved this frame
            var travelDistance = WaveConfig.MoveSpeed * Time.deltaTime;

            // Move
            this.transform.position = Vector2.MoveTowards(transform.position, targetPosition, travelDistance);

            // If we reach waypoint change target
            if (this.transform.position == targetPosition)
            {
                waypointIndex++;
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
