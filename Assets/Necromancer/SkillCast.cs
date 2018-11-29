using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillCast : MonoBehaviour {

    public GameObject RangedSpellPrefab;
    // Use this for initialization

    void RangedSpell()
    {
        Vector3 spawnSpellLoc = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Quaternion rotation = this.transform.rotation;
        GameObject clone;
        clone = Instantiate(RangedSpellPrefab, spawnSpellLoc, this.transform.rotation);
        this.GetComponent<Animator>().Play("Magic Attack3");
        clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * 15;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space") && !this.GetComponent<Health_Damage_collisions>().IsPlayerDead)
        {
            RangedSpell();
        }
        //castSpell();

	}
}
