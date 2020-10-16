using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class VidaController : MonoBehaviour
{
    private int vida = 3;

    public Text Vida;
    
    public int GetVida()
    {
        return vida;
    }

    public void QuitarVida(int vida)
    {
        this.vida -= vida;
        Vida.text = "Vidas: " + GetVida();
    }
}
