using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode
{
	Tilt,
	Touch
}

public interface IInputHandler
{
	InputMode inputMode { get; }
	public float ReturnHorizontalInput();
}
