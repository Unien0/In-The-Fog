using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bass : MonoBehaviour
{
	private SpriteRenderer sr;
	public GameObject explosionPrefab;


	public Sprite BrokenSprite;

	// Use this for initialization
	void Start()
	{
		sr = GetComponent<SpriteRenderer>();

	}

	// Update is called once per frame
	void Update()
	{

	}


	public void Die()
	{
		sr.sprite = BrokenSprite;
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		OlayerManager.Instance.isDefeat = true;
	
	}
}
