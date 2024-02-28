using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroSpawner : MonoBehaviour
{
	[SerializeField] private Astro[] prefabs;
	[SerializeField] private Vector2 astroSpeeds;
	[SerializeField] private Vector2 delays;
	[SerializeField] private float screenExtend;
	[SerializeField] private float yCut;

	public bool isSpawnEnabled { get; set; }
	private bool isCurrentlySpawning;
	private Vector2 screen;

	private void Start()
	{
		var screenSize = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize);
		screen.x *= screenExtend;

		screen = screenSize;
	}

	private void Update()
	{
		if (!isSpawnEnabled) return;
		if (isCurrentlySpawning) return;

		StartCoroutine(spawning());
	}

	private IEnumerator spawning()
	{
		isCurrentlySpawning = true;
		var random = Random.Range(0, 3);
		Vector2 randomSpawnPosition = new Vector2();
		if (random == 0)
		{
			randomSpawnPosition.x = -screen.x;
			randomSpawnPosition.y = Random.Range(-screen.y * yCut, screen.y * yCut);
		}
		else
		{
			randomSpawnPosition.x = screen.x;
			randomSpawnPosition.y = Random.Range(-screen.y * yCut, screen.y * yCut);
		}

		var spawned = Instantiate(prefabs[Random.Range(0, prefabs.Length)], randomSpawnPosition, Quaternion.identity, transform);

		if (spawned.transform.position.x < 0)
		{
			spawned.Rigid.velocity = Vector2.right * Random.Range(astroSpeeds.x, astroSpeeds.y);
		}
		else
		{
			spawned.Rigid.velocity = Vector2.left * Random.Range(astroSpeeds.x, astroSpeeds.y);
		}

		yield return new WaitForSeconds(Random.Range(delays.x, delays.y));
		isCurrentlySpawning = false;
	}
}
