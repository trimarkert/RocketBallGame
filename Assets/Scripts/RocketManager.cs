using UnityEngine;
using System.Collections;

public class RocketManager : MonoBehaviour {

	public float speed = 10.0f; 
	public float explosionForce = 10.0f; // value used for explosion force
	public float radius = 10.0f; // the radius of the explosion after the rocket has hit something.

	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision){

		Vector3 explosionPosition = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
		foreach(Collider hit in colliders)
		{
			Rigidbody rigidBod = hit.GetComponent<Rigidbody>();
			if(rigidBod != null)
			{
				rigidBod.AddExplosionForce(explosionForce, explosionPosition, radius, 3.0f);
				GameObject.Destroy(this.gameObject);
			}
		}
	}
}
