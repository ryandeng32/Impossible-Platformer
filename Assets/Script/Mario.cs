using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour {

	// set move speed
	public float moveSpeed = 3f;
	private Rigidbody mario;
	// set stopping point for object
	private Vector3 LeftStop = new Vector3 (8, float.Parse("3.3"), 0);
	private Vector3 RightStop = new Vector3 (11, float.Parse("3.3"), 0);
	int check = 0;


	// Use this for initialization
	void Start () {
		mario = GetComponent<Rigidbody> ();
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





