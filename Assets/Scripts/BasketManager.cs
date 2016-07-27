using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BasketManager : NetworkBehaviour {
	[SyncVar(hook = "OnScored")]
	public int score = 0;

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Score"))
		{
			CmdIncreaseScore();
			Destroy(other.gameObject);
		}
	}
	[Command]
	void CmdIncreaseScore(){
		score++;
	}

	public int getScore()
	{
		return score;
	}
	public void OnScored(int newScore)
	{
		score = newScore;
	}
}
