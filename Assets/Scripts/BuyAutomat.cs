using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAutomat : MonoBehaviour
{
    public Transform sPoint;
    public GameObject red, blue, glue;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Buy(int obj)
    {

        switch (obj)
        {
            case 0:
                if (manager.GetComponent<manager>().money > 10)
                {
                    Instantiate(red, sPoint.transform.position, Quaternion.identity);
                    manager.GetComponent<manager>().money -= 10;
                }
                break;
            case 1:
                if (manager.GetComponent<manager>().money > 15)
                {
                    Instantiate(blue, sPoint.transform.position, Quaternion.identity);
                    manager.GetComponent<manager>().money -= 15;
                }
                break;
            case 2:
                if (manager.GetComponent<manager>().money > 20)
                {
                    Instantiate(glue, sPoint.transform.position, Quaternion.identity);
                    manager.GetComponent<manager>().money -= 20;
                }
                break;
        }
    }

}
