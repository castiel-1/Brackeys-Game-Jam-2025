using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTimingController : MonoBehaviour
{
    [SerializeField] private List<ToggleInfo> _toggleInfo;

    private void Start()
    {
        foreach(ToggleInfo toggleInfo in _toggleInfo)
        {
            foreach(CameraInfo cameraInfo in toggleInfo.cameraInfo)
            {
                cameraInfo.camera.IsOn = cameraInfo.startState;
            }
        }

        StartCoroutine(ToggleCameraRoutine());
    }

    IEnumerator ToggleCameraRoutine()
    {
        while (true)
        {
            foreach (ToggleInfo toggleInfo in _toggleInfo)
            {
                yield return new WaitForSeconds(toggleInfo.toggleAfter);

                foreach (CameraInfo cameraInfo in toggleInfo.cameraInfo)
                {
                    cameraInfo.camera.Toggle();
                }

            }
        }
    }
}
