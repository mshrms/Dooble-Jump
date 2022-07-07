using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static MyEvents.EventHolder;

public class Player : MonoBehaviour
{
	//запоминаем высоту последнего прыжка, и если игрок упал ниже по Y, чем самая высокая точка прыжка минус
	// оффсет мертвой зоны, то он умер
	public float HighestJumpPoint { get; private set; }

	[SerializeField] private Transform birdSprite;
	[SerializeField] private float rotateDuration;
	[SerializeField] private Ease rotateEasing;

	[SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce;
	[SerializeField] private float inputSensitivity;
	[SerializeField] private float maxHorizontalSpeed;
	[SerializeField] private float deadZoneOffset;

	private float levelBorder;
	private IInputHandler currentInputHandler;
	private IInputHandler[] inputHandlers;
	private float currentSpeed;
	private bool canJump;
	private bool fallingDown;

	private void OnEnable()
	{
		onPlayerJump += Jump;
		onInputModeChange += ChangeInputMode;
	}
	private void OnDisable()
	{
		onPlayerJump -= Jump;
		onInputModeChange += ChangeInputMode;
	}

	private void Awake()
	{
		InitializeInput();
	}

	private void Start()
	{
		fallingDown = true;
		canJump = false;

		levelBorder = FindObjectOfType<LevelGenerator>().LevelBorder;
	}

	private void InitializeInput()
	{
		inputHandlers = new IInputHandler[] { new TiltInputHandler(), new TouchInputHandler() };

		if (PlayerPrefs.HasKey("InputMode"))
		{
			currentInputHandler = inputHandlers[PlayerPrefs.GetInt("InputMode")];
		}
		else
		{
			PlayerPrefs.SetInt("InputMode", 0);
			currentInputHandler = inputHandlers[0];
		}
	}

	private void Update()
	{
		CalculateFall();
		MovePlayerHorizontally(currentInputHandler.ReturnHorizontalInput() * inputSensitivity);
	}

	private void MovePlayerHorizontally(float horizInput)
	{
		float horizontalSpeed = Mathf.Clamp(horizInput, -maxHorizontalSpeed, maxHorizontalSpeed);

		Vector3 newHorizPos = transform.position;
		newHorizPos.x += horizontalSpeed * Time.deltaTime;
		
		//level borders teleporting
		if (newHorizPos.x > levelBorder)
		{
			newHorizPos.x = -levelBorder;
		}
		if (newHorizPos.x < -levelBorder)
		{
			newHorizPos.x = levelBorder;
		}

		//sprite mirroring
		if (horizInput > 0f)
		{
			birdSprite.DOLocalRotate(Vector3.zero, rotateDuration).SetEase(rotateEasing);
		}
		else if (horizInput < 0f)
		{
			birdSprite.DOLocalRotate(new Vector3(0f, 180f, 0f), rotateDuration).SetEase(rotateEasing);
		}

		transform.position = newHorizPos;
	}

	private void CalculateFall()
	{
		if (!canJump)
		{
			//gravity applying
			currentSpeed += (gravityForce * Time.deltaTime);
			Vector3 newVertPosition = new Vector3(0f, currentSpeed * Time.deltaTime);
			transform.position += newVertPosition;

			//highest point
			if (transform.position.y > HighestJumpPoint)
			{
				HighestJumpPoint = transform.position.y;
			}

			//is falling
			if (currentSpeed < 0f)
			{
				fallingDown = true;
			}
			else
			{
				fallingDown = false;
			}

			//death check
			if (transform.position.y < (HighestJumpPoint - deadZoneOffset))
			{
				onPlayerDeath?.Invoke();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (fallingDown)
		{
			if (other.gameObject.CompareTag("TrapPlatform"))
			{
				onTrapPlatformBreak?.Invoke(other.GetComponent<TrapPlatform>());
			}
			else
			{
				canJump = true;
				currentSpeed = 0;

				onPlayerJump?.Invoke();
			}
		}
	}
	
	private void Jump()
	{
		canJump = false;
		fallingDown = false;
		currentSpeed = jumpForce;
	}

	private void ChangeInputMode()
	{
		switch (currentInputHandler.inputMode)
		{
			case InputMode.Tilt:
				currentInputHandler = inputHandlers[1];
				PlayerPrefs.SetInt("InputMode", 1);
				break;
			case InputMode.Touch:
				currentInputHandler = inputHandlers[0];
				PlayerPrefs.SetInt("InputMode", 0);
				break;
		}

		onInputModeHasChanged?.Invoke();
	}

	private void OnDestroy()
	{
		birdSprite.transform.DOKill();
	}
}
