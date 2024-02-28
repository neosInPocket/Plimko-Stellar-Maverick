using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowPanel : MonoBehaviour
{
	[SerializeField] private TMP_Text result;
	[SerializeField] private TMP_Text tipText;
	[SerializeField] private TMP_Text rewardAmountText;
	[SerializeField] private Button buttonNext;
	[SerializeField] private Button buttonMenu;
	[SerializeField] private Animator screenAnimator;
	[SerializeField] private EnterScreen enterScreen;

	private void Start()
	{
		buttonNext.onClick.AddListener(LoadAnimationToNext);
		buttonMenu.onClick.AddListener(LoadAnimationToMenu);
	}

	public void ShowPanelResult(int coinsRecievee)
	{
		if (coinsRecievee == 0)
		{
			result.text = "YOU LOSE";
			tipText.text = "TRY TO BUY SOME UPGRADES IN SHOP. GOOD LUCK NEXT TIME";
			rewardAmountText.text = 0.ToString();
		}
		else
		{
			result.text = "LEVEL COMPLETED";
			tipText.text = "CONGRATULATIONS! YOU HAVE UNLOCKED NEXT LEVEL";
			rewardAmountText.text = coinsRecievee.ToString();
		}
	}

	public void LoadAnimationToNext()
	{
		enterScreen.onEnd = LoadNextAnimation;
		screenAnimator.SetTrigger("exit");
	}

	public void LoadAnimationToMenu()
	{
		enterScreen.onEnd = LoadMenuAnimation;
		screenAnimator.SetTrigger("exit");
	}

	private void LoadNextAnimation()
	{
		SceneManager.LoadScene("Play");
	}

	private void LoadMenuAnimation()
	{
		SceneManager.LoadScene("Enter");
	}

	public void OnAnimation()
	{

	}
}
