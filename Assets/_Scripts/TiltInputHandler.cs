using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltInputHandler : IInputHandler
{
	public InputMode inputMode => InputMode.Tilt;

	public float ReturnHorizontalInput()
	{
		float input = Input.acceleration.x;
		return input;
	}
}
