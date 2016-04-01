using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PauseMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}

	public void ExitToWindows(){
		Application.Quit();
	}

	public void ExitToMainMenu(string scene){
		SceneManager.LoadScene(scene);
	}
}
