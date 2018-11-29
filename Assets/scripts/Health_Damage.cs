using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Damage : MonoBehaviour {

	public Slider Healthbar;
	private bool IsGameOver = false;






    // Use this for initialization
    void Start () {

	}

	private void OnCollisionEnter(Collision col)
	{
        if (col.gameObject.tag == "hit")
        {
            Debug.Log("hitted");
			Healthbar.value -= .05f;
            
            //GetComponent<Animator>().Play("DAMAGED01");
        }
        else if (col.gameObject.tag == "heal")
		{
			Healthbar.value += .30f;
			Destroy (col.gameObject);
		}
	}

	void OnTriggerStay(Collider stay_col)
	{
		if (stay_col.gameObject.tag == "damage")
		{
			Healthbar.value -= .011f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
