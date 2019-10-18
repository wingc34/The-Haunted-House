using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class ChangeMaterial : MonoBehaviour {

    public Texture[] textures;
    public int changedClothIndex = 9;

    private SkinnedMeshRenderer skinnedMeshRenderer;
    private ParticleSystem changingEffect;
    private int changingClothIndex;
    private float timeCounter;
    private bool timerStarted;
    private bool isCollidedCustomers;

	// Use this for initialization
	void Start () {
		skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        changingEffect = GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		if(timerStarted) {
            timeCounter += Time.deltaTime;
            if (timeCounter >= 2f) {
                timeCounter = 0;
                timerStarted = false;
                changedClothIndex = changingClothIndex;
                skinnedMeshRenderer.material.mainTexture = textures[changingClothIndex];
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Customer(Clone)")
            isCollidedCustomers = true;
        if (!isCollidedCustomers && other.gameObject.CompareTag("Closet") && (CnInputManager.GetButtonDown("Action"))) {
            if (other.gameObject.name == "Zombie" && changedClothIndex != 0) {
                changingClothIndex = 0;
                playAnimation();
            } else if (other.gameObject.name == "Mummy" && changedClothIndex != 1) {
                changingClothIndex = 1;
                playAnimation();
            } else if (other.gameObject.name == "Pumpkin" && changedClothIndex != 2) {
                changingClothIndex = 2;
                playAnimation();
            } else if (other.gameObject.name == "Skeleton" && changedClothIndex != 3) {
                changingClothIndex = 3;
                playAnimation();
            } else if (other.gameObject.name == "Joker" && changedClothIndex != 4) {
                changingClothIndex = 4;
                playAnimation();
            } else if (other.gameObject.name == "Frankenstein" && changedClothIndex != 5) {
                changingClothIndex = 5;
                playAnimation();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Customer(Clone)")
            isCollidedCustomers = false;
        timeCounter = 0;
        timerStarted = false;
        changingEffect.Stop();
    }

    private void playAnimation()
    {
        timerStarted = true;
        changingEffect.Play();
    } 
}
