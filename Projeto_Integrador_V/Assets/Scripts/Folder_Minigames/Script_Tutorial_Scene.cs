using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_Tutorial_Scene : MonoBehaviour
{


    //Textos em tela
    [SerializeField] public TextMeshProUGUI Minigame_Name_Text, Title_Name_Text, Explanation_Text;

    public string Minigame_Name, Title_Name, Explanation;

    public Image Background_Image;

    Slider slider;
    public Script_Camera_Fade Fade;
    bool Stop_Changing;

    private void Start()
    {
        slider = GetComponent<Slider>();
        Stop_Changing = false;

        Minigame_Name_Text.text = Minigame_Name;
        Title_Name_Text.text = Title_Name;
        Explanation_Text.text = Explanation;

        Fade.Ready = false;
        Fade.Fade_In = true;

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Stationary || Getting_Touch.phase == TouchPhase.Moved)
            {
                slider.value += Time.deltaTime;

                if (slider.value == 1f && Fade.Ready)
                {
                    Stop_Changing = true;
                    Fade.Fade_Out = true;
                }
            }

        }
        else if (!Stop_Changing)
        {
            slider.value -= 0.5f * Time.deltaTime;
        }
    }
}