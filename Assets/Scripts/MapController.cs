using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public ProxyScreen ProxyScreenPrefab;
    public GameObject FitToScanOverlay;

    Dictionary<int, ProxyScreen> proxyScreens = new Dictionary<int, ProxyScreen>();
    List<AugmentedImage> tempAugmentedImages = new List<AugmentedImage>();

    public void Update()
    {
        if (Session.Status != SessionStatus.Tracking) return;

        Session.GetTrackables<AugmentedImage>(tempAugmentedImages, TrackableQueryFilter.Updated);

        foreach (var image in tempAugmentedImages)
        {
            ProxyScreen proxyScreen = null;
            proxyScreens.TryGetValue(image.DatabaseIndex, out proxyScreen);
            if (image.TrackingState == TrackingState.Tracking && proxyScreen == null)
            {
                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                proxyScreen = (ProxyScreen)Instantiate(ProxyScreenPrefab, anchor.transform);
                proxyScreen.Image = image;
                proxyScreens.Add(image.DatabaseIndex, proxyScreen);
            }
            else if (image.TrackingState == TrackingState.Stopped && proxyScreen != null)
            {
                //TODO: What does "Stopped" mean?
                proxyScreens.Remove(image.DatabaseIndex);
                GameObject.Destroy(proxyScreen.gameObject);
            }
        }

        // Show the fit-to-scan overlay if there are no images that are Tracking.
        foreach (var visualizer in proxyScreens.Values)
        {
            if (visualizer.Image.TrackingState == TrackingState.Tracking)
            {
                FitToScanOverlay.SetActive(false);
                return;
            }
        }

        FitToScanOverlay.SetActive(true);
    }
}

