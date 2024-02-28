using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pointers : MonoBehaviour
{
	[SerializeField] private Pointer prefab;
	[SerializeField] private float screenSizeDelta;
	[SerializeField] private Vector2Int pointersCounts;
	[SerializeField] private PounceBall pounceBall;
	[SerializeField] private Vector2 speeds;
	private float shootSpeed;
	private int pointersCount;
	private List<Pointer> currentPointers;
	private Pointer currentPointer;
	private Pointer prevPointer;
	public Action OnPoint { get; set; }

	public void StartInit()
	{
		var screenSize = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize);

		shootSpeed = DataPlinkoManager.PlinkoSaves.shootSpeeds ? speeds.y : speeds.x;
		pointersCount = DataPlinkoManager.PlinkoSaves.pointersCount ? pointersCounts.y : pointersCounts.x;

		currentPointers = new List<Pointer>();
		Vector2 pointerPosition = Vector2.zero;
		float deltaAngle = 2 * Mathf.PI / pointersCount;
		float currentAngle = 0;

		for (int i = 0; i < pointersCount; i++)
		{
			pointerPosition.x = screenSize.x * screenSizeDelta * Mathf.Cos(currentAngle);
			pointerPosition.y = screenSize.x * screenSizeDelta * Mathf.Sin(currentAngle);
			var pointer = Instantiate(prefab, pointerPosition, Quaternion.identity, transform);
			pointer.OnBallEnter += OnBallEnter;
			currentPointers.Add(pointer);
			currentAngle += deltaAngle;
		}
	}

	private bool OnBallEnter(Pointer pointer)
	{
		if (pointer == prevPointer)
		{
			return false;
		}

		bool found = false;
		Pointer otherPointer = null;

		while (!found)
		{
			otherPointer = currentPointers[Random.Range(0, currentPointers.Count)];
			if (otherPointer == currentPointer)
			{
				continue;
			}
			else
			{
				found = true;
			}
		}

		currentPointer.SetPointerTarget(false);
		otherPointer.SetPointerTarget(true);
		prevPointer = currentPointer;
		currentPointer = otherPointer;
		pounceBall.Velocity = (otherPointer.transform.position - pounceBall.transform.position).normalized * shootSpeed;
		OnPoint?.Invoke();
		return true;
	}

	public void StartShoot()
	{
		var randomPointer = currentPointers[0];
		currentPointer = randomPointer;
		currentPointer.SetPointerTarget(true);
		prevPointer = currentPointers[pointersCount / 2];

		pounceBall.Velocity = (randomPointer.transform.position - pounceBall.transform.position).normalized * shootSpeed;
	}

	private void OnDestroy()
	{
		foreach (var pointer in currentPointers)
		{
			pointer.OnBallEnter -= OnBallEnter;
		}
	}
}
