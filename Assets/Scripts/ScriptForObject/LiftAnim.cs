using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftAnim : MonoBehaviour {
  public  bool Open;
  public  bool close;
    public bool work=false;
    Animator AnimLift;
	void Start () {
        AnimLift = gameObject.GetComponent<Animator>();
        AnimLift.speed = 3.3f;
	}
	
	// Update is called once per frame
	void Update () {
        AnimLift.enabled = work;
            if (Open == true)
            {
                AnimLift.Play("NewLiftAnimOpen");
            }
          else  if (close == true)
            {
                AnimLift.Play("LiftAnimClose");
            }
        
    }

}
