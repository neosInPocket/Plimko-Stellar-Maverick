using System.Collections;
using UnityEngine;

public class Astro : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Rigidbody2D rb;
	public Rigidbody2D Rigid => rb;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name == "Edge")
		{
			Destroy(gameObject);
		}
	}
}
