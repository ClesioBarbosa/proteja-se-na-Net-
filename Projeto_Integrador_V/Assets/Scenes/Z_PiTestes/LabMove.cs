using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LabMove : MonoBehaviour
{
   private Quaternion rotacaoInicial;
    private bool giroscopioAtivo;

    [Header("Configurações")]
    [SerializeField] float suavizacao = 5f;   // quanto maior, mais suave
    [SerializeField] float limiteRotacao = 30f; // limite de inclinação

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            giroscopioAtivo = true;

            rotacaoInicial = transform.rotation;
        }
        else
        {
            giroscopioAtivo = false;
            Debug.Log("Giroscópio não suportado.");
        }
    }

    void FixedUpdate()
    {
        if (!giroscopioAtivo) return;

        // Pegando rotação do celular
        Quaternion gyro = Input.gyro.attitude;

        // Convertendo para o sistema da Unity
        Quaternion rotacao = new Quaternion(gyro.x, gyro.y, -gyro.z, -gyro.w);

        // Aplicando limites (evita girar demais)
        Vector3 angulos = rotacao.eulerAngles;

        angulos.x = LimitarAngulo(angulos.x, limiteRotacao);
        angulos.y = 0; // trava eixo Y
        angulos.z = LimitarAngulo(angulos.z, limiteRotacao);

        Quaternion rotacaoFinal = Quaternion.Euler(angulos);

        // Suaviza o movimento
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            rotacaoInicial * rotacaoFinal,
            Time.deltaTime * suavizacao
        );
    }

    float LimitarAngulo(float angulo, float limite)
    {
        if (angulo > 180) angulo -= 360;
        return Mathf.Clamp(angulo, -limite, limite);
    }
}
