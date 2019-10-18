using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CnControls;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {
	public Text txtTime;
	public Text txtScore;
	public Text txtCountDown;
	public Text txtPauseScore;
	public Text txtFinalScore;
    public Text txtFinish;
    public GameObject btnNextLevel;
	public GameObject tutorial;
	public GameObject pauseMenuUI;
	public GameObject finishScreen;
	public float gameTimeInSec = 10f;
	public float timeLeft;
	private int min;
	private int sec;
	private int score;
	private bool gameFinish = false;
	private bool gameStart = false;
	private bool GameIsPaused = false;
	private float countNo = 3.9f;

	// Use this for initialization
	void Start () {
		finishScreen.SetActive (false);
		timeLeft = gameTimeInSec;
		txtCountDown.text = "";
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (tutorial.activeSelf && CnInputManager.GetButtonDown("switch"))
			tutorial.SetActive (false);
		
		if (!tutorial.activeSelf && !gameStart) {
			countNo -= Time.fixedDeltaTime;
			txtCountDown.text = ""+(int)countNo;
		}
		if (countNo <= 1f && !gameStart) {
			Time.timeScale = 1;
			txtCountDown.text = "";
			gameStart = true;
		}
        if(score < 150)    //player must reach to 150 scores so that can go next level.
        {
            btnNextLevel.SetActive(false);
            txtFinish.fontSize = 40;
            txtFinish.text = "You Failed! Try Again";
        }
        else
        {
            btnNextLevel.SetActive(true);
            txtFinish.fontSize = 80;
            txtFinish.text = "Finish!";
        }


		timeLeft -= Time.deltaTime;
		if(min>=0)
		txtTime.text = min+" : "+sec;
		txtScore.text = "Score: " + score;
		txtPauseScore.text = "Score: " + score;
		sec = (int)Mathf.Floor(timeLeft % 60);
		min = (int)Mathf.Floor(timeLeft / 60);
		gameOver();

		if (gameFinish) {
			finishScreen.SetActive (true);
			txtFinalScore.text = "SCORE:" + score;
			Time.timeScale = 0;
			gameFinish = false;
		}
	
		InputSystem ();

	}
	void gameOver(){
		if (timeLeft <= 0f) {
			gameFinish = true;
		}
	}

	public void addScore(){
		score += 10;
	}
    public void addBounsScore()
    {
        score += 30;
    }

	void Resume(){
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1;
		GameIsPaused = false;
	}
	void Pause(){
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0;
		GameIsPaused = true;
	}

	void Tip(){
		tutorial.SetActive (true);
	}
	void LoadMenu(){
		SceneManager.LoadScene ("Start Menu");
	}
	void QuitGame(){
		Application.Quit ();
	}
	void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
	void NextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
	void InputSystem(){
		if (CnInputManager.GetButtonDown ("Pause") && gameStart && !gameFinish) {
			Pause ();
		}
		if (CnInputManager.GetButtonDown ("Resume")) {
			Resume ();
		}
		if (CnInputManager.GetButtonDown ("Menu")) {
			LoadMenu ();
		}
		if (CnInputManager.GetButtonDown ("Tip")) {
			Tip ();
		}
		if (CnInputManager.GetButtonDown ("Quit")) {
			QuitGame ();
		}
		if (CnInputManager.GetButtonDown ("Restart")) {
			RestartGame();
		}
		if (CnInputManager.GetButtonDown ("NextLevel")) {
			print (SceneManager.sceneCount);
			if(SceneManager.GetActiveScene ().buildIndex + 1 < 3)
				NextLevel();
			else LoadMenu ();

		}
	}
}
