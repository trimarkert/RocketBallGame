using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShootRocket : MonoBehaviour {

	public float speed = 10.0f; // how fast rocket shoots
	public GameObject rocketPrefab; //prefab of rocket object
	//Variables relating to the score ball
	public GameObject scoreBallPrefab; //The object that will be spawned when we let go of the right mouse button
	public Slider chargeSlider; // the slider that is associated with shooting the basketball.
	public float chargeSpeed = 10.0f; //how fast to fill up bar for basketball shot.
	public float maxForce = 350.0f; 
	public Transform bulletSpawn;
	public GameObject parentObject;

	// Update is called once per frame
	void Update () 
	{
		if( Input.GetButtonDown("Fire1"))
		{
			GameObject tempRocket = GameObject.Instantiate(rocketPrefab, 
			                       bulletSpawn.position, 
			                       Quaternion.LookRotation(transform.forward)) as GameObject;
			//Simple one line fix! Don't forget to just put in Physics. and trying to find an appropriate function
			Physics.IgnoreCollision(tempRocket.GetComponent<Collider>(), parentObject.GetComponent<CapsuleCollider>());
		}

		/**
		 * The plan: when right click is held down, we increase the value of the slider based on time
		 * when the right click is let go, we spawn a ball who will take the value as a parameter for the force
		 * to go forward from the slider itself.
		 */ 
		if(Input.GetButton("Fire2"))
		{
			chargeSlider.value += (chargeSpeed * Time.deltaTime);
		}
		if(Input.GetButtonUp("Fire2"))
		{
			GameObject tempBall = GameObject.Instantiate(scoreBallPrefab,
			                       bulletSpawn.position,
			                       Quaternion.LookRotation(transform.forward)) as GameObject;
			Rigidbody tempRigid = tempBall.GetComponent<Rigidbody>();
			if(tempRigid != null)
			{
				tempRigid.AddForce(tempBall.transform.forward * chargeSlider.value * maxForce);
			}
			chargeSlider.value = 0f;
		}
	}
}
