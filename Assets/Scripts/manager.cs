using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public int money = 100,targetMoney=1000;
    public Text tMoney;
    public GameObject SChanger;
    public string end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tMoney.text = "Money: " + money;
        if (money >= targetMoney)
        {
            SChanger.GetComponent<SceneChanger>().SceneLoad(end);
        }
    }
    
}
