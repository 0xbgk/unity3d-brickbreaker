using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
	PaddleScript paddleScript;
	void Start()
	{
		paddleScript = FindObjectOfType<PaddleScript>();
	}		

	public void Die()
	{
		PaddleScript.ISSTARTED = false;
		Destroy(gameObject);
		paddleScript.SpawnBall();
	}
}
