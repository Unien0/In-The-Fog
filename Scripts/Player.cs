using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//����ֵ
	public float moveSpeed = 3;//̹���ٶ�
	private Vector3 bulletEulerAngles;
	private float timeVal;//��ʱ��
	private float defendTimeVal = 3;//�޵�ʱ���ʱ��
	private bool isDefended = true;//�޵��ж�


	//����
	public Sprite[] tankeSprite;//�� �� �� ��
	public GameObject bulletPrefab;//�ӵ�
	private SpriteRenderer sr;
	public GameObject explosionPrefab;//̹��������Ч
	public GameObject defendEffectPrefab;//�޵���Ч






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
		//�Ƿ����޵�״̬
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
		//����cd
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


	//̹�˵Ĺ�������
	private void Attack()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//�ӵ������ĽǶȣ���ǰ̹�˷���
			Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles +
				bulletEulerAngles));
			timeVal = 0;
		}
	}


	//̹�˵��ƶ�����
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


	//̹����������
	private void Die()
	{
		if (isDefended)
		{
			return;
		}
		OlayerManager.Instance.isDead = true;

		//������ը��Ч
		Instantiate(explosionPrefab, transform.position, transform.rotation);

		//����
		Destroy(gameObject);
	}
}
