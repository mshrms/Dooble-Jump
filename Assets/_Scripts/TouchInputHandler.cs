using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputHandler : IInputHandler
{
	public InputMode inputMode => InputMode.Touch;

	public float ReturnHorizontalInput()
	{
		if (Input.GetMouseButton(0))
		{
			float mouseXPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f;
			return mouseXPos;
		}

		return 0f;
	}
}
