using UnityEngine;
using UnityEngine.SceneManagement;

public class FramesChanger : MonoBehaviour
{
	[SerializeField] private GameObject startFrame;
	[SerializeField] private Animator animator;
	private GameObject currentFrame;
	private GameObject nextFrame;

	public void Start()
	{
		currentFrame = startFrame;
	}

	public void ChangeTo(GameObject frame)
	{
		nextFrame = frame;
		animator.SetTrigger("change");
	}

	public void ChangeToGame()
	{
		animator.SetTrigger("changetogame");
	}

	public void OnChange()
	{
		nextFrame.SetActive(true);
		currentFrame.SetActive(false);
		currentFrame = nextFrame;
	}

	public void OnChangeToGame()
	{
		SceneManager.LoadScene("Play");
	}
}
