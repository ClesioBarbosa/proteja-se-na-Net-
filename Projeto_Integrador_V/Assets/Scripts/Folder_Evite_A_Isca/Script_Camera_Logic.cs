using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Script_Camera_Logic : MonoBehaviour
{

    public CinemachineVirtualCamera vcam;
    public CinemachineTrackedDolly dolly;

    public CinemachineSmoothPath path;

    public bool Had_To_Move;

    void Start()
    {
        Had_To_Move = false;
        dolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    void Update()
    {
        if (Had_To_Move)
        {
            dolly.m_PathPosition += Time.deltaTime * 1.0f;
        }
        else
        {
            dolly.m_PathPosition = 0f;


            Just_One_Waypoint();
        }
    }

    public void Create_Waypoint(Vector3 pos)
    {
        int Current_Paths = path.m_Waypoints.Length;

        System.Array.Resize(ref path.m_Waypoints, Current_Paths + 1);

        path.m_Waypoints[Current_Paths] = new CinemachineSmoothPath.Waypoint
        {
            position = pos,
            roll = 0f
        };
    }

    void Just_One_Waypoint()
    {
        if (path.m_Waypoints.Length > 1)
        {
            CinemachineSmoothPath.Waypoint First = path.m_Waypoints[0];

            path.m_Waypoints = new CinemachineSmoothPath.Waypoint[1];
            path.m_Waypoints[0] = First;
        }
    }
}
