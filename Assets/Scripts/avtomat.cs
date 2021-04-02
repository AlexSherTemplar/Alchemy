using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avtomat : MonoBehaviour
{
    //�������� �� �������
    public GameObject[] output; //����� 0-�� 1-�� 2 - ��� 3 - �� 4 - �����
    public Transform spawnPoint; //����� ������ �����
    public Text parametrs;
    //public Transform triggerCotel;//������� ��� ������������ ���-�� ���������

    public string tagRedDiamond = "red";
    public string tagBlueDiamond = "blue";
    public string tagGlue = "glue";
    public string temp = "low";//����������� �������� ����� ������

    public int red, blue, glue;//���-�� ������������
    

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
        //���� �� �������� �������� - ������� ������. ���� ��� - �����
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
                else//��� � ��� �������
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
