using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour {
	public float spawnTime;
	public GameObject customer;
	private float nextTime = 0.5f;
    private Path path;
	// Use this for initialization
	void Start () {
		path = GameObject.FindGameObjectWithTag(Tags.path).GetComponent<Path>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.timeSinceLevelLoad > nextTime) {
			nextTime += (Random.Range(2,5) * 4);
			Instantiate (customer, path.GetPoint(0), transform.rotation);
		}
	}
}
