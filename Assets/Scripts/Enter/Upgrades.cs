using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
	[SerializeField] private TMP_Text purchasedTextFirst;
	[SerializeField] private TMP_Text purchasedTextSecond;
	[SerializeField] private TMP_Text buttonTextFirst;
	[SerializeField] private TMP_Text buttonTextSecond;
	[SerializeField] private Button buttonFirst;
	[SerializeField] private Button buttonSecond;
	[SerializeField] private Color redColor;
	[SerializeField] private Color greenColor;
	[SerializeField] private int firstUpgadeCost;
	[SerializeField] private int secondUpgadeCost;
	[SerializeField] private TMP_Text coinsAmountText;


	private void Start()
	{
		RefreshUpgrades();
	}

	public void RefreshUpgrades()
	{
		bool firstUpgrade = DataPlinkoManager.PlinkoSaves.upgrade1;
		bool secondUpgrade = DataPlinkoManager.PlinkoSaves.upgrade2;
		int currentCoins = DataPlinkoManager.PlinkoSaves.currentPinkoCoins;

		RefreshOne(firstUpgrade, firstUpgadeCost, purchasedTextFirst, buttonTextFirst, buttonFirst);
		RefreshOne(secondUpgrade, secondUpgadeCost, purchasedTextSecond, buttonTextSecond, buttonSecond);

		coinsAmountText.text = currentCoins.ToString();
	}

	private void RefreshOne(bool upgrade, int cost, TMP_Text purchasedText, TMP_Text buttonText, Button button)
	{
		bool firstPurchased = upgrade;
		bool firstHasCoins = DataPlinkoManager.PlinkoSaves.currentPinkoCoins >= cost;

		if (firstPurchased)
		{
			purchasedText.text = "YES";
			purchasedText.color = greenColor;
			button.interactable = false;
			buttonText.text = "PURCHASED";
		}
		else
		{
			if (firstHasCoins)
			{
				purchasedText.text = "NO";
				purchasedText.color = redColor;
				button.interactable = true;
				buttonText.text = "PURCHASE";
			}
			else
			{
				purchasedText.text = "NO";
				purchasedText.color = redColor;
				button.interactable = false;
				buttonText.text = "NO COINS";
			}
		}
	}

	public void PurchaseOne(bool firstUpgrade)
	{
		if (firstUpgrade)
		{
			DataPlinkoManager.PlinkoSaves.currentPinkoCoins -= firstUpgadeCost;
			DataPlinkoManager.PlinkoSaves.upgrade1 = true;
			DataPlinkoManager.Save();
		}
		else
		{
			DataPlinkoManager.PlinkoSaves.currentPinkoCoins -= secondUpgadeCost;
			DataPlinkoManager.PlinkoSaves.upgrade2 = true;
			DataPlinkoManager.Save();
		}

		RefreshUpgrades();
	}
}
