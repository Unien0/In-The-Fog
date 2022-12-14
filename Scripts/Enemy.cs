using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	//属性值
	public float moveSpeed = 3;//移动速度
	private Vector3 bulletEulerAngles;
	private float v;
	private float h;


	//引用
	public Sprite[] tankeSprite;//上 右 下 左
	public GameObject bulletPrefab;//子弹
	private SpriteRenderer sr;
	public GameObject explosionPrefab;//坦克死亡特效

	//计时器
	private float timeVal=0;//计时器
	private float timeValChangeDirection=4;




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
		
		//攻击cd
		if (timeVal >= 3f)
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
		Move();//调用移动方法


	}


	//坦克的攻击方法
	private void Attack()
	{
		
			//子弹产生的角度：当前坦克方向
			Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
			timeVal = 0;
		
	}


	//坦克的移动方法
	private void Move()
	{
        if (timeValChangeDirection>=4)
        {
			int num = Random.Range(0, 8);
            if (num>5)
            {
				v = -1;
				h = 0;
            }
            else if(num==0)
            {
				v = 1;
				h = 0;
            }
            else if (num>0&&num<=2)
            {
				h = -1;
				v = 0;
            }
            else if (num>2&&num<=4)
            {
				h = 1;
				v = 0;

            }

			timeValChangeDirection = 0;
        }
        else
        {
			timeValChangeDirection += Time.fixedDeltaTime;
        }



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
		//得分
		OlayerManager.Instance.playerScore++;

		//产生爆炸特效
		Instantiate(explosionPrefab, transform.position, transform.rotation);

		//死亡
		Destroy(gameObject);
	}
}
