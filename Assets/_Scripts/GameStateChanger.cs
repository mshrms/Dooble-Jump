using UnityEngine;
using static MyEvents.EventHolder;

public enum GameState
{
	MainMenu,
	PauseMenu,
	Playmode,
	GameOver
}

public class GameStateChanger : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject playmodeMenu;
	[SerializeField] private GameObject gameOverMenu;

	private GameState currentGameState;

	private void OnEnable()
	{
		onReturnToMainMenu += ChangeToMainMenu;
		onGamePause += ChangeToPauseMenu;
		onGameStart += ChangeToPlaymode;
		onPlayerDeath += ChangeToGameOver;
	}
	private void OnDisable()
	{
		onReturnToMainMenu -= ChangeToMainMenu;
		onGamePause -= ChangeToPauseMenu;
		onGameStart -= ChangeToPlaymode;
		onPlayerDeath -= ChangeToGameOver;
	}

	private void Start()
	{
		currentGameState = GameState.MainMenu;

		mainMenu.SetActive(true);
		pauseMenu.SetActive(false);
		playmodeMenu.SetActive(false);
		gameOverMenu.SetActive(false);
	}

	private void ChangeToMainMenu()
	{
		pauseMenu.SetActive(false);
		mainMenu.SetActive(true);
		gameOverMenu.SetActive(false);

		currentGameState = GameState.MainMenu;
	}
	private void ChangeToPauseMenu()
	{
		Time.timeScale = 0f;

		playmodeMenu.SetActive(false);
		pauseMenu.SetActive(true);
		gameOverMenu.SetActive(false);

		currentGameState = GameState.PauseMenu;
	}
	private void ChangeToPlaymode()
	{
		switch (currentGameState)
		{
			case GameState.MainMenu:
				mainMenu.SetActive(false);
				break;

			case GameState.PauseMenu:
				pauseMenu.SetActive(false);
				break;

			case GameState.GameOver:
				gameOverMenu.SetActive(false);
				break;
		}

		playmodeMenu.SetActive(true);

		Time.timeScale = 1f;

		currentGameState = GameState.Playmode;
	}
	private void ChangeToGameOver()
	{
		playmodeMenu.SetActive(false);
		gameOverMenu.SetActive(true);
	}
}
