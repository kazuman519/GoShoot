using UnityEngine;
using System.Collections; 



public class CameraController : MonoBehaviour {

	public Camera mainCamera;
	public Camera charactorCamera;

	// Use this for initialization
	void Start () {
		print ("CameraController Start");
		mainCamera.enabled =  false;
		charactorCamera.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetMouseButtonDown(0)) {
			if (mainCamera.enabled) {
				mainCamera.enabled = false;
				charactorCamera.enabled = true;
			} else {
				mainCamera.enabled = true;
				charactorCamera.enabled = false;
			}
		}
		*/
	}
}
