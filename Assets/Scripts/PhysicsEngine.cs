using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

    public float mass; // M [kg] // 2,000,000 kg the mass of a space shuttle
    public Vector3 velocityVector; // [m * s^-1]
    public Vector3 netForceVector; // kN [kg * m * s^-2]
    public bool showTrails = true;

    private List<Vector3> forceVectorList = new List<Vector3>();
    private PhysicsEngine[] physicsEngineArray;
    private LineRenderer lineRenderer;
    private int numberOfForces;
    private const float gravitationalConstant = 6.673e-11f; // [ m^3 * s^-2 * kg^-1 ]

    // Use this for initialization
    void Start () {
        SetupForceTrails();
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
    }

    void FixedUpdate () {
        DrawTrails();
        CalculateGravity();
        UpdatePosition();
    }

    public void AddForce (Vector3 forceVector) {
        forceVectorList.Add(forceVector);
    }

    private void CalculateGravity () {
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

    private void UpdatePosition () {
        netForceVector = Vector3.zero;

        foreach (Vector3 forceVector in forceVectorList) {
            netForceVector += forceVector;
        }

        forceVectorList = new List<Vector3>(); // clear the list

        Vector3 accelerationVector = netForceVector / mass;

        velocityVector += accelerationVector * Time.deltaTime;
        transform.position += velocityVector * Time.deltaTime;
    }

    private void SetupForceTrails () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.yellow, Color.yellow);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.useWorldSpace = false;
    }

    private void DrawTrails () {
        if (showTrails) {
            lineRenderer.enabled = true;
            numberOfForces = forceVectorList.Count;
            lineRenderer.SetVertexCount(numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList) {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
        } else {
            lineRenderer.enabled = false;
        }
    }
}
