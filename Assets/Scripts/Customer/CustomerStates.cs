using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStates : MonoBehaviour {
	public const int runFront = 1;
	public const int crouch = 2;
	public const int runBack = 3;
	public int result;

	public CustomerStates(){
		result = Random.Range (1, 4);
	}
		
}
