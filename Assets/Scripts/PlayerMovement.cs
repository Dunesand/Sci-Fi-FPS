using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private float maxCameraAngle = 85f;
	[SerializeField]
	private float minCameraAngle = 85f;

	private Rigidbody rb;
	private bool onGround;

	Vector3 velocity;
	Vector3 rotation;
	float cameraRotation;
	float currentCameraRotation;
	float jump;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		onGround = true;
	}

	void FixedUpdate()
	{
		PeformMovement ();
		PeformRotation ();
		PeformCameraRotation ();
		PeformJump ();
	}

	void OnCollisionEnter(Collision collision)
	{
		onGround = true;
	}

	public void Move(Vector3 _totalVelocity)
	{
		velocity = _totalVelocity;
	}

	public void Rotate(Vector3 _yRotation)
	{
		rotation = _yRotation;
	}

	public void RotateCamera(float _cameraRotation)
	{
		cameraRotation = _cameraRotation;
	}

	public void Jump(float _jump)
	{
		jump = _jump;
	}
		

	void PeformMovement()
	{
		if (velocity != Vector3.zero) {
			rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
		}
	}

	void PeformRotation()
	{
		rb.MoveRotation (rb.rotation * Quaternion.Euler(rotation));

	}

	void PeformCameraRotation()
	{
		currentCameraRotation -= cameraRotation;
		currentCameraRotation = Mathf.Clamp (currentCameraRotation, minCameraAngle, maxCameraAngle); 
		
		cam.transform.localEulerAngles = new Vector3(currentCameraRotation, 0f, 0f);
	}

	void PeformJump()
	{
		if (jump != 0) {
			if (onGround) {
				rb.AddForce (new Vector3 (0f, jump, 0f));
				onGround = false;
			}
		}
	}
		
}
