using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEvents.EventHolder;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private float yOffset;
	[SerializeField] [Range(0f,1f)] private float lerpSpeed;
	private Player playerInstance;
	private bool hasPlayer;

	private void OnEnable()
	{
		onPlayerSpawned += GetPlayer;
		onPlayerDeath += ClearPlayer;
		onReturnToMainMenu += ClearPlayer;
	}
	private void OnDisable()
	{
		onPlayerSpawned -= GetPlayer;
		onPlayerDeath -= ClearPlayer;
		onReturnToMainMenu -= ClearPlayer;
	}

	private void GetPlayer()
	{
		playerInstance = FindObjectOfType<Player>();
		hasPlayer = true;

		SetCamYPos(yOffset);
	}

	private void ClearPlayer()
	{
		playerInstance = null;
		hasPlayer = false;

		SetCamYPos(0f);
	}

	private void Update()
	{
		if (hasPlayer)
		{
			float newPos = Mathf.Lerp(transform.position.y, playerInstance.HighestJumpPoint + yOffset, lerpSpeed);
			SetCamYPos(newPos);
		}
	}

	private void SetCamYPos(float yPos)
	{
		Vector3 newPos = transform.position;
		newPos.y = yPos;
		transform.position = newPos;
	}
}
