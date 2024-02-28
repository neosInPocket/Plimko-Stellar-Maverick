using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeSounds : MonoBehaviour
{
	[SerializeField] private List<AudioSource> sounds;

	private void Start()
	{
		foreach (var sound in sounds)
		{
			sound.enabled = DataPlinkoManager.PlinkoSaves.volumeEffects;
		}
	}
}
