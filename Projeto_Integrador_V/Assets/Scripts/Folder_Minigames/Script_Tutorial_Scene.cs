using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Script_Tutorial_Scene : MonoBehaviour
{


    //Textos em tela
    [SerializeField] public TextMeshProUGUI Minigame_Name_Text, Title_Name_Text, Explanation_Text;

    //Nome da cena pra qual esse script irá mandar.
    [SerializeField] public string Minigame_Scene, Minigame_Name;

    public Image Background_Image;

    public VideoPlayer Controls_Video, Gameplay_Video;

    private void Start()
    {
        Minigame_Name_Text.text = Minigame_Name;

        switch (Minigame_Scene)
        {
            case "Evite a Isca":
                Background_Image.color = Color.blue;
                Minigame_Name = "Evite a Isca";
                Title_Name_Text.text = "Clique na porta correta!";
                Explanation_Text.text = "Observe o link correto acima das portas e clique apenas na porta que mostra o link igual.";
                //Controls_Video = ;
                //Gameplay_Video = ;
                break;
            case "Labirinto":
                Background_Image.color = Color.red;
                Minigame_Name = "Labirinto";
                Title_Name_Text.text = "Desbrave o labirinto!";
                Explanation_Text.text = "Faça alguma coisa no labirinto, não sei. Tenho que perguntar a João as regras desse minigame.";
                //Controls_Video = ;
                //Gameplay_Video = ;
                break;
            case "Minigame 3":
                Background_Image.color = Color.green;
                Minigame_Name = "O nome do minigame";
                Title_Name_Text.text = "Título chamativo pras regras!";
                Explanation_Text.text = "Explicação básica das regras do minigame";
                //Controls_Video = ;
                //Gameplay_Video = ;
                break;
        }
    }

    void Update()
    {
        //Se for realizado o toque na tela...
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Began)
            {
                //Chama a cena
                SceneManager.LoadScene(Minigame_Scene);
            }
        }
    }
}
