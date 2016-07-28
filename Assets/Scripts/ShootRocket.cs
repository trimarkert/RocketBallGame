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
	public Slider chargeSlider; // the slider that is associated with shooting the basketball.
	public float chargeSpeed = 10.0f; //how fast to fill up bar for basketball shot.
	public float maxForce = 350.0f; 
	public Transform bulletSpawn;
	public GameObject myCamera;
	public GameObject spawningPlayer; // Reference to self. not sure if this will work

	[Command]
	public void Cmd_ShootARocket(){
			Debug.Log("Made it to command");
			if(gameObject.layer == LayerMask.NameToLayer("BlueTeam"))
			{
				GameObject tempRocket = Instantiate(blueRocket, 
				                                    myCamera.transform.position, 
				                                    Quaternion.LookRotation(myCamera.transform.forward)) as GameObject;
				//NetworkServer.SpawnWithClientAuthority(tempRocket, connectionToClient);
				Debug.Log ("Created tempRocket");
				Debug.Log (tempRocket.ToString());
				Debug.Log ("is Client comes back as" + isClient);
				NetworkServer.Spawn(tempRocket);
			}
			else{
				GameObject tempRocket = Instantiate(redRocket, 
				                                    myCamera.transform.position, 
				                                    Quaternion.LookRotation(myCamera.transform.forward)) as GameObject;
				//NetworkServer.SpawnWithClientAuthority(tempRocket, connectionToClient);
				Debug.Log ("Created tempRocket");
				Debug.Log (tempRocket.ToString());
				Debug.Log ("is Client comes back as" + isClient);
				NetworkServer.Spawn(tempRocket);
			}
	
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
			Debug.Log("Got to fireButton");
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
			GameObject tempBall = GameObject.Instantiate(scoreBallPrefab,
			                       bulletSpawn.position,
			                       Quaternion.LookRotation(myCamera.transform.forward)) as GameObject;
			Rigidbody tempRigid = tempBall.GetComponent<Rigidbody>();
			if(tempRigid != null)
			{
				tempRigid.AddForce(tempBall.transform.forward * chargeSlider.value * maxForce);
			}
			if(gameObject.layer == LayerMask.NameToLayer("BlueTeam"))
			{
				tempBall.layer = LayerMask.NameToLayer("BlueTeam");
			}
			else{
				tempBall.layer = LayerMask.NameToLayer("RedTeam");
			}
			chargeSlider.value = 0f;
		}
	}
}
