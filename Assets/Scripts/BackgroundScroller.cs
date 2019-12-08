using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cause the background to scroll by controlling the offset of the y axis
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    #region Fields
    [SerializeField] private float scrollSpeed = 0.02f;

    private Material backgroundMaterial;
    private Vector2 scrollOffset;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        scrollOffset = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Progress the background scroller
        backgroundMaterial.mainTextureOffset += scrollOffset * Time.deltaTime;
    }
}
