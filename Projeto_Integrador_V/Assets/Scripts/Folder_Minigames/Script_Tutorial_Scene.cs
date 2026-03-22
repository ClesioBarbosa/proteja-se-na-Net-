using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Script_Tutorial_Scene : MonoBehaviour
{
    public string Minigame_Scene;
    
    AsyncOperation loading;

    void Start()
    {
        StartCoroutine(Loading_Scene());
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch Getting_Touch = Input.GetTouch(0);

            if (Getting_Touch.phase == TouchPhase.Began)
            {

                SceneManager.LoadScene(Minigame_Scene);
            }
        }
    }

    IEnumerator Loading_Scene()
    {
        loading = SceneManager.LoadSceneAsync(Minigame_Scene);

        
        loading.allowSceneActivation = false;

        
        while (loading.progress < 0.9f)
        {
            Debug.Log("Carregando: " + loading.progress);
            yield return null;
        }

        Debug.Log("Cena pronta pra ativar!");
    }

    public void AtivarCena()
    {
        loading.allowSceneActivation = true;
    }
}
