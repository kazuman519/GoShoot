﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string name = "homura";
	public float  turmPower = 3000;
	
	float attackConunt = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		turmPower -= Time.deltaTime * 100;
		print ("TurntPower:" + turmPower);

		if (attackConunt > 0) {
			attackConunt-= Time.deltaTime;
		}

		if (turmPower <= 0) {
			transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}
	}

	void OnCollisionEnter(Collision col){
		print("col_enter:"+col.gameObject.tag); 

		if (col.gameObject.tag.Equals("monster")) {
			print ("モンスターです");
			Monster monster = col.gameObject.GetComponent<Monster>();
			float attackPower = 5000;
			if (attackConunt > 0) {
				attackPower *= 2;
			}
			monster.GetComponent<Rigidbody>().AddForce(new Vector3(1500,attackPower,1500));
			monster.turmPower -= attackPower/50;
		}
	}

	public void attackAction() {
		attackConunt = 2.0f;
		GetComponent<Rigidbody>().AddForce(new Vector3(1500,0,1500));
	}
}
