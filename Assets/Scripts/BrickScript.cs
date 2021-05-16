using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "BALL")
		{
			//PaddleScript.SCORE += 50;
			int temp = PlayerPrefs.GetInt("score");
			temp += 50;
			PlayerPrefs.SetInt("score", temp);
			PaddleScript.BRICKSTONEXTLEVEL++;
			Destroy(gameObject);
		}
	}
}
