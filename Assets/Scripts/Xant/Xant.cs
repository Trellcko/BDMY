using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Xant : MonoBehaviour
{

    public  int RandomLove;
    public bool Loves=false;
    float MoveStop = 2.5f;
    public int MEfloor;
    public bool stopForProv = false;
    float y;
    float x;
    public int d;
    public static bool canInHotel;
    public bool prov;
    int rooms;
    bool move_Plus = true;
    bool move_Minus = false;
    Animator myAnim;
    Color alfa;//прозрачность

    public bool liveinHotel = false;//живе чи не живе в готелы
    public bool NeverInHotel = false;//не сможе эити в готелы
    void Start()
    {
        alfa = gameObject.GetComponent<SpriteRenderer>().color;

        myAnim = gameObject.GetComponent<Animator>();// привязка аниматора
       

        transform.position = new Vector2(-43f, -21.30f);//Спавн 

        gameObject.layer = 10;// зминна столкновени й

    }
    public Vector2 MonstMove_Plus = new Vector2(5, 0);// для  руху вперед
    public Vector2 MonstMove_Minus = new Vector2(-5, 0);// для руху назад










    void Update()
    {
       
        if (Time.timeScale == 2.5f)
        {
            myAnim.speed = 0.5f;
        }
        else if (Time.timeScale == 1.75f)
        {
            myAnim.speed = 0.75f;
        }
        else
        {
            myAnim.speed = 1;
        }
        if (canInHotel == true && prov == true)
        {
          
            
            liveinHotel = true;
            StopCoroutine(ChekYourMoney());
            gameObject.tag = "InHotel";// позщначаємо як гость готеля
            Database.Free[rooms] = false;// займаємо комнату
            move_Plus = true;//двигаемось вперед
            canInHotel = false;
            prov = false;


        }
        if (move_Plus == true)//Якщо не живе в готелы
        {
            transform.Translate(MonstMove_Plus * Time.deltaTime);// рух клеро до Reception

            myAnim.Play("XantMovePlus");


        }
        
        else if (move_Minus == true)// якщо може підніматися вверх
        {

            myAnim.Play("XantMoveMinus");

            transform.Translate(MonstMove_Minus * Time.deltaTime);//змінна руху
        }
    }





  
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "StairsUp")//при столкновении с лестницой
        {
            StartCoroutine(AlfaUp());
        }
        if (collision.tag == "Reception" && gameObject.tag == "NotInHotel")//провирка обекта чи є він в готелли
        {
            move_Plus = false;//зупинка руху

            StartCoroutine(ChekYourMoney());// метод проверки денег

        }

        if (collision.name == "door 1 " + "(" + d + ")")// вход в дом
        {

            move_Minus = false;//зупинка руху 
            StartCoroutine(MyRoom());//запуск корунтин для входу в дом
        }

        if (collision.tag == "delet" && NeverInHotel == true)//видалення не потрибниз
        {
            Destroy(gameObject);//видалення
        }


    }

    public IEnumerator MyRoom()
    {

        foreach (GameObject doors in Database.Doors)// пошук потрибнои двери
        {
            if (doors.name == "Door" + d + "Op")//якщо имя совпадае
            {
                doors.GetComponent<SpriteRenderer>().sortingOrder = 3;//перекласти спрайт вперед



                alfa = gameObject.GetComponent<SpriteRenderer>().color;//беремо цвет
                while (alfa.a > 0)//якщо прозрачность больше 0
                {
                    yield return new WaitForSeconds(0.1f);//чекаемо 0.1с
                    alfa.a -= 0.1F;//уменшаемо прозру
                    gameObject.GetComponent<SpriteRenderer>().color = alfa;//записуемо прозрачность

                }

                doors.GetComponent<SpriteRenderer>().sortingOrder = -1;


                gameObject.GetComponent<Rigidbody2D>().simulated = false;

                gameObject.AddComponent<IIhunt>();
            }
      }
    }




    IEnumerator ChekYourMoney()
    {
        MoveStop /= Time.timeScale;
        prov = true;
        RandomLove = Random.Range(0, 11);
        if (Loves == false && RandomLove == 0 &&Camera.main.GetComponent<Database>().MonsInHotel.Length>0)
        {
            foreach (GameObject Mylove in Camera.main.GetComponent<Database>().MonsInHotel)
            {
                if (Mylove.GetComponent<IIhunt>())
                {
                    if (Mylove.GetComponent<XantStat>().pol != gameObject.GetComponent<XantStat>().pol && !Mylove.GetComponent<IIhunt>().Ilove )
                    {
                        yield return new WaitForSeconds(MoveStop);
                        Loves = true;
                        Mylove.GetComponent<IIhunt>().Ilove = true;
                        d = Mylove.GetComponent<IIhunt>().mYroom;
                        if (d <= 4)
                        {
                            MEfloor = 1;
                        }
                      
                        gameObject.tag = "InHotel";
                        x = gameObject.transform.position.x + 2;
                        y = gameObject.transform.position.y + 3f;
                        Instantiate(Camera.main.GetComponent<Database>().LoveIcon, new Vector2(x, y), Quaternion.identity);
                        IconLove.Lovers_Move = true;
                        move_Plus = true;
                        break;
                        
                    }
                 
                   
                }
            }
        }

        else
        {
            for (rooms = 0; rooms < Database.PriceRoom.Length; rooms++)//перебераемо всі комнати
            {

                if (Database.Free[rooms] == true && gameObject.tag != "InHotel")// якщо хватаэ монет і є свободна комната
                {



                    x = gameObject.transform.position.x + 2;
                    y = gameObject.transform.position.y + 3f;
                    d = rooms;
                    if (d <= 4)
                    {
                        MEfloor = 1;
                    }
                    Database.x = x;
                    Database.y = y;
                    Camera.main.GetComponent<Database>().CanMon = true;
                    yield return new WaitForSeconds(MoveStop);


                    stopForProv = true;
                    break;




                }




            }
        }
        if (gameObject.tag == "NotInHotel")// якщо не сможе жити в готелі
        {
            if (stopForProv == false)
            {
                
                yield return new WaitForSeconds(MoveStop);
            }
            NeverInHotel = true;//позначка ніколи в готелі
            gameObject.layer = 9;//міняемо лейаут
            move_Plus = false;//зупиняемо рух вперед
            move_Minus = true;// двигаемось назад
        }
        prov = false;
       
    }
    
   IEnumerator AlfaUp()
    {
        move_Plus = false;
        move_Minus = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        while(alfa.a>0)
        {
            yield return new WaitForSeconds(0.1f);
            alfa.a -= 0.1f;
            gameObject.GetComponent<SpriteRenderer>().color = alfa;
        }
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y+8);
        while (alfa.a < 1)
        {
            yield return new WaitForSeconds(0.1f);
            alfa.a += 0.1f;
            gameObject.GetComponent<SpriteRenderer>().color = alfa;
        }

        move_Plus = false;
        move_Minus = true;
        gameObject.layer = 9;
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }
}
    