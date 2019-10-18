using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour {

	public enum FSMstate
	{
		walk,runFront,crouch,runBack //0,1,2,3
	};
	public FSMstate curstate; 
	public float walkSpeed = 3.0f;
	public float runSpeed = 5.0f;
	private int ranstate;
	private CustomerStates Cstates;
	private CustomerMovement customerMovement;
	private float timer = 0.0f;
	private float timer2 = 0.0f;
	private float crouchSpeed = 0.1f;
	private float moveSpeed;
	private Animator anim;
	private ScaredThingsBubble scaredThings;

	// Use this for initialization
	void Start () {
		curstate = FSMstate.walk;
		ranstate = Random.Range(2,4); 
		walkSpeed = (Random.Range (2, 6) * 0.5f);
		customerMovement = GetComponent<CustomerMovement> ();
		anim = GetComponent<Animator> ();
		scaredThings = GetComponentInChildren<ScaredThingsBubble> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (curstate) {
		case FSMstate.walk:
			UpdateWalkState ();
			break;
		case FSMstate.runFront:
			UpdateRunFrontState();
			break;
		case FSMstate.crouch:
			UpdateCrouchState();
			break;
		case FSMstate.runBack:
			UpdateRunBackState();
			break;
		default:
			break;
		}
		anim.SetFloat ("Speed",moveSpeed);
	}

	void UpdateWalkState(){
		customerMovement.speed = walkSpeed;
		customerMovement.Walk ();
		moveSpeed = customerMovement.animSpeed;
		anim.SetBool ("isScared", false);
	}
	void UpdateRunFrontState(){
		customerMovement.speed = runSpeed;
		customerMovement.Walk ();
		moveSpeed = customerMovement.animSpeed;
		anim.SetBool ("isScared", false);
		CalculateTime (2.0f);
	}
	void UpdateCrouchState(){
		customerMovement.speed = crouchSpeed;
		anim.SetBool ("isScared", true);
		scaredThings.bubbleEffect (true);
		moveSpeed = 0.1f;
		CalculateTime (2.0f);
	}
	void UpdateRunBackState(){
		timer2 += Time.deltaTime;
		if (timer2 <= 0.5f) {
			customerMovement.speed = crouchSpeed;
			moveSpeed = 0.1f;
		} else {
			customerMovement.speed = runSpeed;
			customerMovement.ReverseWalk ();
			moveSpeed = customerMovement.animSpeed;
		}
		scaredThings.bubbleEffect (true);
		anim.SetBool ("isScared", true);
		CalculateTime (2.0f);
	}

	public void isScared(){
		curstate = (FSMstate)ranstate;
	}

	public void allScared(){
		curstate = (FSMstate)1;
	}

	void CalculateTime(float timeLimit){
		timer += Time.deltaTime;
		if (timer >= timeLimit) {
			curstate = FSMstate.walk;
			scaredThings.bubbleEffect (false);
			timer = 0.0f;
		}
	}

}
