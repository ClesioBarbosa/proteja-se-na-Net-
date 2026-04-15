using UnityEngine;

public class NPCVision : MonoBehaviour
{
    [Header("Configurações de Visão")]
    public float raioDeVisao = 10f;
    [Range(0, 180)]
    public float anguloDeVisao = 60f;

    [Header("Referência ao Player")]
    public Transform player;

    public bool playerDetectado = false;

    void Update()
    {
        if (player == null) return;

        Vector3 direcaoAoPlayer = player.position - transform.position;
        float distancia = direcaoAoPlayer.magnitude;

        // Verifica se o player está dentro do raio
        if (distancia <= raioDeVisao)
        {
            // Verifica se está dentro do ângulo de visão frontal
            float angulo = Vector3.Angle(transform.forward, direcaoAoPlayer.normalized);

            if (angulo <= anguloDeVisao / 2f)
            {
                if (!playerDetectado)
                {
                    Debug.Log("Player entrou na frente do NPC!");
                    playerDetectado = true;
                }
            }
            else
            {
                playerDetectado = false;
            }
        }
        else
        {
            playerDetectado = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualização do raio no editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioDeVisao);

        // Visualização do ângulo de visão
        Vector3 direcaoEsquerda = Quaternion.Euler(0, -anguloDeVisao / 2f, 0) * transform.forward;
        Vector3 direcaoDireita = Quaternion.Euler(0, anguloDeVisao / 2f, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direcaoEsquerda * raioDeVisao);
        Gizmos.DrawRay(transform.position, direcaoDireita * raioDeVisao);
    }
}
