﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    int score = 500;

    float deltaTime = 0.0f;

    GameObject textbox;

	// Use this for initialization
	void Start () {
        textbox = GameObject.Find("Score");
        deltaTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if(score >= 0)
        {
            textbox.GetComponent<Text>().text = "Year: " + score.ToString() + " AD";
        }
        else
        {
            textbox.GetComponent<Text>().text = "Year: " + Mathf.Abs(score).ToString() + " BC";
        }
        deltaTime += 0.1f;
        if(deltaTime > 1.0f)
        {
            score -= (int)deltaTime;
            deltaTime = 0.0f;
        }
	}
}
