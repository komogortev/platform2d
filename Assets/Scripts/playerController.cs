using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 

public class playerController : MonoBehaviour {
	
	//Player Control
	private Rigidbody2D playerRigidbody2D;
	public GameObject playerTorso;
	public GameObject playerHips;

	public float maxSpeed = 10f;
	public float jumpForce = 700.0f;

	private bool grounded;
	private float groundRadius = 0.1f;
	public Transform groundCheckTransform;
	public LayerMask groundCheckLayerMask;

	bool facingRight = true;

	Animator animator;

	void Start () {
		//Fetch the RigidBody from the GameObject
		playerRigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	void Update () {
		 //Execute Jump mechanics
		if (grounded && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
			animator.SetBool("grounded", false);
			playerRigidbody2D.AddForce(new Vector2(0, jumpForce));

		}
	}

	void FixedUpdate () {
		//test ground (jumping) status
		UpdateGroundedStatus();

		//Get raw horizontal movement and accelerate Player wi
		float move = Input.GetAxis ("Horizontal");

		//Translate any horizontal (direction) speed into positive value
		animator.SetFloat ("speed", Mathf.Abs(move));
		animator.SetFloat("vSpeed", playerRigidbody2D.velocity.y);

		playerRigidbody2D.velocity = new Vector2 (move * maxSpeed, playerRigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}
	 
	void UpdateGroundedStatus(){
		//1 test whether Player is on the ground
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundRadius, groundCheckLayerMask);
		//2 update animator params
		animator.SetBool("grounded", grounded);
	}

	void Flip () {
		playerTorso.transform.localScale = new Vector3 (facingRight ? 1 : -1, 1,1);
		playerHips.transform.localScale = new Vector3 (facingRight ? 1 : -1, 1,1);
		facingRight = !facingRight;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag("Enemy")) {
			//@Todo switch to flickering animation
		}  

		if (other.gameObject.CompareTag("Object")) {
			//@Todo add celebration animation
		}  

		if (other.gameObject.CompareTag("ExitGate")) {
			//@Todo add celebration animation
		}  
	}

}
