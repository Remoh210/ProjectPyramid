﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    //int score = 500;
    int score = 0;

    float deltaTime = 0.0f;

    Image blackscreen;

    GameObject textbox;

	// Use this for initialization
	void Start () {
        textbox = GameObject.Find("Score");
        deltaTime = 0.0f;
        blackscreen = this.gameObject.transform.parent.Find("BlackScreen").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if(score >= 0)
        {
            textbox.GetComponent<Text>().text = "Score: " + score.ToString();
            //textbox.GetComponent<Text>().text = "Year: " + score.ToString() + " AD";
        }
        else
        {
            //textbox.GetComponent<Text>().text = "Year: " + Mathf.Abs(score).ToString() + " BC";
        }
        //deltaTime += 0.1f;
        deltaTime += 0.2f;
        if(deltaTime > 1.0f)
        {
            //score -= (int)deltaTime;
            if(!GameObject.Find("Necromancer").GetComponent<Health_Damage_collisions>().IsPlayerDead)
            {
                score += (int)deltaTime;
            }
            deltaTime = 0.0f;
        }
	}

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }
}
