using UnityEngine;
using TMPro;
using static MyEvents.EventHolder;

public class InputModeUIDisplayer : MonoBehaviour
{
	[SerializeField] TMP_Text currentInputModeText;
	[SerializeField] TMP_Text changeInputModeButtonText;

	private void OnEnable()
	{
		onInputModeHasChanged += ShowInputModeInUI;
	}
	private void OnDisable()
	{
		onInputModeHasChanged -= ShowInputModeInUI;
	}

	private void Start()
	{
		ShowInputModeInUI();
	}

	private void ShowInputModeInUI()
	{
		int inputModeInt = PlayerPrefs.GetInt("InputMode");
		currentInputModeText.text = "Current Input Mode: " + ((InputMode)inputModeInt).ToString();

		string changeTo;

		if(inputModeInt == 0)
		{
			changeTo = "Touch";
		}
		else
		{
			changeTo = "Tilt";
		}

		changeInputModeButtonText.text = "Change to " + changeTo;
	}
}
