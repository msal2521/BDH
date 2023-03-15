using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    AudioClip clip_1;
    AudioClip clip_2;
    AudioClip clip_3;

    AudioSource AS; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void bounce()
    {
        AS.clip = clip_2;
        AS.Play(); 

    }
}
