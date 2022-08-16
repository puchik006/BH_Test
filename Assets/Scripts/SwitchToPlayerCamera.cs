using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchToPlayerCamera : MonoBehaviour
{
    private void OnEnable()
    {
        if (Camera.main.enabled)
        {
            Camera.main.enabled = false;
        }
    }
}
