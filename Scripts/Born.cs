using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;//生成玩家

    public GameObject[] enemyPrefaList;//生成敌人

    public bool createPlayer;//判断是否生成的是玩家

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);//生成速度
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank()
    {
        if (createPlayer)//如果生成的是玩家
        {                
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {     //如果生成敌人
            int num = Random.Range(0, 2);//随机敌人编号
            Instantiate(enemyPrefaList[num], transform.position, Quaternion.identity);//生成
        }
    }

}
