using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private float lastWidth;
    private float lastHeight;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1080, 1920, false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}