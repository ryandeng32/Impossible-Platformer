using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedPlatform : MonoBehaviour {
	// set move speed
	public float moveSpeed = 3.5f;
	private Rigidbody sp;
	// set stopping point from left to right 
	private Vector3 LeftStop = new Vector3 (-4, 1, 0);
	private Vector3 RightStop = new Vector3 (5, 1, 0);
	int check = 0;

	// different collider used for a platform top and a spiked buttom
	void OnCollisionEnter2D (Collision2D hit)
	{
		
		if (hit.collider.GetType () == typeof(BoxCollider2D)) {
			print ("front hit or back");
		}else if (hit.collider.GetType() == typeof(CapsuleCollider2D)){
			Destroy(hit.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		sp = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		// use check to switch between left and right
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


