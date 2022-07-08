using UnityEngine;
using static MyEvents.EventHolder;

public class MenuButtons : MonoBehaviour
{
	//функционал, подключаемый к кнопкам в меню
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
