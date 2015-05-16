using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	 
	public Player player;
	public Monster monster;

	bool isBattoleStart;

	// Use this for initialization
	void Start () {
		print ("name:"+player.name);
		freezeRotationY (player);
		isBattoleStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!isBattoleStart) {
				isBattoleStart = true;
				releaseFreezeRotationY (player);
			}
		}

		if (isBattoleStart) {
			player.transform.Rotate (Vector3.up, player.turmPower * Time.deltaTime);
			monster.transform.Rotate (Vector3.up, monster.turmPower * Time.deltaTime);
		}
	}

	void freezeRotationY(Player gameObject) {
		gameObject.GetComponent<Rigidbody>().constraints =
			RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationZ |
				RigidbodyConstraints.FreezePositionY;
	}

	void releaseFreezeRotationY(Player gameObject) {
		gameObject.GetComponent<Rigidbody>().constraints =
			RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationZ;
	}
}
