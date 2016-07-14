using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;
	Camera sceneCamera;

	// Use this for initialization
	void Start () {
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
	}
}
