using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_Touch_And_Hold : MonoBehaviour
{
    Slider slider;
    public Script_Camera_Fade Fade;
    bool Stop_Changing;

    void Start()
    {
        Stop_Changing = false;
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Stationary || Getting_Touch.phase == TouchPhase.Moved)
            {
                slider.value += Time.deltaTime;

                if(slider.value == 1f && Fade.Ready)
                {
                    Stop_Changing = true;
                    Fade.Fade_Out = true;
                }
            }
            
        }
        else if(!Stop_Changing)
        {
            slider.value -= 0.5f * Time.deltaTime;
        }
    }
}
