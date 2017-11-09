﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchControlsScript : MonoBehaviour
{

	public RTSControllerScript selectControl;
	public DragMoveController cameraControl;

	public MoveMarkerScript markerObject;

	public float heightOffset = 5f;

	public List<GameObject> playerPortraits = new List<GameObject> ();

	Image image;
	Camera cam;

	void Start ()
	{
		image = GetComponent<Image> ();

		cam = FindObjectOfType<Camera> ();

		selectControl.enabled = false;
	}

	void Update ()
	{
		
	}

	public void ChangeControl ()
	{
		if (!GameSettings.instance.isPaused) {

			if (selectControl.enabled) {
			
				//Debug.Log ("Camera Drag Enabled");

				image.color = Color.yellow;

				cameraControl.enabled = true;
				selectControl.enabled = false;

				markerObject.gameObject.SetActive (false);

				GameObject[] gos = GameObject.FindGameObjectsWithTag ("Player");

				if (gos.Length > 0) {
					for (int i = 0; i < gos.Length; i++) {

						selectControl.units.Remove (gos [i].transform);
						gos [i].transform.GetChild (0).gameObject.SetActive (false);
					}
				}

				if (playerPortraits.Count > 0) {
					for (int i = 0; i < playerPortraits.Count; i++) {

						playerPortraits [i].SetActive (false);
					}
				}

			} else if (cameraControl.enabled) {
			
				//Debug.Log ("Box Selection Enabled");

				image.color = Color.cyan;

				cameraControl.enabled = false;
				selectControl.enabled = true;

				markerObject.gameObject.SetActive (true);

				if (playerPortraits.Count > 0) {
					for (int i = 0; i < playerPortraits.Count; i++) {

						playerPortraits [i].SetActive (true);
					}
				}
			}
		}
	}

	public void IncreaseCameraHeight ()
	{
		if (!GameSettings.instance.isPaused) {
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y + Mathf.Abs (heightOffset), cam.transform.position.z);
		}
	}

	public void DecreaseCameraHeight ()
	{
		if (!GameSettings.instance.isPaused) {
			cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y - Mathf.Abs (heightOffset), cam.transform.position.z);
		}
	}
}
