using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    public float velocityX = 10f;

    private PuntajeController puntajeControllerK;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 8);
        puntajeControllerK = FindObjectOfType<PuntajeController>();
        Debug.Log(puntajeControllerK.GetPuntos());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemigo")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            puntajeControllerK.AddPuntos(10);
            Debug.Log(puntajeControllerK.GetPuntos());
        }
    }
}
