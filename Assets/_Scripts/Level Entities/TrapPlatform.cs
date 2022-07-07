using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static MyEvents.EventHolder;

public class TrapPlatform : MonoBehaviour
{
	[SerializeField] private float breakMovePosition;
	[SerializeField] private float breakDuration;
	[SerializeField] private Ease breakEasing;

	private void OnEnable()
	{
		onTrapPlatformBreak += CompareWithBrokenPlatform;
	}
	private void OnDisable()
	{
		onTrapPlatformBreak -= CompareWithBrokenPlatform;
	}

	private void CompareWithBrokenPlatform(TrapPlatform someTrapPlatform)
	{
		if (someTrapPlatform.Equals(this))
		{
			AnimateBreak();
			
		}
	}

	private void AnimateBreak()
	{
		transform.DOMoveY(transform.position.y + breakMovePosition, breakDuration).SetEase(breakEasing);
		transform.DOShakeScale(breakDuration).OnComplete(() => DeletePlatform());
	}
	private void DeletePlatform()
	{
		transform.DOKill();
		Destroy(this.gameObject);
	}
}
