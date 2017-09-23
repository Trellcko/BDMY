using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour {

public void Timer2x()
    {
        Time.timeScale = 1.75f;
        
    }
    public void Timer1x()
    {
        Time.timeScale = 1;
  
    }
    public void Timer3x()
    {
        Time.timeScale = 2.5f;
       
    }
}
