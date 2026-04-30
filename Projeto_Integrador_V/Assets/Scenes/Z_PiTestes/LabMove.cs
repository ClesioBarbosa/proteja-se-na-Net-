using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LabMove : MonoBehaviour
{
    public float speed = 5f;
    public float smooth = 5f;

    private Vector2 move;

    void Update()
    {
        // Captura a inclinação lateral
        Vector2 tilt = new Vector2(Input.acceleration.x,Input.acceleration.y);

        // Suaviza o movimento (evita tremedeira)
        move.x = Mathf.Lerp(move.x, tilt.x, Time.deltaTime * smooth);
        move.y = Mathf.Lerp(move.y, tilt.y, Time.deltaTime * smooth);

        // Move o personagem
        Vector3 movement = new Vector3(move.x, 0, move.y);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
