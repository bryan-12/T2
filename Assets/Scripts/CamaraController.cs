using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    private Transform _transform;
    public GameObject Player;
    //public GameObject target; //para obtener la posicon o transform del otro componente, en este caso personaje

    private float x = 0f;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }


    void Update()
    {
        x = Player.transform.position.x;
        _transform.position = new Vector3(x, _transform.position.y, _transform.position.z);
    }
}
