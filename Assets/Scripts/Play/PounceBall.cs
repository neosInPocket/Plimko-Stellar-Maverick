using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PounceBall : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float radius;
	[SerializeField] private GameObject destroyEffect;
	[SerializeField] private GameObject glowEffect;
	[SerializeField] private GameObject trailEffect;
	[SerializeField] private SpriteRenderer spriteRenderer;
	private Vector2 screen;
	public Action Destroyed { get; set; }
	public Vector2 Velocity
	{
		get => rb.velocity;
		set => rb.velocity = value;
	}

	private bool destroyed;
	private float currentTime;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		screen = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize);
		transform.position = Vector2.zero;
	}

	public void StartControls()
	{
		Touch.onFingerDown += BallDirectionChange;
	}

	public void DestroyControls()
	{
		Touch.onFingerDown -= BallDirectionChange;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	private void BallDirectionChange(Finger finger)
	{
		DataPlinkoManager.PlinkoSaves.clickedTime++;

		Velocity *= -1;

		RaycastHit2D[] raycast = Physics2D.RaycastAll(transform.position, Vector3.forward);
		var pointerRaycast = raycast.FirstOrDefault(x => x.collider.gameObject.GetComponent<Pointer>() != null);

		if (pointerRaycast.collider != null)
		{
			var pointer = pointerRaycast.collider.GetComponent<Pointer>();
			if (pointer != null)
			{
				pointer.InvokeOnBallEnter();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Astro>(out Astro astro))
		{
			DestroyObject();
		}
	}

	private void Update()
	{
		currentTime += Time.deltaTime;

		if (destroyed) return;

		if (transform.position.x - radius < -screen.x || transform.position.x + radius > screen.x || transform.position.y + radius > screen.y || transform.position.y - radius < -screen.y)
		{
			destroyed = true;
			DestroyObject();
		}
	}

	private void DestroyObject()
	{
		destroyEffect.SetActive(true);
		glowEffect.SetActive(false);
		trailEffect.SetActive(false);
		spriteRenderer.enabled = false;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		Destroyed?.Invoke();
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= BallDirectionChange;
		DataPlinkoManager.PlinkoSaves.timePlayed += currentTime;
		DataPlinkoManager.Save();
	}
}
