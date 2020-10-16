using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersonajeController : MonoBehaviour
{
    public float velocity = 10f;
    public float jumpForce = 30f;
    public GameObject Kunai;
    public GameObject leftKunai;

    public PuntajeController Puntaje;

    public AudioClip AudioSaltar;
    public AudioClip AudioLanzar;
    public AudioClip AudioMoneda;

    public int numSaltos = 0;


    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform _transform;
    private AudioSource _audioSource;


    private const int ANIMATION_QUIETO = 0;
    private const int ANIMATION_CORRER = 1;
    private const int ANIMATION_SALTAR = 2;
    private const int ANIMATION_LANZAR = 3;
    private const int ANIMATION_MORIR = 4;
    private const int ANIMATION_DESLIZAR = 5;

    private bool muerto = false;
    private bool coins = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _audioSource= GetComponent<AudioSource>();

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
        if(Input.GetKey(KeyCode.C))
        {
            animator.SetInteger("Estado", ANIMATION_DESLIZAR);
         
        }

        if (Input.GetKeyDown(KeyCode.Space)&&numSaltos<2)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            numSaltos++;
            animator.SetInteger("Estado", ANIMATION_SALTAR);
            _audioSource.PlayOneShot(AudioSaltar);

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
            _audioSource.PlayOneShot(AudioLanzar);
        }
        if (coins)
        {
            _audioSource.PlayOneShot(AudioMoneda);
            coins = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemigo")
        {
            animator.SetInteger("Estado", ANIMATION_MORIR);
            muerto = true;
        }
        
        if(other.gameObject.tag == "Monedas")
        {
            coins = true;
            Destroy(other.gameObject);
            _audioSource.PlayOneShot(AudioMoneda);
            Puntaje.AddPuntos(5);
            Debug.Log(Puntaje.GetPuntos());
        }

        if(other.gameObject.layer == 8)
        {
            numSaltos = 0;
        }

        if(other.gameObject.name == "ArrowSign")
        {
           SceneManager.LoadScene("megaman"); 
        }

        /*Enter2d (una vez) al colicionar con algo 
         Stated2d (varias veces) mientras este colicionando con algo
        Exit2d (una vez) al dejar de colicionar con algo*/

        if (other.gameObject.name == "Arbol")
        {
            Debug.Log("Colicion detectada");
        }
    }




}
