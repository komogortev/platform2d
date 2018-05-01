using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerController : MonoBehaviour {
	

	private Rigidbody2D playerRigidbody2D;

	public float jumpForce = 700.0f;

	public float forwardMovementSpeed = 5.0f;
	public float maxSpeed = 10f;
	private float calculatedSpeed;

	private bool grounded;
	public Transform groundCheckTransform;
	float groundRadius = 0.1f;
	public LayerMask groundCheckLayerMask;

	private bool idle = true;


	Animator animator;

	// Use this for initialization
	void Start () {
		playerRigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//1. catch tap
		bool tapTriggered = Input.GetMouseButtonDown(0);

		//2. move character
		if (!idle && grounded) {
			Vector2 newVelocity = playerRigidbody2D.velocity;
			calculatedSpeed = forwardMovementSpeed;
			newVelocity.x = calculatedSpeed;

			playerRigidbody2D.velocity = newVelocity;
		}
		//3. jump
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()  && !idle && grounded && tapTriggered){
			//disable grounded animation
			animator.SetBool ("grounded", false);
			//launch player up
			playerRigidbody2D.AddForce(new Vector2(0, jumpForce));

			Vector2 accellVelocity = playerRigidbody2D.velocity;
			calculatedSpeed = forwardMovementSpeed * 3f;
			accellVelocity.x = calculatedSpeed;
			playerRigidbody2D.velocity = accellVelocity;
		}
		//4. start run anim
		if (idle && tapTriggered) {
			idle = false;
			animator.SetBool("idle", idle);
		}

			
	}

	void FixedUpdate () 
	{



		//test ground status
		UpdateGroundedStatus();
	}

	void UpdateGroundedStatus()
	{
		//1
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundRadius, groundCheckLayerMask);

		//2 update animator params
		animator.SetBool("grounded", grounded);
		animator.SetFloat("vSpeed", playerRigidbody2D.velocity.y);
		animator.SetFloat("speed", playerRigidbody2D.velocity.x);

	}
}
