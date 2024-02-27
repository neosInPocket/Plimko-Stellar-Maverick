using System.Linq;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
	[SerializeField] private AudioSource volumeSource;
	[SerializeField] private string dontDestroyStringCaption;

	private void Awake()
	{
		var currentAlive = FindObjectsByType<VolumeManager>(sortMode: FindObjectsSortMode.None);

		try
		{
			var destroyObject = currentAlive.FirstOrDefault(x => x.gameObject.scene.name == dontDestroyStringCaption);

			if (destroyObject == null)
			{
				throw new System.Exception();
			}

			if (destroyObject != this)
			{
				Destroy(gameObject);
				return;
			}
		}
		catch
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		TriggerVolumeValues(DataPlinkoManager.PlinkoSaves.volumeMusic);
	}

	public void TriggerVolumeValues(bool enabledOrNot)
	{
		volumeSource.volume = !enabledOrNot ? 0f : 1f;
		DataPlinkoManager.PlinkoSaves.volumeMusic = volumeSource.volume == 0 ? false : true;
	}
}
