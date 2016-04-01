using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerNetworkSetup))]

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 10f;
	[SerializeField]
	private float lookSensitivity = 5f;
	[SerializeField]
	private float jumpForce = 6f;

	private PlayerMovement move;
	private PlayerNetworkSetup network;


	private bool _EscPressed;
	private bool activeUI;
	private GameObject inGameMenu;


	void Start()
	{
		move = GetComponent<PlayerMovement>();
		network = GetComponent<PlayerNetworkSetup> ();


		_EscPressed = false;
		activeUI = false;
		inGameMenu = network.PlayerUIInstance.transform.Find ("InGameMenu").gameObject;

	}

	void Update()
	{
		//Check Esc Key and execute action
		bool _EscapeKeyPressed = CheckEscPressed ();
		if(_EscapeKeyPressed){ 
			PauseMenu(); 
		}

		if (!activeUI) { // if a UI is not opened execute movement, else have all movement as zero
			OverallMovement();
		} else {
			OpenUIMovement();
		}
	}

	bool CheckEscPressed(){
		bool _EscapeKey = Input.GetKeyDown(KeyCode.Escape);

		if (_EscapeKey) {
			_EscPressed = true;
		} else {
			_EscPressed = false;
		}

		return _EscPressed;
	}

	void PauseMenu(){
		if(inGameMenu.gameObject.activeSelf != true){
			inGameMenu.gameObject.SetActive(true);
			activeUI = true;

			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			inGameMenu.gameObject.SetActive(false);
			activeUI = false;

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	void OverallMovement()
	{
		//Calculate and apply rotation of the entire player
		Vector3 _yRotation = CalculateRotation ();
		move.Rotate (_yRotation);

		//Calculate and apply rotation of the camera
		float _cameraRotation = CalculateRotationCamera ();
		move.RotateCamera (_cameraRotation);

		//Calculate and Apply Jump
		float _jump = CalculateJump ();
		move.Jump (_jump);

		//Calculate and apply transformation
		Vector3 _totalVelocity = CalculateVelocity ();
		move.Move (_totalVelocity);
	}

	void OpenUIMovement()
	{
		move.Rotate (Vector3.zero);
		move.RotateCamera (0f);
		move.Jump (0f);
		move.Move (Vector3.zero);
	}

	Vector3 CalculateVelocity() //Makes a Vector3 movement from userInput 
	{
			float _xMov = Input.GetAxis ("Horizontal");
			float _zMov = Input.GetAxis ("Vertical");

			Vector3 _horMovement = transform.right * _xMov;
			Vector3 _verMovement = transform.forward * _zMov;

			Vector3 _totalVelocity = (_horMovement + _verMovement) * speed;

			return _totalVelocity;
	}

	Vector3 CalculateRotation() //Makes a Vector for the player Rotation (around Y)
	{
			float _yRot = Input.GetAxisRaw ("Mouse X");

			Vector3 _yRotation = new Vector3 (0f, _yRot, 0f) * lookSensitivity;

			return _yRotation;
	}

	float CalculateRotationCamera() //Makes a float for only the camera rotation (around X)
	{
			float _xRot = Input.GetAxisRaw ("Mouse Y");

			float _cameraRotation = _xRot * lookSensitivity;

			return _cameraRotation;
	}

	float CalculateJump() //Makes a float for a jump force
	{
			float _jumpInput = Input.GetAxisRaw ("Jump");
			float _jump = _jumpInput * jumpForce * 50;

			return _jump;
	}
	
}
