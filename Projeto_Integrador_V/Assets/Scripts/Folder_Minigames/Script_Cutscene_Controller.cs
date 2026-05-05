using UnityEngine;
using UnityEngine.Video;

public class Script_Cutscene_Controller : MonoBehaviour
{
    //A cutscene que aparecer� na tela
    public VideoPlayer Video_Player;

    //O script do minigame da cena
    public MonoBehaviour Minigame_Script;


    void Start()
    {
        //O minigame n�o deve come�ar ainda
        Minigame_Script.enabled = false;

        //A cutscene come�a
        Video_Player.Play();

        //Quando o v�deo eventualmente chegar ao fim, ir� chama a fun��o abaixo
        Video_Player.loopPointReached += Video_Has_Ended;
    }

    
    void Video_Has_Ended(VideoPlayer v)
    {
        //O minigame come�a
        Minigame_Script.enabled = true;

        //O gameObject da cutscene (Todo o canva) � desativado.
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

                //O v�deo para, o minigame come�a e este canva � desativado.
                Video_Player.Stop();
                Minigame_Script.enabled = true;

                gameObject.SetActive(false);
            }
        }
    }
}
