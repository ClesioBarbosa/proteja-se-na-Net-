using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Script_Tutorial_Scene : MonoBehaviour
{


    //Textos em tela
    [SerializeField] public TextMeshProUGUI Minigame_Name_Text, Title_Name_Text, Explanation_Text;

    public string Minigame_Name, Title_Name, Explanation;

    public Image Background_Image;

    public Script_Camera_Fade Fade;

    private void Start()
    {
        Minigame_Name_Text.text = Minigame_Name;
        Title_Name_Text.text = Title_Name;
        Explanation_Text.text = Explanation;

        Fade.Ready = false;
        Fade.Fade_In = true;

    }

    void Update()
    {
        //Se for realizado o toque na tela...
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Began && Fade.Ready)
            {
                Fade.Fade_Out = true;
            }
        }
    }
}