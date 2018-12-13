using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Damage_collisions : MonoBehaviour {

    //public GameObject HealBox;
    public GameObject DamageParticlesPrefab;
    public Slider Healthbar;
    //private Vector3 HealBoxStarPos;
    public bool IsPlayerDead = false;
    //bool HealBoxPickedUp = false;
    private Vector3 StartPos;
    private Quaternion StartRot;

    bool damageable = true;


    void Start () {

        StartPos = transform.position;
        StartRot = transform.rotation;
    }
    //RepawnFunc
    void Respawn()
    {
        //transform.position = StartPos;
        //transform.rotation = StartRot;
        //  GetComponent<Animator>().Play("LOSE00", -1, 0f);
        IsPlayerDead = true;
        GetComponent<Animator>().SetBool("IsDead", IsPlayerDead);
        

    }
    //PARTICLES
    void SpawnParticles()
    {
        Vector3 DamageParticlesLoc = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
        Quaternion rotation = this.transform.rotation;
        GameObject clone;
        clone = Instantiate(DamageParticlesPrefab, DamageParticlesLoc, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "hit" && damageable)
        {
			Healthbar.value -= 0.05f;
            SpawnParticles();
            // GetComponent<Animator>().Play("DAMAGED01");

            if (col.gameObject.transform.parent.tag == "Orc")
            {
                Debug.Log("its an orc!");
                col.gameObject.transform.parent.GetComponent<Orc>().TakeRecoilDamage();
            }
            //damageable = false;
        }
        else if (col.gameObject.tag == "heal")
		{
			Destroy (col.gameObject);
			//HealBoxPickedUp = true;
			//col.gameObject.SetActive(false);
		    //Invoke("RandomHealBox", 3);
			Healthbar.value += .30f;
		}
    }

    void OnTriggerStay(Collider stay_col)
    {
        if (stay_col.gameObject.tag == "damage")
        {
            Healthbar.value -= .011f;
        }
    }

    void OnCollisionExit(Collision col)
    {
        //damageable = true;
    }



	//void RandomHealBox()
	//{
	//	Vector3 position = new Vector3(Random.Range(-5.0F, 5.0F), 0, Random.Range(-5.0F, 5.0F));
	//	Instantiate(HealBox, position, Quaternion.identity);
	//}


    // Update is called once per frame
    void Update () {
        if (Healthbar.value <= 0.03)
        {
            Respawn();
            if (Input.GetKeyDown("up"))
            {
                IsPlayerDead = false;
                GetComponent<Animator>().SetBool("IsDead", IsPlayerDead);
                transform.position = StartPos;
                transform.rotation = StartRot;
                Healthbar.value = 0.2f;
            }

        }


    }

}
