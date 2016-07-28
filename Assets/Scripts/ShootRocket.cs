using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class ShootRocket : NetworkBehaviour {

	public float speed = 10.0f; // how fast rocket shoots
	public GameObject redRocket; //prefab of rocket object
	public GameObject blueRocket;
	//Variables relating to the score ball
	public GameObject scoreBallPrefab; //The object that will be spawned when we let go of the right mouse button
	public GameObject scoreBallBluePrefab; //The object that will be spawned when we let go of the right mouse button
	public GameObject scoreBallRedPrefab; //The object that will be spawned when we let go of the right mouse button
	public Slider chargeSlider; // the slider that is associated with shooting the basketball.
	public float chargeSpeed = 10.0f; //how fast to fill up bar for basketball shot.
	public float maxForce = 350.0f; 
	public Transform bulletSpawn;
	public GameObject myCamera;
	public GameObject spawningPlayer; // Reference to self. not sure if this will work

	[Command]
	public void Cmd_ShootARocket()
	{
			if(spawningPlayer.layer == LayerMask.NameToLayer("BlueTeam"))
			{
				GameObject tempRocket = Instantiate(blueRocket, 
				                                    myCamera.transform.position, 
				                                    Quaternion.LookRotation(myCamera.transform.forward)) as GameObject;
				//NetworkServer.SpawnWithClientAuthority(tempRocket, connectionToClient);

				NetworkServer.Spawn(tempRocket);
			}
			else{
				GameObject tempRocket = Instantiate(redRocket, 
				                                    myCamera.transform.position, 
				                                    Quaternion.LookRotation(myCamera.transform.forward)) as GameObject;
//				//NetworkServer.SpawnWithClientAuthority(tempRocket, connectionToClient);
//			Debug.Log ("The slider is at " + chargeSlider.value);
//			//tempRocket.GetComponent<Rigidbody>().AddForce(tempRocket.transform.forward * chargeSlider.value * maxForce);
				NetworkServer.Spawn(tempRocket);
				
			}
	
	}
	[Command]
	public void Cmd_ShootScoreBall(float myCharge)
	{
		ShootRocket spawningScript = spawningPlayer.GetComponent<ShootRocket>();
		//spawningPlayer.GetComponent<ShootRocket>().bulletSpawn.position
		//Slider spawningSlider = spawningPlayer.GetComponent<ShootRocket>().chargeSlider;


//		scoreBallPrefab.GetComponent<BallManager>().startingForce = myCharge * maxForce;
//		GameObject tempBall = GameObject.Instantiate(scoreBallPrefab,
//		                                             spawningScript.myCamera.transform.position,
//		                                             Quaternion.LookRotation(spawningScript.myCamera.transform.forward)) as GameObject;


		//tempBall.GetComponent<BallManager>().startingForce = chargeSlider.value * maxForce;
		if(spawningPlayer.layer == LayerMask.NameToLayer("BlueTeam"))
		{
//			Debug.Log ("We should really be here");
//			tempBall.layer = LayerMask.NameToLayer("BlueTeam");
			scoreBallBluePrefab.GetComponent<BallManager>().startingForce = myCharge * maxForce;
			GameObject tempBall = GameObject.Instantiate(scoreBallBluePrefab,
			                                             spawningScript.myCamera.transform.position,
			                                             Quaternion.LookRotation(spawningScript.myCamera.transform.forward)) as GameObject;
			NetworkServer.Spawn(tempBall);
		}
		else{
			scoreBallRedPrefab.GetComponent<BallManager>().startingForce = myCharge * maxForce;
			GameObject tempBall = GameObject.Instantiate(scoreBallRedPrefab,
			                                             spawningScript.myCamera.transform.position,
			                                             Quaternion.LookRotation(spawningScript.myCamera.transform.forward)) as GameObject;
			NetworkServer.Spawn(tempBall);
		}
		//tempBall.GetComponent<Rigidbody>().AddForce(tempBall.transform.forward * chargeSlider.value * maxForce);
		//NetworkServer.SpawnWithClientAuthority(tempBall, connectionToClient);
		//tempBall.GetComponent<Rigidbody>().AddForce(tempBall.transform.forward * chargeSlider.value * maxForce);
		//NetworkServer.Spawn(tempBall);
	}
	void Start()
	{
		spawningPlayer = this.gameObject;
	}

	// Update is called once per frame
	void Update () 
	{
		if(!isLocalPlayer)
		{
			return;
		}

		if( Input.GetButtonDown("Fire1"))
		{
			Cmd_ShootARocket();	
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
			Cmd_ShootScoreBall(chargeSlider.value);
			chargeSlider.value = 0f;
		}
	}
}
