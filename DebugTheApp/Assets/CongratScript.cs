﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratScript : MonoBehaviour
{
    public TextMesh text;
    public ParticleSystem sparksParticles;

    private List<string> textToDisplay;

    private float rotatingSpeed;
    private float timeToNextText;

    private int currentText;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextText = 0.0f;
        currentText = 0;

        rotatingSpeed = 1.0f;

        textToDisplay = new List<string>();
        textToDisplay.Add("Congratulation");
        textToDisplay.Add("All Errors Fixed");

        text.text = textToDisplay[0];

        sparksParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextText += Time.deltaTime;
        print(timeToNextText);
        if (timeToNextText > 1.5f)
        {
            timeToNextText = 0.0f;

            currentText++;
            if (currentText < textToDisplay.Count)
            {
                text.text = textToDisplay[currentText];
            }
        }
    }
}