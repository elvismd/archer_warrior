using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // A unique number to differentiate between characters
    [SerializeField] private int series = 0;

    [SerializeField] private Animator animator;
    [SerializeField] private BowManager bowManager;

    public Animator Animator => animator;
    public BowManager BowManager => bowManager;
    public int Series => series;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
