using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public string name = "slime";
	public float  turmPower = 1000;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		print("col_enter:"+col.gameObject.tag); 
		
		if (col.gameObject.tag.Equals("Player")) {
			print ("プレイヤーです");
			col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1500,5000,1500));
		} 
		
	}
}
