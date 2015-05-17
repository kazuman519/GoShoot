using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string name = "homura";
	public float  turmPower = 5000f;
	public float  magicPower = 300f;

	public GameObject ropeBox;
	public GameObject ropePowerParticle;
	public GameObject magicWindParticle;

	float attackConunt = 0f;
	float magicCount = 0f;

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

		if (magicCount > 0) {
			magicCount -= Time.deltaTime;
		} else if (magicCount < 0) {
			magicCount = 0;
			magicWindParticle.SetActive(false);
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
			if (magicCount > 0) {
				attackPower *= 3;
			}
			monster.GetComponent<Rigidbody>().AddForce(new Vector3(1500,attackPower,1500));
			monster.turmPower -= attackPower/50;
		}
	}

	public void attackAction() {
		magicPower -= 20f;
		attackConunt = 2.0f;
		GetComponent<Rigidbody>().AddForce(new Vector3(1500,0,1500));
	}
		
	public void magicWind() {
		magicPower -= 150;
		magicWindParticle.SetActive (true);
		magicCount = 4.5f;
	}


}
