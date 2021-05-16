using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PaddleScript : MonoBehaviour
{

	enum BrickCounter
	{
		Level1 = 11,
		Level2 = 14,
		Level3 = 28,
		Level4 = 5,
		Level5 = 17,
		Level6 = 17,
		Level7 = 12,
		Level8 = 16,
		Level9 = 16,
		Level10 = 16,
		Level11 = 13,
		Level12 = 19,
		Level13 = 18,
		Level14 = 24,
		Level15 = 20,
		Level16 = 16,
		Level17 = 12,
		Level18 = 39,
		Level19 = 24,
		Level20 = 24,
	}
	float maxX, minX;
	float ballSpeed;
	int bricksToHit;
	int SCORE;
	int LIVES;
	public static int BRICKSTONEXTLEVEL;
	public static bool ISSTARTED = false;

	public Text scoreText;

	public Text live_1;
	public Text live_2;
	public Text live_3;
	List<Text> lives = new List<Text>();

	public GameObject ballPrefab;
	public Canvas isPaused;
	public Canvas LevelCleared;
	public Canvas GameOver;
	GameObject theBall = null;
	private bool runOnce;
	private int currentScene;

	void Start()
	{
		runOnce = true;
		GameOver.GetComponent<Canvas>().enabled = false;

		currentScene = Int32.Parse(Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value);
		SetBrickCount();

		Time.timeScale = 1;
		BRICKSTONEXTLEVEL = 0;
		minX = -4.3f;
		maxX = +4.3f;
		ballSpeed = 300f;
		SCORE = PlayerPrefs.GetInt("score");
		LIVES = PlayerPrefs.GetInt("lives");
		live_1.GetComponent<Text>().enabled = false;
		live_2.GetComponent<Text>().enabled = false;
		live_3.GetComponent<Text>().enabled = false;

		lives.Add(live_1);
		lives.Add(live_2);
		lives.Add(live_3);

		SpawnBall();
	}
	void Update()
	{
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, minX, maxX);
		transform.position = pos;

		SCORE = PlayerPrefs.GetInt("score");
		LIVES = PlayerPrefs.GetInt("lives");
		scoreText.text = SCORE.ToString();
		DrawLives(LIVES);
		if (LIVES == 0)
		{
			if (runOnce)
			{
				Time.timeScale = 0;

				GameOver.GetComponent<Canvas>().enabled = true;
				GameObject.Find("lastscore").GetComponent<Text>().text = PlayerPrefs.GetInt("score").ToString();
				Debug.Log(PlayerPrefs.GetString("username") + " " + PlayerPrefs.GetInt("score"));
				UIHandler handler = new UIHandler();
				handler.AddScore();
				runOnce = false;
			}
		}

		if (BRICKSTONEXTLEVEL == bricksToHit)
		{
			Time.timeScale = 0;
			LevelCleared.enabled = true;
		}
	}
	private int SetBrickCount()
	{
		switch (currentScene)
		{
			case 1:
				{
					bricksToHit = (int)BrickCounter.Level1;
					break;
				}
			case 2:
				{
					bricksToHit = (int)BrickCounter.Level2;
					break;
				}
			case 3:
				{
					bricksToHit = (int)BrickCounter.Level3;
					break;
				}
			case 4:
				{
					bricksToHit = (int)BrickCounter.Level4;
					break;
				}
			case 5:
				{
					bricksToHit = (int)BrickCounter.Level5;
					break;
				}
			case 6:
				{
					bricksToHit = (int)BrickCounter.Level6;
					break;
				}
			case 7:
				{
					bricksToHit = (int)BrickCounter.Level7;
					break;
				}
			case 8:
				{
					bricksToHit = (int)BrickCounter.Level8;
					break;
				}
			case 9:
				{
					bricksToHit = (int)BrickCounter.Level9;
					break;
				}
			case 10:
				{
					bricksToHit = (int)BrickCounter.Level10;
					break;
				}
			case 11:
				{
					bricksToHit = (int)BrickCounter.Level11;
					break;
				}
			case 12:
				{
					bricksToHit = (int)BrickCounter.Level12;
					break;
				}
			case 13:
				{
					bricksToHit = (int)BrickCounter.Level13;
					break;
				}
			case 14:
				{
					bricksToHit = (int)BrickCounter.Level14;
					break;
				}
			case 15:
				{
					bricksToHit = (int)BrickCounter.Level15;
					break;
				}
			case 16:
				{
					bricksToHit = (int)BrickCounter.Level16;
					break;
				}
			case 17:
				{
					bricksToHit = (int)BrickCounter.Level17;
					break;
				}
			case 18:
				{
					bricksToHit = (int)BrickCounter.Level18;
					break;
				}
			case 19:
				{
					bricksToHit = (int)BrickCounter.Level19;
					break;
				}
			case 20:
				{
					bricksToHit = (int)BrickCounter.Level20;
					break;
				}
			default:
				{
					Debug.Log("this is not happening");
					break;
				}
		}
		//Debug.Log(bricksToHit + " block to finish game");

		return bricksToHit;
	}
	public void Fire()
	{
		if (ISSTARTED == false)
		{
			if (theBall)
			{
				theBall.GetComponent<Rigidbody2D>().isKinematic = false;
				theBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(ballSpeed, ballSpeed));
			}
		}
		ISSTARTED = true;
	}

	public void SpawnBall()
	{
		Vector2 ballPosition = transform.position + new Vector3(0, 0.5f, 0);
		Quaternion ballRotation = Quaternion.identity;
		theBall = (GameObject)Instantiate(ballPrefab, ballPosition, ballRotation);
		theBall.GetComponent<Rigidbody2D>().isKinematic = true;
		theBall.GetComponent<Rigidbody2D>().transform.SetParent(GameObject.Find("Paddle").transform);
	}

	public void DrawLives(int count)
	{
		for (int i = 0; i < 3; i++)
		{
			lives[i].GetComponent<Text>().enabled = false;
		}
		for (int i = 0; i < count; i++)
		{
			lives[i].GetComponent<Text>().enabled = true;
		}
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
		isPaused.GetComponent<Canvas>().enabled = true;
	}

	public void ResumeGame()
	{
		Time.timeScale = 1;
		isPaused.GetComponent<Canvas>().enabled = false;
	}
}
