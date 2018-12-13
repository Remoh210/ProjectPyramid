using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject skeleton; // the skeleton prefab
    public GameObject orc; // the orc prefab

    float orcTimer = 0.0f;
    float skeleTimer = 0.0f;
    float skeleCheck = 20;
    float orcCheck = 5;

    int prevScore = 0;
    int orcScore = 0;
    int skeleScore = 0;
    int newScore = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        newScore = GameObject.Find("Score").GetComponent<ScoreManager>().GetScore(); //get the score
        orcScore += newScore - prevScore;
        skeleScore += newScore - prevScore;
        prevScore = newScore;

        orcTimer += Time.deltaTime;
        skeleTimer += Time.deltaTime;

        if(orcTimer > orcCheck)
        {
            GameObject newOrc = Instantiate(orc);
            newOrc.transform.position = this.transform.position;
            newOrc.GetComponent<Orc>().player = GameObject.Find("Necromancer").transform;
            orcTimer = 0.0f;
        }

        if (skeleTimer > skeleCheck)
        {
            GameObject newSkeleton = Instantiate(skeleton);
            newSkeleton.transform.position = this.transform.position;
            newSkeleton.GetComponent<Enemy>().player = GameObject.Find("Necromancer").transform;
            skeleTimer = 0.0f;
        }

        if(orcScore - (orcScore % 25) > 0) //decrease time limit for orc spawns if score is high enough
        {
            orcCheck *= 0.9f;
            orcScore = 0;
        }

        if (skeleScore - (skeleScore % 50) > 0) //decrease time limit for skeleton spawns if score is high enough
        {
            skeleCheck *= 0.9f;
            skeleScore = 0;
        }
    }
}
