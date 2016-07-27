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
		}
		if(isLocalPlayer)
		{
				gameObject.layer = LayerMask.NameToLayer("BlueTeam");
				gameObject.GetComponent<Renderer>().material.color = Color.blue;
				GameObject tempHUD = GameObject.Instantiate(hudObj) as GameObject;
				GetComponent<ShootRocket>().chargeSlider = tempHUD.GetComponentInChildren<Slider>();
		}
		else{
				gameObject.layer = LayerMask.NameToLayer("RedTeam");
				gameObject.GetComponent<Renderer>().material.color = Color.red;
		}

	}
}
