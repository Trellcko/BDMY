using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KleroSats : MonoBehaviour
{
    public int MyMoney;
    public bool liveinHotel = false;//змінна для провірки чи жиє вінв готелі
    public bool NeverInHotel = false;
    void Start()
    {
        transform.position = new Vector2(-8.85f, -2.6f);//Спавн Клеро
        MyMoney = Random.Range(17, 25);// кілкість гроше у монстра
    }
    public Vector2 MonstMove_Plus = new Vector2(1, 0);// Вектор для начального руху клеро
    public Vector2 MonstMove_Minus = new Vector2(-1, 0);
    void Update()
    {
        if (liveinHotel == false)
        {
            if(NeverInHotel==false)

            transform.Translate(MonstMove_Plus * Time.deltaTime);// рух клеро до Reception
            else if (NeverInHotel==true)
                transform.Translate(MonstMove_Minus*Time.deltaTime);// прочь из готеля

            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Reception" && gameObject.tag == "NotInHotel")//провирка обекта чи є він в готелли
        {
            liveinHotel = true;//зупинка руху
            Invoke("ChekYourMoney", 1.5f);

        }
        if (collision.tag == "Room1" && gameObject.tag == "InHotel")
        {
            gameObject.SetActive(false);
            liveinHotel = true;
        }
    }
    void ChekYourMoney()
    {
        if (MyMoney >= 20)// достаточно в нього гроше
        {
            liveinHotel = false;// рух до комнати
            gameObject.tag = "InHotel";
        }
        else
        {
            NeverInHotel = true;
            liveinHotel = false;
        }
    }
}