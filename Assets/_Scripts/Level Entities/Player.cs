using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEvents.EventHolder;

public class Player : MonoBehaviour
{
	//запоминаем высоту последнего прыжка, и если игрок упал ниже по Y, чем самая высокая точка прыжка минус
	// оффсет мертвой зоны, то он умер
	public float HighestJumpPoint { get; private set; }

	[SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce;
    [SerializeField] private float deadZoneOffset;

	private float currentSpeed;
	private bool canJump;
	private bool fallingDown;

	private void OnEnable()
	{
		onPlayerJump += Jump;
	}
	private void OnDisable()
	{
		onPlayerJump -= Jump;
	}

	private void Start()
	{
		fallingDown = true;
		canJump = false;
	}

	private void Update()
	{
		CalculateFall();
	}

	private void CalculateFall()
	{
		if (!canJump)
		{
			//gravity applying
			currentSpeed += (gravityForce * Time.deltaTime);
			Vector3 newPosition = new Vector3(0f, currentSpeed * Time.deltaTime);
			transform.position += newPosition;

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
			//if (transform.position.y < (HighestJumpPoint - deadZoneOffset))
			//{
			//	onPlayerDeath?.Invoke();
			//}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (fallingDown)
		{
			if (other.gameObject.CompareTag("TrapPlatform"))
			{
				//trap platform break logic (возможно с передачей столкнувшейся треп платформы в событии, чтобы разрушить именно ее после вызова события)
			}

			canJump = true;
			currentSpeed = 0;

			onPlayerJump?.Invoke();
		}
		
	}
	
	private void Jump()
	{
		Debug.Log("Jump start");

		canJump = false;
		fallingDown = false;
		currentSpeed = jumpForce;
	}
}
