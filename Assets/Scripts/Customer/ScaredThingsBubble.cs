using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredThingsBubble : MonoBehaviour {
	public GameObject[] imgsIcon;
	public GameObject popOut;
	GameObject g;
	private RandomScareThing ran;
	private int assignedArrayLength;
	//imgsIconScales 1--0.7 2--0.6 3--0.5 4--0.4
	private Vector3[] positionArray = new Vector3[10];
	private float[] imgsIconScales = new float[]{0.7f,0.5f,0.5f,0.4f};
	public GameObject[] iconsInBubble = new GameObject[6];
	private SpriteRenderer bubbleSR;
	private SpriteRenderer popBbubbleSR;
	//private float timer = 0.0f;
	// Use this for initialization
	void Start () {
		
		initPositionArray ();
		ran = GetComponentInParent<RandomScareThing>();
		bubbleSR = GetComponent<SpriteRenderer>();
		popBbubbleSR = popOut.GetComponent<SpriteRenderer>();
		assignedArrayLength = ran.assignedScareThing.Count;
		//GameObject[] iconsInBubble = new GameObject[assignedArrayLength];
		int posArrayIndex = 0;
		float scale = 1f;
		switch (assignedArrayLength) {
		case 1:
			posArrayIndex = 0;
			scale = imgsIconScales [0];
			break;
		case 2:
			posArrayIndex = 1;
			scale = imgsIconScales [1];
			break;
		case 3:
			posArrayIndex = 3;
			scale = imgsIconScales [2];
			break;
		case 4:
			posArrayIndex = 6;
			scale = imgsIconScales [3];
			break;
		}
		for (int i = 0; i < assignedArrayLength; i++) {
			for (int j = 0; j < imgsIcon.Length; j++) {
				if ((int)ran.assignedScareThing [i] == j) {
					if (assignedArrayLength == 1)
						instantiatePosition (i, j, scale, posArrayIndex);
					else if (assignedArrayLength == 2)
						instantiatePosition (i, j, scale, posArrayIndex);
					else if (assignedArrayLength == 3)
						instantiatePosition (i, j, scale, posArrayIndex);
					else if (assignedArrayLength == 4)
						instantiatePosition (i, j, scale, posArrayIndex);
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(40,0,0);
		popOut.transform.eulerAngles = new Vector3(40,0,0);
	}

	void initPositionArray(){
		positionArray [0] = new Vector3 (0f, 0f, -0.5f);	//for only one item

		positionArray [1] = new Vector3 (0.5f, -0.2f, -0.5f);	//for two items
		positionArray [2] = new Vector3 (-0.5f, -0.2f, -0.5f);

		positionArray [3] = new Vector3 (0.5f, 0f, -0.5f);	//for three items
		positionArray [4] = new Vector3 (-0.5f, 0f, -0.5f);
		positionArray [5] = new Vector3 (0.3f, -0.7f, -0.2f);

		positionArray [6] = new Vector3 (0.5f, 0f, -0.5f);	//for four items
		positionArray [7] = new Vector3 (-0.4f, 0f, -0.5f);
		positionArray [8] = new Vector3 (0.5f, -0.7f, -0.2f);
		positionArray [9] = new Vector3 (-0.4f, -0.7f, -0.2f);
	}
	void instantiatePosition(int assignedArrayIndex, int imgsIconIndex,float scale,int posArrayIndex){
		iconsInBubble [assignedArrayIndex] = Instantiate (imgsIcon [imgsIconIndex], transform.position + positionArray [posArrayIndex+assignedArrayIndex], transform.rotation, this.transform);
		iconsInBubble [assignedArrayIndex].transform.localScale = new Vector3 (scale, scale, 1f);
	}
	public void removeIcon(int no){
		for(int i = 0;i<iconsInBubble.Length;i++)
		{
			if (iconsInBubble [i].name == imgsIcon [no].name+"(Clone)") {
				iconsInBubble [i].GetComponent<SpriteRenderer> ().color = new Color (0f, 0f, 0f, 0f);
				break;
			}
		}
	}
	public void bubbleEffect(bool pop){
		var length = ran.assignedScareThing.Count;
		if (length >= 0) {
			if (pop) {
				bubbleSR.color = new Color (bubbleSR.color.r, bubbleSR.color.g, bubbleSR.color.b, 0);
				popBbubbleSR.color = new Color (popBbubbleSR.color.r, popBbubbleSR.color.g, popBbubbleSR.color.b, 1);
			} else {
				bubbleSR.color = new Color (bubbleSR.color.r, bubbleSR.color.g, bubbleSR.color.b, 1);
				popBbubbleSR.color = new Color (popBbubbleSR.color.r, popBbubbleSR.color.g, popBbubbleSR.color.b, 0);
			}
		}

	}
	public void bubbledisappear(){
			bubbleSR.color = new Color (bubbleSR.color.r,bubbleSR.color.g, bubbleSR.color.b, 0);
			popBbubbleSR.color = new Color (popBbubbleSR.color.r,popBbubbleSR.color.g, popBbubbleSR.color.b, 0);
	}
}
