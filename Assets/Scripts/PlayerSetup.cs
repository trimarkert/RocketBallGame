using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

/**
 * Initial setup of player characters. Makes sure unnecessary components are turned off.
 * Sets up the players in their appropriate team etc.
 */ 
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;
	Camera sceneCamera;
	public GameObject hudObj;

	[SyncVar(hook = "OnColorChange")]
	public Color myColor;

	void Start () {

		//Logic to turn off unnecessary components
		if(!isLocalPlayer)
		{
			for(int curComponent = 0; curComponent < componentsToDisable.Length; curComponent++)
			{
				componentsToDisable[curComponent].enabled = false;
			}
		}else
		{
			sceneCamera = Camera.main;
			if(sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive(false);
			}
			//First check to see if this client already has a hud created (aka they disconnected and reconnected)
			GameObject hudInstance;
			if(GameObject.FindWithTag("HUD") == null)
			{
				hudInstance = GameObject.Instantiate(hudObj) as GameObject;
			}
			else
				hudInstance = GameObject.FindWithTag("HUD");
			GetComponent<ShootRocket>().chargeSlider = hudInstance.GetComponentInChildren<Slider>();
		}
		//Logic to set teams
		GetComponent<Renderer>().material.color = myColor;
		if(myColor == Color.red)
		{
			gameObject.layer = LayerMask.NameToLayer("RedTeam");
		}
		else{
			gameObject.layer = LayerMask.NameToLayer("BlueTeam");
		}

	}

	public void OnColorChange(Color newColor){
		myColor = newColor;
	}

}
