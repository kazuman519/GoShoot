﻿using UnityEngine;
using System.Collections; 



public class CameraController : MonoBehaviour {

	public Camera mainCamera;
	public Camera charactorCamera;

	// Use this for initialization
	void Start () {
		print ("CameraController Start");
		mainCamera.enabled = true;
		charactorCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (mainCamera.enabled) {
				mainCamera.enabled = false;
				charactorCamera.enabled = true;
			} else {
				mainCamera.enabled = true;
				charactorCamera.enabled = false;
			}
		}
	}
}
