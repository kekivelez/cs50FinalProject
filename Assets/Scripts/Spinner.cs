using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    #region Fields
    [SerializeField] private float rotationSpeed = 180f;

    #endregion



    // Update is called once per frame
    void Update()
    {
        // Calculate rotation
        var rotation = rotationSpeed * Time.deltaTime;

        // Rotate
        transform.Rotate(0, 0, rotation);
    }
}
