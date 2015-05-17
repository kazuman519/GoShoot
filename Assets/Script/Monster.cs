using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public string name = "slime";
	public float  turmPower = 4000;
	
	float attackConunt = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		turmPower -= Time.deltaTime * 10;

		if (attackConunt > 0) {
			attackConunt-= Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision col){
		print("col_enter:"+col.gameObject.tag); 
		
		if (col.gameObject.tag.Equals("Player")) {
			print ("プレイヤーです");

			Player player = col.gameObject.GetComponent<Player>();

			float attackPower = 5000;
			if (attackConunt > 0) {
				attackPower *= 2;
			}
			player.GetComponent<Rigidbody>().AddForce(new Vector3(1500,attackPower,1500));
			player.turmPower -= attackPower/10;
		} 	
	}

	public void attackAction() {
		attackConunt = 3.0f;
	}
}
