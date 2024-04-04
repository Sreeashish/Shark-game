using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float targetAspect = 4f / 3f;

    void Start()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float scaleHeight = currentAspect / targetAspect;
        if (scaleHeight < 1.0f)
        {
            Camera camera = GetComponent<Camera>();
            camera.fieldOfView = camera.fieldOfView * scaleHeight;
        }
    }
}
