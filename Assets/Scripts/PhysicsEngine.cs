﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

    public List<Vector3> forceVectorList;
    public Vector3 velocityVector; // average velocity in fixed update
    public Vector3 netForceVector;
    public float mass;

    // Use this for initialization
    void Start () {
        AddForces();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate () {
        AddForces();
        UpdateVelocity();
        transform.position += velocityVector * Time.deltaTime;
    }

    private void UpdateVelocity () {
        Vector3 accelerationVector = netForceVector / mass;

        velocityVector += accelerationVector * Time.deltaTime;
    }

    private void AddForces () {
        Vector3 netForces = new Vector3(0, 0, 0);

        foreach(Vector3 forceVector in forceVectorList) {
            netForces += forceVector;
        }

        netForceVector = netForces;
    }
}
