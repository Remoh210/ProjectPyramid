using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    //int score = 500;
    int score = 0;

    float deltaTime = 0.0f;

    Image blackscreen;

    GameObject textbox;
    Text gameOver;

	// Use this for initialization
	void Start () {
        textbox = GameObject.Find("Score");
        deltaTime = 0.0f;
        blackscreen = this.gameObject.transform.parent.Find("BlackScreen").GetComponent<Image>();
        gameOver = GameObject.Find("GameOverText").GetComponent<Text>();
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

        GameObject player = GameObject.Find("Necromancer");
        if(player.GetComponent<Health_Damage_collisions>().IsPlayerDead)
        {
            if(blackscreen.color.a < 1.0f)
            {
                float alpha = blackscreen.color.a + (Time.deltaTime * 0.5f);
                if (alpha > 1.0f)
                    alpha = 1.0f;
                blackscreen.color = new Color(blackscreen.color.r, blackscreen.color.g, blackscreen.color.b, alpha);
            }

            if (blackscreen.color.a > 0.4f)
            {
                float alpha = gameOver.color.a + (Time.deltaTime * 0.25f);
                if (alpha > 1.0f)
                    alpha = 1.0f;
                gameOver.color = new Color(gameOver.color.r, gameOver.color.g, gameOver.color.b, alpha);
            }
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
