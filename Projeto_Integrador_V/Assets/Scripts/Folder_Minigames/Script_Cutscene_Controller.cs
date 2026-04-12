using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Script_Cutscene_Controller : MonoBehaviour
{
    //A cutscene que aparecerá na tela
    public VideoPlayer Video_Player;


    bool Fade_In;
    bool Ready;

    public Script_Camera_Fade Fade;

    void Start()
    {

        //A cutscene começa
        Video_Player.Play();

        //Quando o vídeo eventualmente chegar ao fim, irá chama a função abaixo
        Video_Player.loopPointReached += Video_Has_Ended;
    }

    
    void Video_Has_Ended(VideoPlayer v)
    {
        Ending_Cutscene();
    }

    private void Update()
    {
        Touching_Screen();

        F_In();
        
    }

    void F_In()
    {
        if (Fade_In && Video_Player.targetCameraAlpha > 0f)
        {
            Video_Player.targetCameraAlpha -= 2f * Time.deltaTime;

            if (Video_Player.targetCameraAlpha <= 0f)
            {
                Video_Player.targetCameraAlpha = 0f;
                Ready = true;
            }
        }
    }

    void Touching_Screen()
    {
        //Quando o jogador toar na tela...
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Began)
            {
                
                StartCoroutine(While_Is_Dark());
            }
        }
    }

    public void Ending_Cutscene()
    {
        //O vídeo para, o minigame começa e este canva é desativado.

        Video_Player.Stop();
        Fade.Dramatic_Fade_In = true;
        Destroy(gameObject);
    }

    public IEnumerator While_Is_Dark()
    {

        Fade_In = true;
        

        yield return new WaitUntil(() => Ready);

        Ending_Cutscene();

    }
}
