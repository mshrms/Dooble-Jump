using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEvents.EventHolder;

public class PlayerSpawner : MonoBehaviour
{
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private Vector3 playerStartPosition;

	private GameObject playerInstance;
	private bool gameInitialized;

	private void OnEnable()
	{
		onGameStart += SpawnPlayer;
		onPlayerDeath += DeletePlayer;
		onReturnToMainMenu += DeletePlayer;
	}
	private void OnDisable()
	{
		onGameStart -= SpawnPlayer;
		onPlayerDeath -= DeletePlayer;
		onReturnToMainMenu -= DeletePlayer;
	}

	private void Start()
	{
		gameInitialized = false;
	}

	private void SpawnPlayer()
	{
		if (!gameInitialized)
		{
			playerInstance = Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);

			gameInitialized = true;
		}
	}

	private void DeletePlayer()
	{
		Destroy(playerInstance);
		playerInstance = null;

		gameInitialized = false;
	}
}
