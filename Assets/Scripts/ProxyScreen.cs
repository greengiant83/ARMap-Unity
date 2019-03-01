using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCoreInternal;
using UnityEngine;

public class ProxyScreen : MonoBehaviour
{
    public AugmentedImage Image;

    public GameObject FrameLowerLeft;
    public GameObject FrameLowerRight;
    public GameObject FrameUpperLeft;
    public GameObject FrameUpperRight;

    public void Update()
    {
        if (Image == null) return;
        /*if (Image == null || Image.TrackingState != TrackingState.Tracking)
        {
            FrameLowerLeft.SetActive(false);
            FrameLowerRight.SetActive(false);
            FrameUpperLeft.SetActive(false);
            FrameUpperRight.SetActive(false);
            return;
        }*/

        float halfWidth = Image.ExtentX / 2;
        float halfHeight = Image.ExtentZ / 2;
        FrameLowerLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.back);
        FrameLowerRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.back);
        FrameUpperLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.forward);
        FrameUpperRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);

        FrameLowerLeft.SetActive(true);
        FrameLowerRight.SetActive(true);
        FrameUpperLeft.SetActive(true);
        FrameUpperRight.SetActive(true);
    }
}

