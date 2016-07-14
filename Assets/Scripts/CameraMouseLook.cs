using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Used to rotate the camera based on mouse movement. original script came from https://www.youtube.com/watch?v=blO039OzUZc
 */ 
public class CameraMouseLook : MonoBehaviour {
	Vector2 mouseLook; //How much movement the camera has made
	Vector2 smoothV; //this is used to make the movement less jerky. may only need minimal use of this in a twitchy shooter
	public float sensitivity = 5.0f; // mouse sensitivity
	public float smoothingFactor = 2.0f; //smothing factor of motion. basically how fast to lerp



	GameObject playerCharacter;

	// Use this for initialization
	void Start () 
	{
		playerCharacter = this.transform.parent.gameObject;	
	}

	// Update is called once per frame
	void Update () 
	{
		//note: maybe look into the difference between get axis and get axis raw
		Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		mouseDirection = Vector2.Scale (mouseDirection, new Vector2(sensitivity * smoothingFactor, sensitivity * smoothingFactor));
		smoothV.x = Mathf.Lerp (smoothV.x, mouseDirection.x, 1f / smoothingFactor);
		smoothV.y = Mathf.Lerp (smoothV.y, mouseDirection.y, 1f / smoothingFactor);
		mouseLook += smoothV;

		/**
		 * This part is the actual rotating of the camera, notice that in the y mouse axis we only need to rotate the camera,
		 * however in the case of the x axis, we are rotating the player character instead so we can turn
		 */
		float finalYValue = Mathf.Clamp(-mouseLook.y, -90f, 90f);
		transform.localRotation = Quaternion.AngleAxis (finalYValue, Vector3.right);
		playerCharacter.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, playerCharacter.transform.up);

	
	}
}
