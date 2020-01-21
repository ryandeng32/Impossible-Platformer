using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	// set move speed
	public float moveSpeed = 3f;
	private Rigidbody fireball;
	// set stopping point for object to go left and right
	private Vector3 TopStop = new Vector3 (-6, 2, 0);
	private Vector3 ButtomStop = new Vector3 (-6, -5, 0);
	int check = 0;


	// Use this for initialization
	void Start () {
		fireball = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		// use to check to make the object go between left and right
		if (check == 0) {
			transform.position = Vector3.MoveTowards (transform.position, TopStop, moveSpeed * Time.deltaTime);
			if (transform.position == TopStop){
				check = 1;}
		}
		if (check == 1) {
			transform.position = Vector3.MoveTowards (transform.position, ButtomStop, moveSpeed * Time.deltaTime);
			if (transform.position == ButtomStop){
				check = 0;}
	}
}
}
