using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{
	private string userName;
	private bool togglePause = true;
	private bool toggleMusic = false;
	private int tempVolume;
	private int lastLevel;
	private int highestLevel;
	private GameObject placeHolder;
	private GameObject userNameText;
	private GameObject soundOn;
	private GameObject soundOff;
	private List<GameObject> levelButtons;
	private int buttonCount = 20;
	Scene currentScene;
	string sceneName;
	string nextSceneName;
	int lvlID = 0;

	private void Start()
	{
		levelButtons = new List<GameObject>();
		userName = PlayerPrefs.GetString("username");
		tempVolume = PlayerPrefs.GetInt("sound");

		lastLevel = PlayerPrefs.GetInt("lastlevel");
		highestLevel = PlayerPrefs.GetInt("highestlevel");

		placeHolder = GameObject.Find("Placeholder");
		soundOn = GameObject.Find("soundOn");
		soundOff = GameObject.Find("soundOff");

		if (highestLevel == 0 || highestLevel <= 0)
		{
			PlayerPrefs.SetInt("highestlevel", 1);
		}
		else
		{
			highestLevel = PlayerPrefs.GetInt("highestlevel");
		}

		for (int i = 1; i <= buttonCount; i++)
		{
			levelButtons.Add(GameObject.Find(i.ToString()));
		}

		foreach (var buttons in levelButtons)
		{
			if (buttons != null)
			{
				for (int i = 0; i < highestLevel; i++)
				{
					levelButtons[i].GetComponent<Button>().interactable = true;
				}
				for (int i = highestLevel; i < buttonCount; i++)
				{
					levelButtons[i].GetComponent<Button>().interactable = false;
				}
			}
		}

		if (placeHolder != null)
		{
			placeHolder.GetComponent<Text>().text = userName;
		}

		if (soundOn != null && soundOff != null)
		{
			if (PlayerPrefs.GetInt("sound") == 0)
			{
				soundOn.GetComponent<Text>().enabled = false;
				soundOff.GetComponent<Text>().enabled = true;
			}
			if (PlayerPrefs.GetInt("sound") == 1)
			{
				soundOn.GetComponent<Text>().enabled = true;
				soundOff.GetComponent<Text>().enabled = false;
			}
		}

		//Debug.Log(userName + " " + tempVolume);
	}

	public void PlayButton()
	{
		PlayerPrefs.SetInt("lives", 3);
		PlayerPrefs.SetInt("score", 0);
		SceneManager.LoadScene("PlayScene");
	}

	public void SettingsButton()
	{
		SceneManager.LoadScene("SettingsScene");
	}

	public void LeaderboardButton()
	{
		SceneManager.LoadScene("LeaderBoard");
	}

	public void BackButton()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("MenuScene");
	}

	public void ExitButton()
	{
		Application.Quit();
	}

	public void ApplyButton()
	{
		userNameText = GameObject.Find("userNameText");
		if (userNameText.GetComponent<Text>().text == "")
		{

		}
		else
		{
			PlayerPrefs.SetString("username", userNameText.GetComponent<Text>().text);
		}
		PlayerPrefs.SetInt("sound", tempVolume);
	}

	public void ToggleMusic()
	{
		if (toggleMusic)
		{
			toggleMusic = false;
			tempVolume = 0;
			soundOn.GetComponent<Text>().enabled = false;
			soundOff.GetComponent<Text>().enabled = true;
		}
		else
		{
			toggleMusic = true;
			tempVolume = 1;
			soundOn.GetComponent<Text>().enabled = true;
			soundOff.GetComponent<Text>().enabled = false;
		}
	}

	public void PauseButton()
	{
		if (togglePause)
		{
			togglePause = false;
			FindObjectOfType<PaddleScript>().PauseGame();
		}
		else
		{
			togglePause = true;
			FindObjectOfType<PaddleScript>().ResumeGame();
		}
	}

	public void SelectLevel()
	{
		PlayerPrefs.SetInt("lives", 3);
		PlayerPrefs.SetInt("score", 0);
		string setLevel = "Level" + EventSystem.current.currentSelectedGameObject.name;
		PlayerPrefs.SetInt("lastlevel", Int32.Parse(EventSystem.current.currentSelectedGameObject.name));
		SceneManager.LoadScene(setLevel);
	}

	public void NextLevel()
	{
		PaddleScript.ISSTARTED = false;
		currentScene = SceneManager.GetActiveScene();
		sceneName = Regex.Match(currentScene.name, @"\d+").Value;
		nextSceneName = "Level" + (Int32.Parse(sceneName) + 1);
		PlayerPrefs.SetInt("lastlevel", Int32.Parse(sceneName) + 1);

		if (lastLevel >= highestLevel)
		{
			Debug.Log("lastlevel " + PlayerPrefs.GetInt("lastlevel"));
			Debug.Log("highestlevel " + PlayerPrefs.GetInt("highestlevel"));
			PlayerPrefs.SetInt("highestlevel", PlayerPrefs.GetInt("lastlevel"));
		}
		else
		{

		}

		//Debug.Log("Level "+PlayerPrefs.GetInt("lastlevel") + " acıldı.");
		SceneManager.LoadScene(nextSceneName);
	}
}

// TODO 

// EN SON 4. BÖLÜM ACIKKEN 1. BÖLÜME NEXT YAPINCA YLANIZCA 2 BLÖÜM GÖZÜKÜYOR.