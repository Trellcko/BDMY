using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconLove : MonoBehaviour {
    public static bool Lovers_Move;
     Vector2 MovePlus= new Vector2(5, 0);
    Vector2 MoveMinus=new Vector2(-5,0) ;
    const float TimeLifeIcon=6f;

    void Start () {
        Time.timeScale = 1;
        StartCoroutine(Delete());
	}
	
	
	void Update () {
		if (Lovers_Move==true)
        {
            gameObject.transform.Translate(MovePlus * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(MoveMinus * Time.deltaTime);
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(TimeLifeIcon / Time.timeScale);
        Destroy(gameObject);
    }
}
