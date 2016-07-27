using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour {

	public float speed = 10.0f;
	public float jumpForce = 100.0f;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask groundCheckMask;
	public bool grounded;

	private Rigidbody rigid;


	// Use this for initialization
	void Start () 
	{
		if(!isLocalPlayer)
		{
			return;
		}
		grounded = false;
		Cursor.lockState = CursorLockMode.Locked;
		rigid = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if(!isLocalPlayer)
		{
			return;
		}
		//This if else will check whether you have 
		if(Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundCheckMask).Length != 0)
		{
			grounded = true;
		}
		else
			grounded = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if(!isLocalPlayer)
		{
			return;
		}
		//Basic movement and straffing logic. ez pz
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;

		transform.Translate (straffe * Time.deltaTime, 0, translation * Time.deltaTime);

		//jump logic, also easy
		if(Input.GetButtonDown("Jump") && grounded)
		{
			rigid.AddForce(Vector3.up * jumpForce);
		}

		//This is mostly for editor, this should be updated to bring up a menu if we go that far
		if(Input.GetKeyDown("escape"))
			Cursor.lockState = CursorLockMode.None;
	}

}
