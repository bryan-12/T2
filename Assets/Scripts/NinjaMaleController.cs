using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NinjaMaleController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform _transform;

    private const int ANIMATION_QUIETO = 0;
    private const int ANIMATION_CORRER = 1;
    private const int ANIMATION_SALTAR = 2;
    private const int ANIMATION_LANZAR = 3;
    private const int ANIMATION_MORIR = 4;
    private const int ANIMATION_DESLIZAR = 5;
    private const int ANIMATION_ESCALAR = 6;
    private const int ANIMATION_PLANEAR = 7;

    public float velocity = 10f;
    public float jumpForce = 30f;
    public int numSaltos = 0;
    private int vidas = 3;

    public GameObject Kunai;
    public GameObject leftKunai;

    public VidaController Vida;
    public PuntajeController Puntaje;
    

    private bool trepar = false;
    private bool muerto = false;
    private bool planear = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();

    }

    void Update()
    {
        if (muerto) return;
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetInteger("Estado", ANIMATION_QUIETO);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
            animator.SetInteger("Estado", ANIMATION_CORRER);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
            animator.SetInteger("Estado", ANIMATION_CORRER);
            sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && numSaltos < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            numSaltos++;
            animator.SetInteger("Estado", ANIMATION_SALTAR);


        }

        if (Input.GetKey(KeyCode.C))
        {
            animator.SetInteger("Estado", ANIMATION_DESLIZAR);

        }

        

        if (Input.GetKeyUp(KeyCode.X))
        {
            if (!sr.flipX)
            {
                var kunaiposition = new Vector3(_transform.position.x + 2f, _transform.position.y, _transform.position.z);
                Instantiate(Kunai, kunaiposition, Quaternion.identity);

            }
            else
            {
                var leftkunaiposition = new Vector3(_transform.position.x - 2f, _transform.position.y, _transform.position.z);
                Instantiate(leftKunai, leftkunaiposition, Quaternion.identity);
            }
            animator.SetInteger("Estado", ANIMATION_LANZAR);
           
        }

        if (trepar)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            animator.SetInteger("Estado", ANIMATION_ESCALAR);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, velocity);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, -velocity);
            }
        }
        if (!trepar)
            rb.gravityScale = 10;
        ///////////////////////////////////////////
        if (Input.GetKey(KeyCode.V) && planear)
        {
            rb.gravityScale = 1;
            numSaltos = 2;
            animator.SetInteger("Estado", ANIMATION_PLANEAR);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(velocity, -velocity);
                sr.flipX = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-velocity, -velocity);
                sr.flipX = true;
            }
        }

        Caida();
    }

    private void Caida()
    {
        if (rb.velocity.y < -60)
        {
            muerto = true;
            animator.SetInteger("Estado", ANIMATION_MORIR);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemigo")
        {
            vidas--;
            if (vidas == 0) muerto = true;
            animator.SetInteger("Estado", ANIMATION_MORIR);
            if (vidas >= 0)
            {
                
                Vida.QuitarVida(1);
                Debug.Log(Vida.GetVida());
            }
        }


        if (other.gameObject.layer == 8)
        {
            numSaltos = 0;
            planear = false;
        }


        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Escalera")
        {
            trepar = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Escalera")
        {
            trepar = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
        {
            planear = true;
        }
    }
}
