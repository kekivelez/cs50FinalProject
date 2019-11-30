using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    #region Config Parameters
    [SerializeField] private float _scrollSpeed = 0.02f;

    #endregion

    #region Instance Variables
    private Material _backgroundMaterial;
    private Vector2 _scrollOffset;


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _backgroundMaterial = GetComponent<Renderer>().material;
        _scrollOffset = new Vector2(0, _scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _backgroundMaterial.mainTextureOffset += _scrollOffset * Time.deltaTime;
    }
}
