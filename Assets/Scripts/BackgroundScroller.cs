using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private float scrollSpeed = 0.02f;

    #endregion

    #region Fields
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
        backgroundMaterial.mainTextureOffset += scrollOffset * Time.deltaTime;
    }
}
