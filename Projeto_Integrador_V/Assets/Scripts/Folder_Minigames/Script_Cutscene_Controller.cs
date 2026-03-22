using UnityEngine;
using UnityEngine.Video;

public class Script_Cutscene_Controller : MonoBehaviour
{
    //A cutscene que aparecerá na tela
    public VideoPlayer Video_Player;

    //O script do minigame da cena
    public MonoBehaviour Minigame_Script;


    void Start()
    {
        //O minigame não deve começar ainda
        Minigame_Script.enabled = false;

        //A cutscene começa
        Video_Player.Play();

        //Quando o vídeo eventualmente chegar ao fim, irá chama a função abaixo
        Video_Player.loopPointReached += Video_Has_Ended;
    }

    
    void Video_Has_Ended(VideoPlayer v)
    {
        //O minigame começa
        Minigame_Script.enabled = true;

        //O gameObject da cutscene (Todo o canva) é desativado.
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Touching_Screen();
    }

    void Touching_Screen()
    {
        //Quando o jogador toar na tela...
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Began)
            {

                //O vídeo para, o minigame começa e este canva é desativado.
                Video_Player.Stop();
                Minigame_Script.enabled = true;

                gameObject.SetActive(false);
            }
        }
    }
}
