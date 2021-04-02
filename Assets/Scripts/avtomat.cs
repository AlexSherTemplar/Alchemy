using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avtomat : MonoBehaviour
{
    //вешается на триггер
    public GameObject[] output; //зелья 0-РБ 1-РГ 2 - РБГ 3 - БГ 4 - мусор
    public Transform spawnPoint; //точка спавна зелий
    public Text parametrs;
    //public Transform triggerCotel;//триггер для отслеживания кол-ва элементов

    public string tagRedDiamond = "red";
    public string tagBlueDiamond = "blue";
    public string tagGlue = "glue";
    public string temp = "low";//температура задается через панель

    public int red, blue, glue;//кол-во ингридиентов
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        parametrs.text = "Temperature: " + temp + "\n RED: " + red + "\n BLUE: " + blue + "\n GLUE: " + glue;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagRedDiamond)
        {
            red++;
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == tagBlueDiamond)
        {
            blue++;
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == tagGlue)
        {
            glue++;
            Destroy(other.gameObject);
        }
        
    }

    public void MixAll()
    {
        //если по условиям проходим - спавним нужное. если нет - мусор
        if (red == 1)
        {
            if (blue == 1)
            {
                if (glue == 1)
                {
                    if (temp == "max")
                    {
                        GameObject gO = Instantiate(output[2], spawnPoint.transform.position, Quaternion.identity);
                        
                    }
                    else
                    {
                        GameObject gO = Instantiate(output[4], spawnPoint.transform.position, Quaternion.identity);
                    }
                }
                else//ред и блу готовка
                {
                    if (temp == "low")
                    {
                        GameObject gO = Instantiate(output[0], spawnPoint.transform.position, Quaternion.identity);
                        
                    }
                    else
                    {
                        GameObject gO = Instantiate(output[4], spawnPoint.transform.position, Quaternion.identity);
                    }
                }

            }
            else if (glue == 1)
            {
                if (temp == "mid")
                {
                    GameObject gO = Instantiate(output[1], spawnPoint.transform.position, Quaternion.identity);
                }
                else
                {
                    GameObject gO = Instantiate(output[4], spawnPoint.transform.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject gO = Instantiate(output[4], spawnPoint.transform.position, Quaternion.identity);
            }
        }
        else if (blue == 1)
        {
            if (glue == 1)
            {
                if (temp == "mid")
                {
                    GameObject gO = Instantiate(output[3], spawnPoint.transform.position, Quaternion.identity);
                }
                else
                {
                    GameObject gO = Instantiate(output[4], spawnPoint.transform.position, Quaternion.identity);
                }
            }


        }
        else
        {
            GameObject gO = Instantiate(output[4], spawnPoint.transform.position, Quaternion.identity);
            
        }
        red = blue = glue = 0;
        
    }
    public void ChangeTemp(int t)
    {
        temp = t == 0 ? "low" : t == 1 ? "mid" : "max";
    }
}
