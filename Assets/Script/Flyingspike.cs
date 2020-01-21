using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingspike : MonoBehaviour {

	// set move speed 
	public float moveSpeed = 7f;
	private Rigidbody fs;
	// set stopping point for fs
	private Vector3 RightStop = new Vector3 (5, float.Parse("3.5"), 0);

	// Use this for initialization
	void Start () {
		fs = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	// make sure that sp is destroyed after at RightStop
	void Update () {
		// set spawn point 
		var fsPos = new Vector3(-30, float.Parse("3.5"), 0);
	
			transform.position = Vector3.MoveTowards (transform.position, RightStop, moveSpeed * Time.deltaTime);
		if (transform.position == RightStop){

			// respawn object
			Instantiate (gameObject, fsPos, transform.rotation);
			Destroy (gameObject);
			}
		}
}


