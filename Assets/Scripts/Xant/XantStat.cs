 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XantStat : MonoBehaviour {
  public string NUME;
    public  int x;
   public string pol;
    public int Hp;
   public string race = "Hunt";
    string ocupation;
    public Sprite SpriteAttack;
    public Sprite Female;
    public Sprite Male;
    // Use this for initialization
    private void Awake()
    {
        
    }
    private void OnMouseDown()
    {
        
    }
    void Start()
    {
        if (gameObject.name == "XantGirl(Clone)")
            pol = "Girl";
        else if (gameObject.name == "Xant(Clone)")
            pol = "Man";
    
        if (pol=="Girl")
        {
            x = Random.Range(0, 11);
            if (x <=5)
            {

                ocupation = Database.ocupationUpto8hoursG[Random.Range(0, Database.ocupationUpto8hoursG.Length)];//выбор работы
            }
            else if (x >5&&x<=10)
            {
                ocupation = Database.OcupationFroom00UpTo4Gnight[Random.Range(0, Database.OcupationFroom00UpTo4Gnight.Length)];//выбор работы

            }
           
        }
        else
        {
            x = Random.Range(0, 11);
            if (x <= 5)
            {
                ocupation = Database.ocupationUpto8hoursM[Random.Range(0, Database.ocupationUpto8hoursM.Length)];//выбор работы
            }
            else if (x > 5 && x <= 10)
            {
                ocupation = Database.OcupationFroom00UpTo4Mnight[Random.Range(0, Database.OcupationFroom00UpTo4Mnight.Length)];//выбор работы
            }
        
            
        }
        
    }
	

    private void OnMouseEnter()
    {
      
            Camera.main.GetComponent<Database>().see = true;
            Database.PlaceForSpriteAttack.GetComponent<SpriteRenderer>().sprite = SpriteAttack;

            if (pol == "Girl")
            {

                Database.PlaceForSpitePol.GetComponent<SpriteRenderer>().sprite = Female;
            }
            else if (pol == "Man")
            {
                Database.PlaceForSpitePol.GetComponent<SpriteRenderer>().sprite = Male;
            }
            Database.ocup_mon = ocupation;
            Database.race_mon = race;
        }
    

    private void OnMouseExit()
    {
        Camera.main.GetComponent<Database>().see = false;
        Database.ocup_mon = "";
        Database.race_mon = "";
        Database.PlaceForSpriteAttack.GetComponent<SpriteRenderer>().sprite = null;
        Database.PlaceForSpitePol.GetComponent<SpriteRenderer>().sprite = null;
    }


  


  
}
