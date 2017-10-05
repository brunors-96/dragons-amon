using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb2d;
    public float velocidade;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
    }

    private void Movimento()
    {
        rb2d.velocity = new Vector2(velocidade, rb2d.velocity.y);
        animator.SetBool("Walk", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Virar")
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            velocidade = -velocidade;
        }
    }
}
