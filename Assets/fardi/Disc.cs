﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{

    public AudioSource theAudio;
    public LpPlayer loadedLP;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(loadedLP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool shouldStart()
    {
        return true;
    }
}
