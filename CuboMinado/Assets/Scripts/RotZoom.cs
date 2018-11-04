﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotZoom : MonoBehaviour {

    public float rotSpeed, zoomSpeed;
	private float initZoom;
	private Camera cam;
	public void SetPos(Vector3 dimensoes, float distanceBetweenTiles){
		initZoom = -dimensoes.z*distanceBetweenTiles*1.5f;
		cam = Camera.main;
		cam.transform.position = new Vector3(0, 0, initZoom);
	}

	void Update () {
		Rotaciona();
		Zoom();

		if (Input.GetKeyDown(KeyCode.R))
			ResetRotation();
	}

    private void Rotaciona() {
        if (Input.GetMouseButton(1)){
			Vector2 rot = new Vector2(
				Input.GetAxis("Mouse X") * rotSpeed,
				Input.GetAxis("Mouse Y") * rotSpeed
			);

			transform.RotateAround(Vector3.down, rot.x);
			transform.RotateAround(Vector3.right, rot.y);
		}
    }

	public void ResetRotation(){
		transform.rotation = Quaternion.identity;
		cam.transform.position = new Vector3(0, 0, initZoom);
	}

	private void Zoom(){
		cam.transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel")*zoomSpeed);
	}


}
