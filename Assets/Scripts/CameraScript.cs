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
        // Force screen to the correct resolution when building standalone
        Screen.SetResolution(1080, 1920, false);
    }

}