using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsFrame : MonoBehaviour
{
	[SerializeField] private Color enabledColor;
	[SerializeField] private Color disabledColor;
	[SerializeField] private TMP_Text playTime;
	[SerializeField] private TMP_Text clicks;
	[SerializeField] private TMP_Text deaths;
	[SerializeField] private Image volumeImage;
	[SerializeField] private Image effectsVolumeImage;
	private VolumeManager volumeManager;
	private bool volumeEnabled;
	private bool effectsVolumeEnabled;

	private void Start()
	{
		volumeManager = FindFirstObjectByType<VolumeManager>();

		volumeEnabled = DataPlinkoManager.PlinkoSaves.volumeMusic;
		effectsVolumeEnabled = DataPlinkoManager.PlinkoSaves.volumeEffects;

		RefreshVolumeValues();
		RefreshStats();
	}

	private void RefreshStats()
	{
		int timePlayed = (int)DataPlinkoManager.PlinkoSaves.timePlayed;
		TimeSpan time = TimeSpan.FromSeconds(timePlayed);
		string text = time.ToString(@"hh\:mm\:ss");

		playTime.text = text;
		clicks.text = DataPlinkoManager.PlinkoSaves.clickedTime.ToString();
		deaths.text = DataPlinkoManager.PlinkoSaves.deathTime.ToString();
	}

	private void RefreshVolumeValues()
	{
		if (volumeEnabled)
		{
			volumeImage.color = enabledColor;
		}
		else
		{
			volumeImage.color = disabledColor;
		}

		if (effectsVolumeEnabled)
		{
			effectsVolumeImage.color = enabledColor;
		}
		else
		{
			effectsVolumeImage.color = disabledColor;
		}
	}

	public void TriggerVolume()
	{
		volumeEnabled = !volumeEnabled;

		volumeManager.TriggerVolumeValues(volumeEnabled);
		RefreshVolumeValues();
	}

	public void TriggerEffectsValues()
	{
		effectsVolumeEnabled = !effectsVolumeEnabled;

		DataPlinkoManager.PlinkoSaves.volumeEffects = effectsVolumeEnabled;
		RefreshVolumeValues();
	}
}
