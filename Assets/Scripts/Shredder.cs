using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy game objects that collide with it
/// </summary>
public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
