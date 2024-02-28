using System;
using UnityEngine;

public class EnterScreen : MonoBehaviour
{
	public Action onEnd { get; set; }

	public void OnAnimationEnd()
	{
		onEnd();
	}
}
