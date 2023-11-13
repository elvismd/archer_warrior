using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 12f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;   
    }
}
