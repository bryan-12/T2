using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PuntajeController : MonoBehaviour
{  
    private int puntaje = 0;

    public Text PuntajeText;

    public int GetPuntos()
    {
        return puntaje;
    }

    public void AddPuntos(int puntaje)
    {
        this.puntaje += puntaje;
        PuntajeText.text = "Puntaje: " + GetPuntos();
    }

}
