using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

	public float moveSpeed = 0.1f;
	public GameObject paddle;

	private Rigidbody2D controllerBody;
	private float ScreenWidth;

	private void Start()
	{
		ScreenWidth = Screen.width;
		controllerBody = paddle.GetComponent<Rigidbody2D>();
	}
	private void Update()
	{
		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).position.x > (ScreenWidth / 2 + 70))
			{
				// move right
				RunController(1f);
			}
			if (Input.GetTouch(0).position.x < (ScreenWidth / 2 - 70))
			{
				// move left
				RunController(-1f);
			}
		}
		else
		{
			controllerBody.velocity = Vector2.zero;
		}
		//int i = 0;
		//// loop over every touch found
		//while (i < Input.touchCount)
		//{
		//	if (Input.GetTouch(i).position.x > ScreenWidth / 2)
		//	{
		//		// move right
		//		RunController(1f);
		//	}
		//	else if (Input.GetTouch(i).position.x < ScreenWidth / 2)
		//	{
		//		// move left
		//		RunController(-1f);
		//	}
		//	else
		//	{
		//		controllerBody.velocity = Vector2.zero;
		//	}

		//	++i;
		//}

	}

	private void FixedUpdate()
	{

#if UNITY_EDITOR
		RunController(Input.GetAxis("Horizontal"));
#endif
	}

	private void RunController(float horizontalInput)
	{
		// move controller		
		controllerBody.velocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0);

		//controllerBody.AddForce(new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0));
	}
}
