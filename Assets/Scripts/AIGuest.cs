using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGuest : MonoBehaviour
{
    public GameObject target, home;
    private NavMeshAgent agent;
    public GameObject sellPoint;
    public bool readyDel,targetBool;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target");
        home = GameObject.FindGameObjectWithTag("home");
        sellPoint = GameObject.FindGameObjectWithTag("sellpoint");
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (readyDel)
        {
            GoHome();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        targetBool = true;
         if (other.transform.tag=="target"&&readyDel==false) //если подошли к столу заказа
        {
            
            int p = Random.Range(1, 3);
            sellPoint.GetComponent<SellTable>().MakeZakaz(p);
        }else if (other.transform.tag=="home"&&readyDel==true)//если вернулись на точку
        {
            Destroy(gameObject);
        }
    }
    
    public void GoHome()
    {
        readyDel = true;
        agent.SetDestination(home.transform.position);
    }
}
