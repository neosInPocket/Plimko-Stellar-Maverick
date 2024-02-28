using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class LevelPlay : MonoBehaviour
{
	[SerializeField] private Pointers pointers;
	[SerializeField] private Train train;
	[SerializeField] private ShowPanel showPanel;
	[SerializeField] private PounceBall pounceBall;
	[SerializeField] private AstroSpawner astroSpawner;
	[SerializeField] private Image progressImage;
	[SerializeField] private Transform continueFrame;
	[SerializeField] private TMP_Text currentLevelCaption;

	private int tempProgress;
	private int goalProgress;
	private int rewardProgress;

	private void Start()
	{
		currentLevelCaption.text = "LEVEL " + DataPlinkoManager.PlinkoSaves.currentPlinkoLevel;
		GetProgressValues();
		SetProgress();


		if (CheckTrain())
		{
			train.TrainCompleted += OnTrainCompleted;
			train.TrainStart();
		}
		else
		{
			OnTrainCompleted();
		}

		pointers.StartInit();


		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void OnTrainCompleted()
	{
		continueFrame.gameObject.SetActive(true);
		Touch.onFingerDown += OnContinue;
	}

	private void OnContinue(Finger finger)
	{
		continueFrame.gameObject.SetActive(false);
		Touch.onFingerDown -= OnContinue;

		pointers.StartShoot();
		pounceBall.StartControls();

		astroSpawner.isSpawnEnabled = true;
		pounceBall.Destroyed += LoseGame;
		pointers.OnPoint += HandlePoint;
	}

	private void LoseGame()
	{
		pounceBall.Destroyed -= LoseGame;
		pointers.OnPoint -= HandlePoint;
		astroSpawner.isSpawnEnabled = false;
		pounceBall.DestroyControls();

		showPanel.ShowPanelResult(0);
		showPanel.gameObject.SetActive(true);

		DataPlinkoManager.PlinkoSaves.deathTime++;
		DataPlinkoManager.Save();
	}

	private void WinGame()
	{
		pounceBall.Destroyed -= LoseGame;
		pointers.OnPoint -= HandlePoint;
		astroSpawner.isSpawnEnabled = false;
		pounceBall.DestroyControls();

		showPanel.ShowPanelResult(rewardProgress);
		showPanel.gameObject.SetActive(true);
		DataPlinkoManager.PlinkoSaves.currentPlinkoLevel++;
		DataPlinkoManager.PlinkoSaves.currentPinkoCoins += rewardProgress;
		DataPlinkoManager.Save();
	}

	private void HandlePoint()
	{
		tempProgress++;

		if (tempProgress == goalProgress)
		{
			WinGame();
		}

		SetProgress();
	}

	private void SetProgress()
	{
		progressImage.fillAmount = (float)tempProgress / (float)goalProgress;
	}

	private void GetProgressValues()
	{
		var currentLevel = DataPlinkoManager.PlinkoSaves.currentPlinkoLevel;
		rewardProgress = (int)(8 * Mathf.Sqrt(currentLevel) + 10);
		goalProgress = (int)(5 * Mathf.Sqrt(currentLevel));
	}

	private bool CheckTrain()
	{
		if (DataPlinkoManager.PlinkoSaves.train)
		{
			DataPlinkoManager.PlinkoSaves.train = false;
			DataPlinkoManager.Save();
			return true;
		}
		else
		{
			return false;
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnContinue;
	}
}
