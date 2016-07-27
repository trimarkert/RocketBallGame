using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public BasketManager teamOneBasket;
	public BasketManager teamTwoBasket;
	public int maxScore = 3;
	public Text scoreText;
	void Start()
	{
		teamOneBasket = GameObject.Find("TeamOneBucket").GetComponent<BasketManager>();
		teamTwoBasket = GameObject.Find("TeamTwoBucket").GetComponent<BasketManager>();
	}
	void Update () {
		scoreText.text = "Team One: " + teamOneBasket.getScore() + " Team Two: " + teamTwoBasket.getScore();
		if(teamOneBasket.getScore() >= maxScore || teamTwoBasket.getScore() >= maxScore)
		{
			enabled = false;
		}
	}
}
