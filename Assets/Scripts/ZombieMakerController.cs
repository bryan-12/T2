using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using System;
using System.Collections.Specialized;


public class ZombieMakerController : MonoBehaviour
{
    public GameObject Zombie;
    private Transform _transform;
    private float tiempo = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo >= 8)
        {
            Instantiate(Zombie, _transform.position, Quaternion.identity);
            tiempo = 0;
        }
    }
}
