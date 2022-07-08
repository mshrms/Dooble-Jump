using UnityEngine;
using System.Text;
using TMPro;
using static MyEvents.EventHolder;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text playmodeScoreText;

    private const string highscoreString = "Highscore";
    private int highscore;
    private int score;

	private Player playerInstance;
	private bool hasPlayer;

	private StringBuilder stringBuilder;

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

	void Start()
    {
		if (PlayerPrefs.HasKey(highscoreString))
		{
            highscore = PlayerPrefs.GetInt(highscoreString);
		}
		else
		{
            highscore = 0;
            PlayerPrefs.SetInt(highscoreString, highscore);
		}

		stringBuilder = new StringBuilder();

	}

    void Update()
    {
		if (hasPlayer)
		{
			score = (int)playerInstance.HighestJumpPoint;

			if (score > highscore)
			{
				highscore = score;
				PlayerPrefs.SetInt(highscoreString, highscore);
			}

			stringBuilder.Clear();
			stringBuilder.Append("Score: " + score + "\n" + "Highscore: " + highscore);

			string newText = stringBuilder.ToString();
			playmodeScoreText.text = newText;
			gameOverScoreText.text = newText;
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
}
