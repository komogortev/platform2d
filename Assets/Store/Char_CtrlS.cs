using UnityEngine;
using System.Collections;

public class Char_CtrlS : MonoBehaviour {
	
	public float maxSpeed = 10f;
	bool right = true;
	
	Animator anim;
	
	bool ground = false;
	public Transform grCheck;
	float grRadius = 0.2f;
	public LayerMask wtGround;
	public float jForce = 700f;
	
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	void Update()
	{
		if (ground && Input.GetKeyDown (KeyCode.Space)) 
		{
			anim.SetBool("Ground",false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jForce));
		}
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		ground = Physics2D.OverlapCircle (grCheck.position, grRadius, wtGround);
		anim.SetBool ("Ground", ground);
		
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space)) {
			GetComponent<AudioSource>().Play();
				}
		
		float move = Input.GetAxis("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		if (move > 0 && !right) 
		{
			Flip ();
		} 
		else 
		{
			if (move < 0 && right)
			{
				Flip ();
			}
		}
	}
	
	void Flip()
	{
		right = !right;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
