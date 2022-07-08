using UnityEngine;
using static MyEvents.EventHolder;

public class MenuButtons : MonoBehaviour
{
	//����������, ������������ � ������� � ����
	public void StartGame()
	{
		onGameStart?.Invoke();
	}

	public void PauseGame()
	{
		onGamePause?.Invoke();
	}

	public void GoToMainMenu()
	{
		onReturnToMainMenu?.Invoke();
	}

	public void ChangeInputMode()
	{
		onInputModeChange?.Invoke();
	}
}
