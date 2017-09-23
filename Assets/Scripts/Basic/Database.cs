using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Database : MonoBehaviour
{
    public GameObject LoveIcon;
    bool Open=true;
    public  GameObject[] MonsInHotel;
    public int day = 1;
    const float TimeH = 0.25f;
    public int Hours = 12;// години
    public int Minute=0; //минут
    public  static float x;// MonstrsInHome х
    public static float y;// MonstrsInHome y
    public float TimeSpawn=2.5f;//промежуток между спавнами
    public GameObject MonstrsInHome; //
    public Slider hpSlid;
    public static GameObject PlaceForSpriteAttack;
    public static GameObject PlaceForSpitePol;
    public Text Ocupation;
    public Text Name;
    public Text Race;
    public static string ocup_mon;
    public static string name_mon;
    public static string race_mon;
    public GameObject StatsMonst;
   public  bool see=false;
    public bool CanMon = false;
    int needInfo;

    
    public static string[] ocupationUpto8hoursM = { "Heaver", "Cleaner", "Waiter" };
    public static string[] ocupationUpto8hoursG = { "Nanny", "Nurse"," Waitress" };
    public static string[] OcupationFroom00UpTo4Mnight = { "Security Guard" };
    public static string[] OcupationFroom00UpTo4Gnight = { "Night butterfly" };
    public static bool[] Free = { true, true, true, true, true };
    public static int[] PriceRoom = { 20, 20, 20, 20, 20 };
    public static GameObject[] Doors;

    private void Awake()
    {
        
        Cursor.visible = false;//скрыл курсор
        Screen.SetResolution(1280, 720, false);//размер екрана
        Ocupation.color = new Color(186,33,203);
    }

    private void Start()
    {
        needInfo = 5;
        Doors = GameObject.FindGameObjectsWithTag("Door");
        PlaceForSpitePol = GameObject.Find("FemaleOrMale");
        PlaceForSpriteAttack = GameObject.Find("Attacks");
        StartCoroutine(Timer());
    }
    private void Update()
    {

        
        MonstrsInHome.SetActive(CanMon);
        MonstrsInHome.transform.position = new Vector2(x, y);
        if (CanMon==true)
        {
            StartCoroutine(SpawnCanMonst());
           
        }
        if (Xant.canInHotel==true)
        {
            StopCoroutine(SpawnCanMonst());
            CanMon = false;
        }
        hpSlid.gameObject.SetActive(see);
        StatsMonst.SetActive(see);
        Ocupation.text = ocup_mon;
        Name.text = name_mon;
        Race.text = race_mon;
    }
    IEnumerator SpawnCanMonst()
    {
       
        yield return new WaitForSeconds(TimeSpawn/Time.timeScale );
        CanMon = false;
    }

    IEnumerator Timer()
    {
      
        while (true)
        {
            yield return new WaitForSeconds(TimeH);
            Minute += 1;
            if (Minute == 60)
            {
                Hours += 1;
                if (Hours==24)
                {
                    Hours = 0;
                    day += 1;
                }
                Minute = 0;
            }
        }
    }

    public void AddMonst()
    {
        MonsInHotel = GameObject.FindGameObjectsWithTag("InHotel");
    }
    

}

