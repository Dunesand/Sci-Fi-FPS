using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void PlayGame(string scene){
		SceneManager.LoadScene(scene);
	}
	public void ExitGame(){
		Application.Quit();
	}

}
