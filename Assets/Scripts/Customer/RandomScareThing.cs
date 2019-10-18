using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomScareThing : MonoBehaviour {
	public enum scareThingsList	//sort in prob 
	{
		Zombie,
		Mummy,
		Pumpkin,
		Skeleton,
		Joker,
		Frankenstein,
		NumberOfTypes
	};
	private int[] scareThingProb = new int[]{20,18,8,18,18,18};	//in percentage
	public int outputSize;	
	public List<scareThingsList> assignedScareThing = new List<scareThingsList> ();	//final randomed list
	private bool ranFinish = false;
    private bool addedBonus = false;
    private gameController gamCon;
	private ScaredThingsBubble scaredThings;
	private CustomerAI customerAI;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		gamCon = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController> ();
		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			if (gamCon.timeLeft < gamCon.gameTimeInSec - 60)
				outputSize = 3;
		}
		randomScareThings(outputSize);
		scaredThings = GetComponentInChildren<ScaredThingsBubble> ();
		customerAI = GetComponent<CustomerAI> ();
	}

	// Update is called once per frame
	void Update () {
        if (assignedScareThing.Count <= 0)      //When player scared all things, bubble disappear and add bonus score to player.
        {
            CalculateTime(2.0f);
            if (!addedBonus)
            {
                gamCon.addBounsScore();
                addedBonus = true;
            }
        }
			
	}

	void randomScareThings(int size){
		int count = 0;
		int tempThing;
		while (!ranFinish) {
			if (count == size)
				ranFinish = true;
			else {
				tempThing = ranProb();
				bool canAdd = true;
				foreach (scareThingsList s in assignedScareThing) {	//check whether it is random a repeat value
					if (tempThing == (int)s) {
						canAdd = false;
						break;
					}
				}
				if (canAdd) {	//when it is no repeat value, then add
					assignedScareThing.Add ((scareThingsList)tempThing);
					count++;
				}
			}
		}
	}
	private int ranProb(){
		int ranNo = Random.Range (0, 101);	//prob percentage
		int tempInt = 0;
		for (int i = 0; i < scareThingProb.Length; i++) {
			if (i == 0) {
				if (ranNo >= 0 && ranNo < (int)scareThingProb.GetValue (i))
					return 0;
			} else {
				if (ranNo >= tempInt && ranNo < ((int)scareThingProb.GetValue (i) + tempInt))
					return i;
			}
			tempInt += (int)scareThingProb.GetValue (i);
		}
		return 0;
	}

	public bool isMatchScareThing(int no){		//check if player current cloth match customer scare things
		foreach (scareThingsList s in assignedScareThing) {
			if (no == (int)s) {
				assignedScareThing.Remove (s);
				gamCon.addScore();
				scaredThings.removeIcon ((int)s);
				return true;
			}
		}
		return false;
	}
		

	public List<scareThingsList> getAssignedScareThings(){
		List<scareThingsList> scaredThings = new List<scareThingsList> (assignedScareThing);
		return scaredThings;
	}

	void CalculateTime(float timeLimit){
		timer += Time.deltaTime;
		if (timer >= timeLimit) {
			customerAI.allScared ();
			scaredThings.bubbledisappear ();
		}
	}

}
