using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Train : MonoBehaviour
{
	[SerializeField] private Image pounceArrow;
	[SerializeField] private Image progressArrow;
	[SerializeField] private Image pointerArrow;
	[SerializeField] private TMP_Text phraseText;
	[SerializeField] private Sprite normalSprite;
	[SerializeField] private Sprite closedSprite;
	[SerializeField] private Image characterImage;
	public Action TrainCompleted { get; set; }

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void TrainStart()
	{
		gameObject.SetActive(true);
		Touch.onFingerDown += Soldier;
		phraseText.text = "welcome, soldier!";
	}

	private void Soldier(Finger finger)
	{
		Touch.onFingerDown -= Soldier;
		Touch.onFingerDown += Pounce;
		phraseText.text = "watch your ball! as soon as the level starts, he will fly towards one of several runes";
		characterImage.sprite = closedSprite;
		pounceArrow.gameObject.SetActive(true);
	}

	private void Pounce(Finger finger)
	{
		Touch.onFingerDown -= Pounce;
		Touch.onFingerDown += Pointer;
		phraseText.text = "the target rune is indicated by a yellow aura. Click on the screen at the moment when your ball flies over it to get a point";
		characterImage.sprite = normalSprite;
		pounceArrow.gameObject.SetActive(false);
		pointerArrow.gameObject.SetActive(true);
	}

	private void Pointer(Finger finger)
	{
		Touch.onFingerDown -= Pointer;
		Touch.onFingerDown += Progress;
		phraseText.text = "The level will be completed when you get a certain number of points. For each level completed you will be rewarded in the form of coins!!";
		characterImage.sprite = normalSprite;
		pointerArrow.gameObject.SetActive(false);
		progressArrow.gameObject.SetActive(true);
	}

	private void Progress(Finger finger)
	{
		Touch.onFingerDown -= Progress;
		Touch.onFingerDown += LastPhrase;
		phraseText.text = "if your ball goes off the screen, the game will be over. oh yes, I completely forgot about the meteor shower, which continuously falls throughout the entire level.";
		characterImage.sprite = closedSprite;
		progressArrow.gameObject.SetActive(false);
	}

	private void LastPhrase(Finger finger)
	{
		Touch.onFingerDown -= LastPhrase;
		Touch.onFingerDown += TrainEnded;
		phraseText.text = "be careful! Good luck!";
		characterImage.sprite = normalSprite;
	}

	private void TrainEnded(Finger finger)
	{
		TrainCompleted();
		Touch.onFingerDown -= TrainEnded;
		gameObject.SetActive(false);
	}
}
