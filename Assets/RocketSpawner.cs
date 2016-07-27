using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RocketSpawner : NetworkBehaviour {

	public void spawnObject(GameObject target)
	{
		NetworkServer.Spawn(target);
	}
}
