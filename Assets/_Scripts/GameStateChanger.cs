using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEvents.EventHolder;

public enum GameState
{
	MainMenu,
	PauseMenu,
	Playmode
}

public class GameStateChanger : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject playmodeMenu;

	private GameState currentGameState;

	private void Start()
	{
		currentGameState = GameState.MainMenu;
		mainMenu.SetActive(true);
		pauseMenu.SetActive(false);
		playmodeMenu.SetActive(false);
	}

	private void OnEnable()
	{
		onReturnToMainMenu += ChangeToMainMenu;
		onGamePause += ChangeToPauseMenu;
		onGameStart += ChangeToPlaymode;
	}
	private void OnDisable()
	{
		onReturnToMainMenu -= ChangeToMainMenu;
		onGamePause -= ChangeToPauseMenu;
		onGameStart -= ChangeToPlaymode;
	}

	private void ChangeToMainMenu()
	{
		pauseMenu.SetActive(false);
		mainMenu.SetActive(true);

		currentGameState = GameState.MainMenu;
	}
	private void ChangeToPauseMenu()
	{
		Time.timeScale = 0f;
		playmodeMenu.SetActive(false);
		pauseMenu.SetActive(true);

		currentGameState = GameState.PauseMenu;
	}
	private void ChangeToPlaymode()
	{
		if (currentGameState == GameState.MainMenu)
		{
			mainMenu.SetActive(false);
			playmodeMenu.SetActive(true);
		}
		if (currentGameState == GameState.PauseMenu)
		{
			pauseMenu.SetActive(false);
			playmodeMenu.SetActive(true);
		}

		Time.timeScale = 1f;
		currentGameState = GameState.Playmode;
	}
}
