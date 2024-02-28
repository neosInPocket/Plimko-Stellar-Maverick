using System;
using UnityEngine;

public class Pointer : MonoBehaviour
{
	[SerializeField] private GameObject BallEnterEffect;
	[SerializeField] private GameObject targetEffect;

	public Func<Pointer, bool> OnBallEnter { get; set; }

	public void InvokeOnBallEnter()
	{
		bool value = OnBallEnter(this);

		if (value)
		{
			BallEnterEffect.SetActive(false);
			BallEnterEffect.SetActive(true);
		}
	}

	public void SetPointerTarget(bool value)
	{
		targetEffect.SetActive(value);
	}
}
