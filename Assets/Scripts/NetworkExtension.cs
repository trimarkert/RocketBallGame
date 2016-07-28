using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkExtension : NetworkManager {
	public Transform[] redSpawnPositions;
	public Transform[] blueSpawnPositions;
	private int userCount = 0;
	private int nextRed = 0;
	private int nextBlue = 0;

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{

		if(userCount % 2 == 1)
		{
				GameObject player = (GameObject)Instantiate(playerPrefab,
			                                            blueSpawnPositions[nextBlue].position,
			                                            blueSpawnPositions[nextBlue].rotation);
				nextBlue ++;
				if(nextBlue >= blueSpawnPositions.Length)
					nextBlue = 0;
				player.layer = LayerMask.NameToLayer("BlueTeam");
				player.GetComponent<PlayerSetup>().myColor = Color.blue;
				NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
				
		}
		else
		{
			GameObject player = (GameObject)Instantiate(playerPrefab,
			                                            redSpawnPositions[nextRed].position,
			                                            redSpawnPositions[nextRed].rotation);
			nextRed ++;
			if(nextRed >= redSpawnPositions.Length)
				nextRed = 0;
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
