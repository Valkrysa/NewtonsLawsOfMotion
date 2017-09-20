using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {

    public float fuelMass; // M [kg]
    public float maxThrust; // kN [kg * m * s^-2] // 2,278 gives you the same thrust as space shuttle
    [Range (0, 1f)]
    public float thrustPercent;
    public Vector3 thrustUnitVector;

    private PhysicsEngine physicsEngine;
    private float currentThrust; // N [kg * m * s^-2]

	// Use this for initialization
	void Start () {
        physicsEngine = GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (fuelMass > FuelThisUpdate()) {
            fuelMass -= FuelThisUpdate();
            physicsEngine.mass -= FuelThisUpdate();
            ExertForce();
        } else {
            Debug.LogWarning("OUT OF ROCKET FUEL");
        }
    }

    private void ExertForce () {
        currentThrust = maxThrust * thrustPercent * 1000f;
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // N

        physicsEngine.AddForce(thrustVector);
    }

    private float FuelThisUpdate() {
        float exhaustMassFlow = 66.77f; // [kg/s]
        float effectiveExhaustVelocity; // [m * s^-1] liquid H 0

        effectiveExhaustVelocity = 4462;

        // thrust = mass flow * exhaust velocity
        // mass flow = thrust / exhaust velocity
        exhaustMassFlow = currentThrust / effectiveExhaustVelocity;

        return exhaustMassFlow * Time.deltaTime; // [kg]
    }
}
