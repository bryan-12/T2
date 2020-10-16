using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroidController : MonoBehaviour
{
   
   /* void Start()
    {
        
    }
    void Update()
    {
        
    }*/

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "IceBox")
        {
            Destroy(other.gameObject);
        }
    }
}
