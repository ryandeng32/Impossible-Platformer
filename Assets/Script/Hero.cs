using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hero : MonoBehaviour {

	// declare images 
	public GameObject health1;
	public GameObject health2;
	public GameObject health3;
	public GameObject gameStart;
	public GameObject gameOver;
	public GameObject gameWin;
	public GameObject gameObjective;
	int healthTotal = 30;
	public Text txtOut;
		// Defines Hero speed horizontally
		public float speed = 5;

		// Defines Hero facing direction
	public bool facingRight = true;

		// Jump speed
		public float jumpSpeed = 5f;

		// Hero components
		private Rigidbody2D rb;
		private Animator animator;

		// Will flip depending if on ground
		bool isJumping = false;

		// Used to check if Hero is on the ground
		private float rayCastLength = 0.005f;

		// Sprite width and height
		private float width;
		private float height;

		// How long is the jump button held
		private float jumpButtonPressTime;

		// Max jump amount
		public float maxJumpTime = 0.2f;


	int score = 0;
	void Start(){
		gameOver.SetActive (false);
		gameStart.SetActive(true);
		gameWin.SetActive (false);
		gameObjective.SetActive (true);
	}
		void FixedUpdate(){

			// Get horizontal movement -1 Left, or 1 Right
			float horzMove = Input.GetAxisRaw ("Horizontal");

			// Need to get Hero y
			Vector2 vect = rb.velocity;

			// Change x and keep y as is
			rb.velocity = new Vector2 (horzMove * speed, vect.y);



			// Set the speed so the right Animation is played
			animator.SetFloat("Speed", Mathf.Abs(horzMove));

			// Makes sure Hero is facing the right direction
			if (horzMove > 0 && !facingRight) {
				FlipHero ();
			} else if (horzMove < 0 && facingRight) {
				FlipHero ();
			}

			// Get vertical movement
			float vertMove = Input.GetAxis ("Jump");

			if (IsOnGround () && isJumping == false) {
				if (vertMove > 0f) {
					isJumping = true;

				}
			}

			// If button is held pass max time set 
			// vertical move to 0
			if (jumpButtonPressTime > maxJumpTime) {
				vertMove = 0f;
			}

			// If is jumping and we have a valid jump press length
			// make Manny jump
			if (isJumping && (jumpButtonPressTime < maxJumpTime)) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
			}

			// If we have moved high enough make Manny fall
			// Set Mannys Rigidbody 2d Gravity Scale to 2
			if (vertMove >= 1f) {
				jumpButtonPressTime += Time.deltaTime;
			} else {
				isJumping = false;
				jumpButtonPressTime = 0f;
			}

		}

		// Makes sure components have been created when the 
		// game starts
		void Awake(){
			
			animator = GetComponent<Animator> ();
			rb = GetComponent<Rigidbody2D> ();

			// Gets Hero collider width and height and
			// then adds more to it. Used to raycast to see
			// if Hero is colliding with anything so we
			// can jump
			width = GetComponent<Collider2D> ().bounds.extents.x + 0.1f;
			height = GetComponent<Collider2D> ().bounds.extents.y + 0.2f;
		}

		// When moving in a direction face in that direction
		void FlipHero(){

			// Flip the facing value
			facingRight = !facingRight;

			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		public bool IsOnGround(){

			// Check if contacting the ground straight down
			bool groundCheck1 = Physics2D.Raycast (new Vector2 (
				transform.position.x,
				transform.position.y - height),
				-Vector2.up, rayCastLength);

			// Check if contacting ground to the right
			bool groundCheck2 = Physics2D.Raycast (new Vector2 (
				transform.position.x + (width - 0.2f),
				transform.position.y - height),
				-Vector2.up, rayCastLength);

			// Check if contacting ground to the left
			bool groundCheck3 = Physics2D.Raycast (new Vector2 (
				transform.position.x - (width - 0.2f),
				transform.position.y - height),
				-Vector2.up, rayCastLength);

			if (groundCheck1 || groundCheck2 || groundCheck3)
				return true;

			return false;

		}


	// check hero collision
	// healthTotal is deduct by one if hero is hit by a enemy
	void OnCollisionEnter2D (Collision2D hit)
	{
		
		if (hit.gameObject.tag == "Coin") {
			Destroy (hit.gameObject);
			score += 1;
		}
		if (hit.gameObject.tag == "Bee") {
			if (hit.collider.GetType () == typeof(BoxCollider2D)) {
				gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1) * 700.0f);

			} else if (hit.collider.GetType () == typeof(CapsuleCollider2D)) {
			//	Instantiate(gameObject, HeroPos, transform.rotation);
			//	Destroy (gameObject);
				healthTotal -= 1;
			}
		}
		if (hit.gameObject.tag == "Snail") {
			var SnailPos = new Vector3(20, -4, 0);
			
			if (hit.collider.GetType () == typeof(BoxCollider2D)) {
				gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1) * 800.0f);

				Instantiate(hit.gameObject,SnailPos, transform.rotation);
				Destroy (hit.gameObject);

			} else if (hit.collider.GetType () == typeof(CapsuleCollider2D)) {
				//Instantiate(gameObject, HeroPos, transform.rotation);
				//Destroy (gameObject);
				healthTotal -= 1;

			}
		}
		if (hit.gameObject.tag == "Mario") {
			var MarioPos = new Vector3 (20, float.Parse("3.3"), 0);

			if (hit.collider.GetType () == typeof(BoxCollider2D)) {
				gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1) * 600.0f);

				Instantiate (hit.gameObject, MarioPos, transform.rotation);
				Destroy (hit.gameObject);

			} else if (hit.collider.GetType () == typeof(CapsuleCollider2D)) {
				//Instantiate (gameObject, HeroPos, transform.rotation);
				//Destroy (gameObject);
				healthTotal -= 1;
			}
		}
		if (hit.gameObject.tag == "Spike") {
			
			//Instantiate(gameObject, HeroPos, transform.rotation);
			//		Destroy (gameObject);
			healthTotal -=1;
		}
		if (hit.gameObject.tag == "fs") {

			//Instantiate(gameObject, HeroPos, transform.rotation);
			//Destroy (gameObject);
			healthTotal -= 1;
		}

	if (hit.gameObject.tag == "Fireball") {

		//Instantiate(gameObject, HeroPos, transform.rotation);
		//Destroy (gameObject);
			healthTotal -= 1;
		}


			if (hit.gameObject.tag == "Prizeblock") {
				if (hit.collider.GetType() == typeof(BoxCollider2D)) {

					Destroy (hit.gameObject);
				}

			}
		if (hit.gameObject.tag == "Pacman") {
			//Instantiate(gameObject, HeroPos, transform.rotation);
			//Destroy (gameObject);
			healthTotal -= 1;
		}
			}

		



	// display health point and game start/instruction/end images
	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			gameStart.SetActive(false);}
		if (Input.GetKeyDown (KeyCode.S)) {
			gameObjective.SetActive (false);
		}
		txtOut.text = "Your score is " + score;
		if (healthTotal == 3) {
			health1.SetActive (true);
			health2.SetActive (true);
			health3.SetActive (true);
		}
		if (healthTotal == 2) {
			health1.SetActive (true);
			health2.SetActive (true);
			health3.SetActive (false);
		}
		if (healthTotal == 1) {
			health1.SetActive (true);
			health2.SetActive (false);
			health3.SetActive (false);
		}
		if (healthTotal == 0) {
			gameOver.SetActive (true);
		}	

		if (score == 5) {
			gameWin.SetActive (true);
		}
	}
}
