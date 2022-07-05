using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEvents.EventHolder;

public class Player : MonoBehaviour
{
	//���������� ������ ���������� ������, � ���� ����� ���� ���� �� Y, ��� ����� ������� ����� ������ �����
	// ������ ������� ����, �� �� ����
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
			//trap platform break logic (�������� � ��������� ������������� ���� ��������� � �������, ����� ��������� ������ �� ����� ������ �������)
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
		// if(�� �� �����) ���������� �������� + ���������� ���������� �����
		// if(�����������) ��������� ��������, ����� �� ��������
		// ����� ��������� ��������, �� ���� �� ����� ������� �����
		yield return null;
	}

	private void StartJump()
	{
		
	}
	private void EndJump()
	{

	}
}
