using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlayerManager : MonoBehaviour
{

    //ÊôÐÔ
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;

    public GameObject born;

    //µ¥Àý
    private static OlayerManager instance;

    public static OlayerManager Instance { 
        get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Recover();
        }
    }

    private void Recover()
    {
        if (lifeValue <= 0)
        {

        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born,new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
}
