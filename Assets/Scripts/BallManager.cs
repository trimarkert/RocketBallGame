using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {
	public float startingForce;


	// Use this for initialization
	void Start () {
		if(GetComponent<Rigidbody>() != null)
		{
			Debug.Log("The starting force is: " + startingForce);
			Debug.Log ();
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * startingForce);
		}
	}
	
	public void AddAForce(float forceAmount)
	{
		gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * forceAmount);
	}
}
