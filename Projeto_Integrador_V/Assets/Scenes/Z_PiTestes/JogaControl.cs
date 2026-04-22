using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogaControl : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float v;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity=new Vector3( dir.x * v,rb.velocity.y, dir.y * v);
    }
}
