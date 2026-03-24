using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Tutorial_Scene : MonoBehaviour
{
    //Nome da cena pra qual esse script irá mandar.
    public string Minigame_Scene;


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
