using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDrag : MonoBehaviour {

    [Range(1f, 2f)]
    public float dragVelocityExponent = 1f;
    public float dragConstant;

    private PhysicsEngine physicsEngine;

    // Use this for initialization
    void Start() {
        physicsEngine = GetComponent<PhysicsEngine>();
    }

    void FixedUpdate () {
        Vector3 velocityVector = physicsEngine.velocityVector;
        float speed = velocityVector.magnitude;
        float dragSize = CalculateDrag(speed);
        Vector3 dragVector = dragSize * -velocityVector.normalized;

        physicsEngine.AddForce(dragVector);
    }

    public float CalculateDrag (float speed) {
        return dragConstant * Mathf.Pow(speed, dragVelocityExponent);
    }
}
