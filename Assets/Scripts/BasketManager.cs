using UnityEngine;
using System.Collections;

public class BasketManager : MonoBehaviour {
	
	public int score = 0;

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Score"))
		{
			score++;
			Destroy(other.gameObject);
		}
	}

	public int getScore()
	{
		return score;
	}
}
