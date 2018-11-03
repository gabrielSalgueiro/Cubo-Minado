using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberRot : MonoBehaviour {

	private Camera Cam;

	// Use this for initialization
	void Start () {
		Cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Cam.transform);
	}
}
