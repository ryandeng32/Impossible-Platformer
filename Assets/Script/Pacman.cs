using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour {

	// set move speed
	public float moveSpeed = 1.5f;
	private Rigidbody pacman;
	// set stopping point for object to go left and right
	private Vector3 LeftStop = new Vector3 (-9, float.Parse("3.6"), 0);
	private Vector3 RightStop = new Vector3 (-5, float.Parse("3.6"), 0);
	int check = 0;


	// Use this for initialization
	void Start () {
		pacman = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		// use check to make object go between left and right
		if (check == 0) {
			transform.position = Vector3.MoveTowards (transform.position, LeftStop, moveSpeed * Time.deltaTime);
			if (transform.position == LeftStop){
				check = 1;}
		}
		if (check == 1) {
			transform.position = Vector3.MoveTowards (transform.position, RightStop, moveSpeed * Time.deltaTime);
			if (transform.position == RightStop){
				check = 0;}
		}
	}
}





