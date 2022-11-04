using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	//����ֵ
	public float moveSpeed = 3;//�ƶ��ٶ�
	private Vector3 bulletEulerAngles;
	private float v;
	private float h;


	//����
	public Sprite[] tankeSprite;//�� �� �� ��
	public GameObject bulletPrefab;//�ӵ�
	private SpriteRenderer sr;
	public GameObject explosionPrefab;//̹��������Ч

	//��ʱ��
	private float timeVal=0;//��ʱ��
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
		
		//����cd
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
		Move();//�����ƶ�����


	}


	//̹�˵Ĺ�������
	private void Attack()
	{
		
			//�ӵ������ĽǶȣ���ǰ̹�˷���
			Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
			timeVal = 0;
		
	}


	//̹�˵��ƶ�����
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


	//̹����������
	private void Die()
	{
		//�÷�
		OlayerManager.Instance.playerScore++;

		//������ը��Ч
		Instantiate(explosionPrefab, transform.position, transform.rotation);

		//����
		Destroy(gameObject);
	}
}
