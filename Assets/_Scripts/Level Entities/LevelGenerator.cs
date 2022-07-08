using System.Collections.Generic;
using UnityEngine;
using static MyEvents.EventHolder;
using DG.Tweening;

public enum PlatformType
{
	Platform,
	MovingPlatform,
	TrapPlatform
}

public class LevelGenerator : MonoBehaviour
{
	//доступно для проверок движущихся платформ и появления персонажа с другой стороны экрана
	public float LevelWidth { get; private set; }

	[SerializeField] private float levelWidth;
	[SerializeField] private float platformDeleteDistance;

	[SerializeField] ObjectPool objectPool;

	[SerializeField] private PlatformType[] genProbability;
	[SerializeField] private int desiredPlatformCount;

	[SerializeField] private Vector3 startPlatformPosition;
	[SerializeField] private Vector2 platformHorizontalPosRange;
	[SerializeField] private Vector2 platformVerticalPosRange;
	[SerializeField] private Vector2 safetyPlatformVerticalPosRange;

	private List<GameObject> generatedPlatforms;
	private Vector3 nextPosition;
	private bool gameInitialized;

	private Player playerInstance;
	private bool hasPlayer;

	private void OnEnable()
	{
		onGameStart += StartLevelGeneration;
		onReturnToMainMenu += ClearAllPlatforms;

		onPlayerSpawned += GetPlayer;
		onPlayerDeath += ClearPlayer;
		onReturnToMainMenu += ClearPlayer;

		onTrapPlatformBreak += DeleteBrokenTrapPlatform;
	}

	private void OnDisable()
	{
		onGameStart -= StartLevelGeneration;
		onReturnToMainMenu -= ClearAllPlatforms;

		onPlayerSpawned -= GetPlayer;
		onPlayerDeath -= ClearPlayer;
		onReturnToMainMenu -= ClearPlayer;

		onTrapPlatformBreak -= DeleteBrokenTrapPlatform;
	}

	private void Awake()
	{
		LevelWidth = levelWidth;
	}
	private void Start()
	{
		gameInitialized = false;
		generatedPlatforms = new List<GameObject>();
	}

	private void Update()
	{
		if (hasPlayer)
		{
			if (generatedPlatforms.Count < desiredPlatformCount)
			{
				GenerateRandomPlatform();
			}
			else
			{
				TryDeleteLowestPlatforms();
			}
		}
	}

	private void GetPlayer()
	{
		playerInstance = FindObjectOfType<Player>();
		hasPlayer = true;
	}

	private void ClearPlayer()
	{
		playerInstance = null;
		hasPlayer = false;
	}

	private void StartLevelGeneration()
	{
		if (!gameInitialized)
		{
			GenerateStartPlatform();
			gameInitialized = true;
		}

		while (generatedPlatforms.Count < desiredPlatformCount)
		{
			GenerateRandomPlatform();
		}
	}

	private void GenerateStartPlatform()
	{
		Platform startPlatformInstance = objectPool.platforms.Get();
		startPlatformInstance.transform.position = startPlatformPosition;

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

		//и затем прибавляем рандомные отступы для новой позиции
		nextPosition += new Vector3(
			Random.Range(platformHorizontalPosRange.x, platformHorizontalPosRange.y),
			Random.Range(platformVerticalPosRange.x, platformVerticalPosRange.y),
			0f);

		switch (newPlatformType)
		{
			case PlatformType.Platform:
				newPlatformInstance = objectPool.platforms.Get().gameObject;
				newPlatformInstance.transform.position = nextPosition;
				break;

			case PlatformType.MovingPlatform:
				newPlatformInstance = objectPool.movingPlatforms.Get().gameObject;
				newPlatformInstance.transform.position = nextPosition;
				break;

			case PlatformType.TrapPlatform:
				newPlatformInstance = objectPool.trapPlatforms.Get().gameObject;
				newPlatformInstance.transform.position = nextPosition;

				AddSafetyPlatform(nextPosition);
				break;

			default:
				Debug.Log("Something is wrong in random platform generation switch.");
				break;
		}

		generatedPlatforms.Add(newPlatformInstance);
	}

	//добавить рядом с ловушкой обычную платформу чтобы оставить уровень проходимым
	private void AddSafetyPlatform(Vector3 position)
	{
		position += new Vector3(
			Random.Range(platformHorizontalPosRange.x, platformHorizontalPosRange.y),
			Random.Range(safetyPlatformVerticalPosRange.x, safetyPlatformVerticalPosRange.y),
			0f);

		GameObject safetyPlatformInstance = objectPool.platforms.Get().gameObject;
		safetyPlatformInstance.transform.position = position;

		generatedPlatforms.Add(safetyPlatformInstance);
	}

	private void TryDeleteLowestPlatforms()
	{
		while ((playerInstance.HighestJumpPoint - generatedPlatforms[0].transform.position.y) 
			> platformDeleteDistance)
		{
			var platformToDelete = generatedPlatforms[0];
			generatedPlatforms.Remove(platformToDelete);

			platformToDelete.transform.DOKill();

			ChooseCorrectPoolOnDestroy(platformToDelete);
		}
	}
	private void ClearAllPlatforms()
	{
		nextPosition = Vector3.zero;

		foreach (var platform in generatedPlatforms)
		{
			platform.transform.DOKill();
			ChooseCorrectPoolOnDestroy(platform);
		}
		generatedPlatforms.Clear();

		gameInitialized = false;
	}

	private void DeleteBrokenTrapPlatform(TrapPlatform platformToDelete)
	{
		generatedPlatforms.Remove(platformToDelete.gameObject);
	}

	private void ChooseCorrectPoolOnDestroy(GameObject platformToDelete)
	{
		switch (platformToDelete.tag)
		{
			case "Platform":
				objectPool.platforms.Release(platformToDelete.GetComponent<Platform>());
				break;

			case "MovingPlatform":
				objectPool.movingPlatforms.Release(platformToDelete.GetComponent<MovingPlatform>());
				break;

			case "TrapPlatform":
				objectPool.trapPlatforms.Release(platformToDelete.GetComponent<TrapPlatform>());
				break;
		}
	}
}
