using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
		print ("name:"+player.name);
	}
	
	// Update is called once per frame
	void Update () {
		print ("turnPower:" + player.turmPower);
		player.transform.Rotate (Vector3.up, player.turmPower * Time.deltaTime);
	}
}
