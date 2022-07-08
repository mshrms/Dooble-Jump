using UnityEngine;

public class TouchInputHandler : IInputHandler
{
	public InputMode inputMode => InputMode.Touch;

	private Camera mainCam;

	public TouchInputHandler()
	{
		mainCam = Camera.main;
	}

	public float ReturnHorizontalInput()
	{
		if (Input.GetMouseButton(0))
		{
			float mouseXPos = mainCam.ScreenToViewportPoint(Input.mousePosition).x - 0.5f;
			return mouseXPos;
		}

		return 0f;
	}
}
