using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Script_Cutscene_Controller : MonoBehaviour
{
    //A cutscene que aparecer� na tela
    public VideoPlayer Video_Player;
    public RawImage image;

    public Image Skip_Image;
    Color Less_Image;

    bool Fade_In;
    bool Ready;

    Slider slider;
    public Script_Camera_Fade Fade;
    bool Stop_Changing;

    Coroutine Skipping;

    void Start()
    {
        Skipping = null;

        Less_Image = image.color;

        slider = GetComponent<Slider>();
        Stop_Changing = false;

        slider.value = 0f;

        //A cutscene come�a
        Video_Player.Play();

        //Quando o v�deo eventualmente chegar ao fim, ir� chama a fun��o abaixo
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
        if (Fade_In && image.color.a > 0f)
        {
            Less_Image.a -= 1f * Time.deltaTime;
            image.color = Less_Image;
            Skip_Image.color = Less_Image;

            if (image.color.a <= 0.05f)
            {
                Less_Image.a = 0;
                image.color = Less_Image;
                Skip_Image.color = Less_Image;
                Ready = true;
            }
        }
    }

    void Touching_Screen()
    {
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Stationary || Getting_Touch.phase == TouchPhase.Moved)
            {
                slider.value += Time.deltaTime;

                if (slider.value == 1f)
                {
                    Stop_Changing = true;

                    if(Skipping == null)
                    {
                        Skipping = StartCoroutine(While_Is_Dark());
                    }
                }
            }

        }
        else if (!Stop_Changing)
        {
            slider.value -= 0.5f * Time.deltaTime;
        }
    }

    public void Ending_Cutscene()
    {
        //O v�deo para, o minigame come�a e este canva � desativado.

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
