using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour {

    private PhysicsEngine[] physicsEngineArray;
    private const float gravitationalConstant = 6.673e-11f; // [ m^3 * s^-2 * kg^-1 ]

    // Use this for initialization
    void Start () {
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate () {
        CalculateGravity();
    }

    private void CalculateGravity() {
        // every physics engine acts on every other physics engine
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray) {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray) {
                if (physicsEngineA != physicsEngineB && physicsEngineA != this) {
                    Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position;
                    float rSquared = Mathf.Pow(offset.magnitude, 2f);
                    float gravityMagnitude = gravitationalConstant * physicsEngineA.mass * physicsEngineB.mass / rSquared;
                    Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;

                    physicsEngineA.AddForce(-gravityFeltVector);
                }
            }
        }
    }
}
