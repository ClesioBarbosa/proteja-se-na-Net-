using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MandarPraOutraTela : MonoBehaviour
{
    [SerializeField] Script_Tutorial_Scene Tutorial_Scene;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter_Minigame(string Minigame_Scene)
    {
        Tutorial_Scene.Minigame_Scene = Minigame_Scene;
        SceneManager.LoadScene(Minigame_Scene);
    }
}
