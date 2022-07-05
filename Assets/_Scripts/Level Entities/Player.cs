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

	private float acceleration;
    private float currentJumpDuration;
	private bool wasOnSurface;

	private void OnEnable()
	{
		onPlayerJump += StartJump;
		onPlayerLand += EndJump;
	}
	private void OnDisable()
	{
		onPlayerJump -= StartJump;
		onPlayerLand -= EndJump;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("TrapPlatform"))
		{
			//trap platform break logic (возможно с передачей столкнувшейся треп платформы в событии, чтобы разрушить именно ее после вызова события)
		}
		onPlayerJump?.Invoke();
	}

	

	//private void CheckForSurface()
	//{

	//}
	private void CheckForDeath()
	{

	}
	private IEnumerator CalculateJump()
	{
		// if(не на земле) продолжить рассчеты + запоминать высочайшую точку
		// if(приземлился) закончить рассчеты, выйти из корутины
		// также проводить рассчеты, не упал ли игрок слишком низко
		yield return null;
	}

	private void StartJump()
	{
		
	}
	private void EndJump()
	{

	}
}
