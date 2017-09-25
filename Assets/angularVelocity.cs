using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angularVelocity : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.angularVelocity = new Vector3(4f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
