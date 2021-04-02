using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellTable : MonoBehaviour
{
    public string tagRedBean = "RedBean", tagYellowLife = "YellowLife", tagGreenGrass = "GreenGrass", tagComplex = "Complex";//можно сдеать через массив или словари
    public Text ZAKAZ; //куда выводим текст заказа
    public int RedBean, YellowLife, GreenGrass, Complex;
    public bool status;
    public GameObject player;
    private manager manag;
    public int pay;
    public Text timer;
    public Transform visualTimer;
    public float t = 0, timeLeft = 0;
    // Start is called before the first frame update
    void Start()
    {
        manag = player.GetComponent<manager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (status)
        {
            ZAKAZ.text = "RedBean: " + RedBean + "\n YellowLife: " + YellowLife + "\n GreenGrass: " + GreenGrass + "\n Complex: " + Complex;
        }
        else ZAKAZ.text = "Relax";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagRedBean)
        {
            RedBean--;
            Destroy(other.gameObject);
        }
        else if (other.tag == tagYellowLife)
        {
            YellowLife--;
            Destroy(other.gameObject);
        }
        else if (other.tag == tagGreenGrass)
        {
            GreenGrass--;
            Destroy(other.gameObject);
        }
        else if (other.tag == tagComplex)
        {
            Complex--;
            Destroy(other.gameObject);
        }
        else Destroy(other.gameObject);
    }

    private void Update()
    {
        if (status)
        {
            Timer();
            
        }
    }
    private void Timer()
    {
        //timeLeft = t;
            
               timeLeft -=  Time.deltaTime;
                timer.text = Convert.ToInt32(timeLeft) + " / " + Convert.ToInt32(t);
                wait(10);

        //visualTimer.transform.localScale = new Vector3(timeLeft / t /2, 1.0f, 1.0f);
        visualTimer.GetComponent<Image>().fillAmount = timeLeft / t;
            
            if (timeLeft <= 0)
            {
                aiGoHome();
            }
    }
    public void MakeZakaz(int points)
    {
        pay = 0;
        t = 0;
        RedBean = YellowLife = GreenGrass = Complex = 0;
        //формируем список заказа
        while (points > 0)
        {
            int choice = UnityEngine.Random.Range(0, 4);
            switch (choice)
            {
                case 0:
                    RedBean++;
                    points--;
                    pay += 50;
                    t += 20;
                    break;
                case 1:
                    YellowLife++;
                    points--;
                    pay += 100;
                    t += 20;
                    break;
                case 2:
                    GreenGrass++;
                    points--;
                    pay += 150;
                    t += 20;
                    break;
                case 3:
                    Complex++;
                    points--;
                    pay += 200;
                    t += 20;
                    break;

            }
        }
        status = true;
        timeLeft = t;
        //Timer();
    }
    public void SellZakaz()
    {


        if (RedBean == 0 && YellowLife == 0 && GreenGrass == 0 && Complex == 0 && status)
        {
            manag.money += pay;
            aiGoHome();
        }

    }
    private void aiGoHome()
    {
        status = !status;
        pay = 0;
        GameObject g = GameObject.FindGameObjectWithTag("guest");
        g.GetComponent<AIGuest>().readyDel = true;
    }
    IEnumerator wait(int sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
