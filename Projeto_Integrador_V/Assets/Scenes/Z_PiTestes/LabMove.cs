using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LabMove : MonoBehaviour
{
    Gyroscope gyro;
    bool gyroEnabled;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
            yield return new WaitForSeconds(1f);
            gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            gyro = Input.gyro;

            return true;
        }
            return false;
    }

     private void Update()
    {
        if (gyroEnabled) {GirarGiros();}
        else Debug.Log("NÃO");
    }

    void GirarGiros()
    {
        gameObject.transform.rotation  = gyro.attitude;
    }
}
