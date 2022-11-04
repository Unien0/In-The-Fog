using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//属性值
	public float moveSpeed = 3;//坦克速度
	private Vector3 bulletEulerAngles;
	private float timeVal;//计时器
	private float defendTimeVal = 3;//无敌时间计时器
	private bool isDefended = true;//无敌判断


	//引用
	public Sprite[] tankeSprite;//上 右 下 左
	public GameObject bulletPrefab;//子弹
	private SpriteRenderer sr;
	public GameObject explosionPrefab;//坦克死亡特效
	public GameObject defendEffectPrefab;//无敌特效






	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//是否处于无敌状态
		if (isDefended)
		{
			defendEffectPrefab.SetActive(true);
			defendTimeVal -= Time.deltaTime;
			if (defendTimeVal <= 0)
			{
				isDefended = false;
				defendEffectPrefab.SetActive(false);
			}
		}
		//攻击cd
		if (timeVal >= 0.3f)
		{
			Attack();
		}
		else
		{
			timeVal += Time.deltaTime;
		}


	}

	private void FixedUpdate()
	{
        if (OlayerManager.Instance.isDefeat)
        {
			Destroy(gameObject);
			return;
        }
		Move();


	}


	//坦克的攻击方法
	private void Attack()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//子弹产生的角度：当前坦克方向
			Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles +
				bulletEulerAngles));
			timeVal = 0;
		}
	}


	//坦克的移动方法
	private void Move()
	{
		float h = Input.GetAxisRaw("Horizontal");
		transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime, Space.World);
		if (h < 0)
		{
			sr.sprite = tankeSprite[3];
			bulletEulerAngles = new Vector3(0, 0, 90);
		}
		else if (h > 0)
		{
			sr.sprite = tankeSprite[1];
			bulletEulerAngles = new Vector3(0, 0, -90);
		}

		if (h != 0)
		{
			return;
		}

		float v = Input.GetAxisRaw("Vertical");
		transform.Translate(Vector3.up * v * moveSpeed * Time.deltaTime, Space.World);

		if (v < 0)
		{
			sr.sprite = tankeSprite[2];
			bulletEulerAngles = new Vector3(0, 0, -180);
		}
		else if (v > 0)
		{
			sr.sprite = tankeSprite[0];
			bulletEulerAngles = new Vector3(0, 0, 0);
		}
	}


	//坦克死亡方法
	private void Die()
	{
		if (isDefended)
		{
			return;
		}
		OlayerManager.Instance.isDead = true;

		//产生爆炸特效
		Instantiate(explosionPrefab, transform.position, transform.rotation);

		//死亡
		Destroy(gameObject);
	}
}
