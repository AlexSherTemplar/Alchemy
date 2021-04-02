using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMakeZakaz : MonoBehaviour
{
    public GameObject sellPoint;
    public bool inside;
    // Start is called before the first frame update
    void Start()
    {
        sellPoint = GameObject.FindGameObjectWithTag("sellpoint");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //не отлавливает касание триггера другим коллайдером
    private void OnTriggerEnter(Collider other)
    {
        inside = true;
        if (other.tag == "guest" &&!other.transform.GetComponent<AIGuest>().readyDel) //если подошли к столу заказа
        {

            int p = Random.Range(1, 3);
            sellPoint.GetComponent<SellTable>().MakeZakaz(p);
        }
        else if (other.tag == "guest"&& other.transform.GetComponent<AIGuest>().readyDel)//если вернулись на точку
        {
            Destroy(other.gameObject);
        }
    }
}
