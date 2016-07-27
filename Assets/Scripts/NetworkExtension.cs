using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkExtension : NetworkManager {
	public Transform testSpawnPnt;
	public int userCount = 0;

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{

		if(userCount % 2 == 1)
		{
				GameObject player = (GameObject)Instantiate(playerPrefab, testSpawnPnt.position, Quaternion.identity);
				player.layer = LayerMask.NameToLayer("BlueTeam");
				player.GetComponent<PlayerSetup>().myColor = Color.blue;
				NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}
		else
		{
			GameObject player = (GameObject)Instantiate(playerPrefab, testSpawnPnt.position, Quaternion.identity);
			player.layer = LayerMask.NameToLayer("RedTeam");
			player.GetComponent<PlayerSetup>().myColor = Color.red;
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}
		userCount ++;

	}
	public override void OnServerDisconnect (NetworkConnection conn)
	{
		base.OnServerDisconnect (conn);
		userCount --;
	}

}
