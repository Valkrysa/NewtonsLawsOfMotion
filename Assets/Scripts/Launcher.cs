using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
    
    public float maxLaunchSpeed = 200f;
    public AudioClip powerUpSound;
    public AudioClip launchSound;
    public GameObject projectile;

    private float currentLaunchSpeed = 0f;
    private AudioSource audioSource;
    private float launchPowerTickTime = 0.5f;
    private float launchPowerPerSecond;
    private GameObject launchedBallsParent;

    // Use this for initialization
    void Start () {
        launchedBallsParent = GameObject.Find("LaunchedBalls");
        audioSource = GetComponent<AudioSource>();

        launchPowerPerSecond = maxLaunchSpeed / powerUpSound.length;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown () {
        audioSource.clip = powerUpSound;
        audioSource.Play();
        currentLaunchSpeed = 0f;
        InvokeRepeating("IncreaseLaunchSpeed", 0.1f, launchPowerTickTime);
    }

    void OnMouseUp () {
        CancelInvoke();

        if (currentLaunchSpeed > maxLaunchSpeed) {
            currentLaunchSpeed = maxLaunchSpeed;
        }

        GameObject newBall = Instantiate(projectile, transform.position, Quaternion.identity);
        newBall.GetComponent<PhysicsEngine>().velocityVector = transform.up * currentLaunchSpeed;
        newBall.transform.parent = launchedBallsParent.transform;

        audioSource.clip = launchSound;
        audioSource.Play();
    }

    private void IncreaseLaunchSpeed () {
        if (currentLaunchSpeed < maxLaunchSpeed) {
            currentLaunchSpeed += (launchPowerPerSecond * launchPowerTickTime);
        }
    }
}
