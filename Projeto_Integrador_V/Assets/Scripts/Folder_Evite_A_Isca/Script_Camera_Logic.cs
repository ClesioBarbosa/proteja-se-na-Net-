using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Script_Camera_Logic : MonoBehaviour
{

    public CinemachineVirtualCamera vcam;
    public CinemachineTrackedDolly dolly;

    bool Had_To_Move;

    void Start()
    {
        Had_To_Move = false;
        // Access the Tracked Dolly component from the Virtual Camera
        dolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    void Update()
    {
        if (Had_To_Move)
        {
            // Or increment it over time for manual movement
            dolly.m_PathPosition += Time.deltaTime * 1.0f;
        }
        else
        {
            dolly.m_PathPosition = 0f;
        }
        
    }

    public void Start_Moving()
    {
        Had_To_Move=true;
    }
}
