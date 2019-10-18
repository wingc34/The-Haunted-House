using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CnControls;

public class MainMenu : MonoBehaviour {

	void Update () {
		if (CnInputManager.GetButtonDown ("Start"))
			PlayGame ();
		if (CnInputManager.GetButtonDown ("Quit"))
			QuitGame ();
	}

	public void PlayGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
	public void QuitGame(){
		Application.Quit ();
	}
}
