using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componantsToDisable; //declaring an array
	[SerializeField]
	private GameObject PlayerUIPrefab;
	[HideInInspector]
	public GameObject PlayerUIInstance;

	void Start()
	{
		//Initiate the player UI and equal names
		PlayerUIInstance = Instantiate (PlayerUIPrefab);
		PlayerUIInstance.name = PlayerUIPrefab.name;

		if (!isLocalPlayer) { //if you are not a player
			for (int i = 0; i < componantsToDisable.Length; i++) { //disable all the objects of the componantsToDisable array with a loop
				componantsToDisable [i].enabled = false;
			}
		} else { //if you are a player
			//Lock and hide the cursor
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	void OnDisable()
	{
		//Destroy Player UI
		Destroy(PlayerUIInstance);

		//Unlock and make the cursor reappear
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}


}
