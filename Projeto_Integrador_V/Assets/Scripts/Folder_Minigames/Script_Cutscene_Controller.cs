using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Script_Cutscene_Controller : MonoBehaviour
{

    public VideoPlayer Video_Player;

    public MonoBehaviour Minigame_Script;


    void Start()
    {
        
        Minigame_Script.enabled = false;

        Video_Player.Play();

        Video_Player.loopPointReached += Video_Has_Ended;
    }

    void Video_Has_Ended(VideoPlayer v)
    {

        Minigame_Script.enabled = true;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        Touching_Screen();
    }

    void Touching_Screen()
    {
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Began)
            {

                Video_Player.Stop();
                Minigame_Script.enabled = true;

                gameObject.SetActive(false);
            }
        }
    }
}
