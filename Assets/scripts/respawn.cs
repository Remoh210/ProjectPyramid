using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class respawn : MonoBehaviour
{

   // private bool IsGameOver = false;

    private Vector3 StartPos;
    private Quaternion StartRot;

    public Slider Healthbar;

    //RepawnFunc
    void Respawn()
    {
        transform.position = StartPos;
        transform.rotation = StartRot;
        GetComponent<Animator>().Play("LOSE00", -1, 0f);
		Healthbar.value = 1;
    }

    // Use this for initialization
    void Start()
    {

        StartPos = transform.position;
        StartRot = transform.rotation;

    }


    void OnCollisionEnter(Collision col_death)
    {
        //Fall Death
        if (col_death.gameObject.tag == "death")
        {
            Respawn();
		}
    }




    //Health Damge Controll
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "damege" && Healthbar.value > 0)
        {
            Healthbar.value -= .011f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Healthbar.value == 0)
        {
            Respawn();
        }
    }
}