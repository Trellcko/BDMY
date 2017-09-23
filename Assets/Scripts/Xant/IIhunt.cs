using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IIhunt : MonoBehaviour {

    public bool Ilove;
    //public int news;
    //public bool newspaper;
    GameObject MyDoors;
    //GameObject newspapers;
    public bool gotoRoom = false;
    public bool exitCourtina = true;
    public bool inRoom = true;
    public int floor;
    public int mYroom;
    public int needFloor;
    public int StartWork;
    public float StartWorkMin;
    public float FinishWokMin;
    public int FinishWork;
    float? PosMyTriggObj;
    int MinusFloor;
    bool inTrigger = false;
    public int myFloor;
    bool InWork;
    bool inEvent = false;
    public bool move_Plus;
    public bool move_Minus;
    int numberRoomTag;
    Color alfa_f;
    Animator myAnim;
   Vector2 MonstMove_Plus = new Vector2(5, 0);// для  руху вперед
   Vector2 MonstMove_Minus = new Vector2(-5, 0);// для руху назад
    Vector2 StairsDowns;
    Vector2 StairsUp;
    void Start()
    {
        Ilove = gameObject.GetComponent<Xant>().Loves;
        #region Component       

        mYroom = gameObject.GetComponent<Xant>().d;
        //newspapers = GameObject.Find("news");
        Camera.main.GetComponent<Database>().AddMonst();

        for (numberRoomTag = 0; numberRoomTag < Database.Doors.Length; numberRoomTag++)
        {
            if (Database.Doors[numberRoomTag].name == "Door" + gameObject.GetComponent<Xant>().d + "Op")
                break;
        }
        MyDoors = GameObject.Find("door 1 (" + gameObject.GetComponent<Xant>().d + ")");
        myFloor = gameObject.GetComponent<Xant>().MEfloor;
        alfa_f = GetComponent<SpriteRenderer>().color;
        myAnim = GetComponent<Animator>();
        floor = myFloor;

        Destroy(gameObject.GetComponent<Xant>());

        #endregion
        #region StartandFinishWork

        if (gameObject.GetComponent<XantStat>().x <= 5)
        {
            StartWork = Random.Range(13, 15);
            StartWorkMin = Random.Range(0, 61);
            FinishWork = Random.Range(19, 21);
            FinishWokMin = Random.Range(0, 61);
        }
        else if (gameObject.GetComponent<XantStat>().x > 5 && gameObject.GetComponent<XantStat>().x <= 10)
        {
            StartWork = Random.Range(1, 3);
            StartWorkMin = Random.Range(0, 61);
            FinishWork = Random.Range(6, 8);
            FinishWokMin = Random.Range(0, 61);
        }

    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == MyDoors.name && gotoRoom == true)
        {
            gotoRoom = false;
            StartCoroutine(EnterRoom());
        }
        if (collision.tag == "StairsUp")
        {
            StartCoroutine(Alfa_and_Teleport());
        }
       
        if (collision.tag == "delet" && (inEvent == true || InWork == true))
        {
            inTrigger = true;
            move_Minus = false;

            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
        //if (collision.name == "news" && newspaper == true && news == 1)
        //{
        //    newspaper = false;
        //    exitCourtina = false;
        //    Invoke("WaitNewspaper", 1.25f / Time.timeScale);

        //}
    }

    private void FixedUpdate()
    {
        Move();
    }
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
        Prov();
        GotoRoom();
        Work();
      


    }
    void Prov()
    {
        if (move_Minus == false && move_Plus == false && exitCourtina == true && gameObject.GetComponent<Rigidbody2D>().simulated == true)

        {
            gotoRoom = true;
        }
    }
    void Move()
    {
        if (exitCourtina == true)
        {

            if (move_Plus == true)
            {
                transform.Translate(MonstMove_Plus * Time.deltaTime);


                myAnim.Play("XantMovePlus");


            }


            else if (move_Minus == true)
            { 

                myAnim.Play("XantMoveMinus");

                transform.Translate(MonstMove_Minus * Time.deltaTime);
            }
        }
    }
    void GotoRoom()
    {
        if (gotoRoom == true)
        {
             needFloor=myFloor ;
            MinusFloor = needFloor - floor;
            if (MinusFloor > 0)
            {
                Upper();
            }
            else if (MinusFloor < 0)
            {
                Downer();
            }
            if (MinusFloor == 0)
            {
                if (PosMyTriggObj == null)
                {
                    PosMyTriggObj = gameObject.transform.position.x - MyDoors.transform.position.x;
                }
                if (PosMyTriggObj < 0)
                {
                    move_Plus = true;
                    move_Minus = false;
                }
                if (PosMyTriggObj > 0)
                {

                    move_Plus = false;
                    move_Minus = true;
                }
            }



        }
    }
    void Work()
    {

        if ((StartWork == Camera.main.GetComponent<Database>().Hours && StartWorkMin == Camera.main.GetComponent<Database>().Minute) || InWork == true)
        {
            needFloor=0;
            gameObject.layer = 11;
            InWork = true;
            //newspaper = false;
            gotoRoom = false;
            if (inRoom == true)
            {

                StartCoroutine(ExitRoom());
            }


            if (exitCourtina == true)
            {
                if (inTrigger == false)
                {
                    Downer();
                }
            }

            if (FinishWork == Camera.main.GetComponent<Database>().Hours && FinishWokMin == Camera.main.GetComponent<Database>().Minute && InWork == true)
            {
                inTrigger = false;
                gameObject.GetComponent<Rigidbody2D>().simulated = true;
                InWork = false;
                gotoRoom = true;
                gameObject.layer = 8;
                PosMyTriggObj = null;
                //newspaper = true;
                //news = 0;
            }
        }

    }

    //void NewSpapers()
    //{

    //    if (InWork == true)
    //    {
    //        Work();
    //        newspaper = false;
    //    }
    //    else
    //        if ((Camera.main.GetComponent<Database>().Hours - FinishWork >= 3 || (gameObject.GetComponent<XantStat>().x <= 5 && StartWork - Camera.main.GetComponent<Database>().Hours > 2)) && gotoRoom == false)
    //    {



    //        if (news == 0)
    //        {
    //            news = Random.Range(1, 4);
    //        }
    //        if (news == 1 && newspaper == true)
    //        {
    //            gameObject.layer = 11;
    //            if (inRoom == true && exitCourtina == true)
    //            {
    //                StartCoroutine(ExitRoom());
    //            }
    //            if (floor > 0)
    //            {
    //                Downer();
    //            }
    //            if (floor == 0)
    //            {
    //                if (PosMyTriggObj == null)
    //                {
    //                    PosMyTriggObj = gameObject.transform.position.x - newspapers.transform.position.x;
    //                }
    //                if (PosMyTriggObj < 0)
    //                {
    //                    move_Plus = true;
    //                    move_Minus = false;
    //                }
    //                else if (PosMyTriggObj > 0)
    //                {
    //                    move_Minus = true;
    //                    move_Plus = false;
    //                }
    //            }
    //        }

    //    }
    //}
    IEnumerator EnterRoom()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        move_Plus = false;
        move_Minus = false;
        exitCourtina = false;
        while (alfa_f.a > 0)
        {
            Database.Doors[numberRoomTag].GetComponent<SpriteRenderer>().sortingOrder = 3;
            yield return new WaitForSeconds(0.05f);
            alfa_f.a -= 0.1f;
            gameObject.GetComponent<SpriteRenderer>().color = alfa_f;
        }
        Database.Doors[numberRoomTag].GetComponent<SpriteRenderer>().sortingOrder = -1;
        exitCourtina = true;
        inRoom = true;

        gotoRoom = false;

        PosMyTriggObj = null;
    }


    IEnumerator ExitRoom()
    {
        inRoom = false;
        move_Plus = false;
        move_Minus = false;
        exitCourtina = false;
        while (alfa_f.a < 1)
        {
            Database.Doors[numberRoomTag].GetComponent<SpriteRenderer>().sortingOrder = 3;
            yield return new WaitForSeconds(0.05f);
            alfa_f.a += 0.1f;
            gameObject.GetComponent<SpriteRenderer>().color = alfa_f;
        }
        Database.Doors[numberRoomTag].GetComponent<SpriteRenderer>().sortingOrder = -1;
        exitCourtina = true;

        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

        IEnumerator Alfa_and_Teleport()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        if (needFloor != 0)
        {
            StairsUp = new Vector2(gameObject.transform.position.x-1, -21.3f + (8 * needFloor));
        }
        else
        {
            StairsUp = new Vector2(gameObject.transform.position.x-1, -21.3f);
        }
        move_Plus = false;
        move_Minus = false;
        exitCourtina = false;
        while (alfa_f.a > 0)
        {
            yield return new WaitForSeconds(0.1f);
            alfa_f.a -= 0.1f;
            gameObject.GetComponent<SpriteRenderer>().color = alfa_f;
        }
        transform.position = StairsUp;
        floor = needFloor ;
        while (alfa_f.a < 1)
        {
            yield return new WaitForSeconds(0.1f);
            alfa_f.a += 0.1f;
            gameObject.GetComponent<SpriteRenderer>().color = alfa_f;
        }
        exitCourtina = true;

        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }
   



    

    void Downer()
    {
        
        if (floor == 0)
        {

            move_Minus = true;
            move_Plus = false;
            if (InWork == true)
            {
                gameObject.layer = 11;
            }
            else gameObject.layer = 9;
        }
        
        else
        {
            if (InWork == true)
            {
                gameObject.layer = 11;
            }
            move_Plus = true;
            move_Minus = false;
            if (InWork == true)
            {
                gameObject.layer = 11;
            }
            else gameObject.layer = 10;
        }
    }

    void Upper()
    {

        if (floor == 0)
        {

            move_Minus = false;
            move_Plus = true;
            gameObject.layer = 8;
        }
        else if (floor % 2 == 0)
        {
            move_Minus = false;
            move_Plus = true;
            gameObject.layer = 10;
        }
       
    }
    void WaitNewspaper()
    {

        gotoRoom = true;
        exitCourtina = true;
        PosMyTriggObj = null;
        exitCourtina = true;
    }
}
  

