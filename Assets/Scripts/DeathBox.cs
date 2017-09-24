using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<PhysicsEngine>().showTrails = false;
    }
}
