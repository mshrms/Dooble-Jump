using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEvents.EventHolder;

public enum PlatformType
{
	Platform,
	MovingPlatform,
	TrapPlatform
}

public class LevelGenerator : MonoBehaviour
{
	[SerializeField] private PlatformFactory platformFactory;
	[SerializeField] private MovingPlatformFactory movingPlatformFactory;
	[SerializeField] private TrapPlatformFactory trapPlatformFactory;

	[SerializeField] private PlatformType[] genProbability;
	[SerializeField] private int platformsToGenOnStart;

	[SerializeField] private Vector3 startPlatformPosition;
	[SerializeField] private Vector2 platformHorizontalPosRange;
	[SerializeField] private Vector2 platformVerticalPosRange;

	private List<GameObject> generatedPlatforms;
	private Vector3 nextPosition;
	private bool gameInitialized;

	private void OnEnable()
	{
		onGameStart += StartLevelGeneration;
		onReturnToMainMenu += ClearAllPlatforms;
	}

	private void OnDisable()
	{
		onGameStart -= StartLevelGeneration;
		onReturnToMainMenu -= ClearAllPlatforms;
	}

	private void Start()
	{
		gameInitialized = false;
		generatedPlatforms = new List<GameObject>();
	}

	private void StartLevelGeneration()
	{
		if (!gameInitialized)
		{
			GenerateStartPlatform();
			gameInitialized = true;
		}

		while (generatedPlatforms.Count < platformsToGenOnStart)
		{
			GenerateRandomPlatform();
		}
	}


	private void GenerateStartPlatform()
	{
		Platform startPlatformInstance = platformFactory.GetNewInstance(startPlatformPosition);
		generatedPlatforms.Add(startPlatformInstance.gameObject);

		nextPosition = startPlatformPosition;
	}

	private void GenerateRandomPlatform()
	{
		int randomNumber = Random.Range(0, genProbability.Length);
		PlatformType newPlatformType = genProbability[randomNumber];
		GameObject newPlatformInstance = null;

		//центрируем позицию новой платформы по X, сохраняем высоту по Y
		nextPosition.x = 0f;

		//и затем прибавляем разброс позиции
		nextPosition += new Vector3(
			Random.Range(platformHorizontalPosRange.x, platformHorizontalPosRange.y),
			Random.Range(platformVerticalPosRange.x, platformVerticalPosRange.y),
			0f);

		switch (newPlatformType)
		{
			case PlatformType.Platform:
				newPlatformInstance = platformFactory.GetNewInstance(nextPosition).gameObject;
				break;

			case PlatformType.MovingPlatform:
				newPlatformInstance = movingPlatformFactory.GetNewInstance(nextPosition).gameObject;
				break;

			case PlatformType.TrapPlatform:
				newPlatformInstance = trapPlatformFactory.GetNewInstance(nextPosition).gameObject;
				break;

			default:
				Debug.Log("Something is wrong in random platform generation switch.");
				break;
		}

		generatedPlatforms.Add(newPlatformInstance);
	}

	private void ClearAllPlatforms()
	{
		nextPosition = Vector3.zero;

		foreach (var platform in generatedPlatforms)
		{
			Destroy(platform);
		}
		generatedPlatforms.Clear();

		gameInitialized = false;
	}
}
