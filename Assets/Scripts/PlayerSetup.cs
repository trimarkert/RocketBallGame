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
			GameObject hudInstance = GameObject.Instantiate(hudObj) as GameObject;
			GetComponent<ShootRocket>().chargeSlider = hudInstance.GetComponentInChildren<Slider>();
		}
	}

	void Update()
	{
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
