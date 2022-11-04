using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;//�������

    public GameObject[] enemyPrefaList;//���ɵ���

    public bool createPlayer;//�ж��Ƿ����ɵ������

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);//�����ٶ�
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank()
    {
        if (createPlayer)//������ɵ������
        {                
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {     //������ɵ���
            int num = Random.Range(0, 2);//������˱��
            Instantiate(enemyPrefaList[num], transform.position, Quaternion.identity);//����
        }
    }

}
