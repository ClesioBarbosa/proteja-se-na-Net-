using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class LabMove : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float suavidade = 5f;
    [SerializeField] private float deadzone = 0.05f; // zona neutra
    
    private Vector2 move;

    // opções de debug
    [SerializeField] TMP_InputField[] mudarVari;
    [SerializeField] TMP_Text tiltText;

    void Start()
    {
        mudarVari[0].text = $"{velocidade}";
        mudarVari[1].text = $"{suavidade}";
        mudarVari[2].text = $"{deadzone}";
    }

    void Update()
    {
        Vector2 tilt;
        // Ajuste para Landscape Left
        //tilt = new Vector2(-Input.acceleration.y, Input.acceleration.x);
        tilt = new Vector2(Input.acceleration.x, Input.acceleration.y);
        tiltText.text = $"Tilt: {tilt}";
        TesteUI();

        // Deadzone (para parar no neutro)
        //if (Mathf.Abs(tilt.x) < deadzone) tilt.x = 0;
        //if (Mathf.Abs(tilt.y) < deadzone) tilt.y = 0;

        // Suavização correta
        move.x = Mathf.Lerp(move.x, tilt.x, Time.deltaTime * suavidade);

        // Movimento no plano XZ
        Vector3 movement = new Vector3(move.x, 0, move.y);
        transform.Translate(movement * velocidade * Time.deltaTime, Space.World);
        
    }

    void TesteUI()
    {
        float velocidadeNova, suavidadeNova, deadzoneNova;
        float.TryParse(mudarVari[0].text, out velocidadeNova);
        float.TryParse(mudarVari[1].text, out suavidadeNova);
        float.TryParse(mudarVari[1].text, out deadzoneNova);

        velocidade = velocidadeNova;
        suavidade = suavidadeNova;
        deadzone = deadzoneNova;
    }
}
