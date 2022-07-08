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
