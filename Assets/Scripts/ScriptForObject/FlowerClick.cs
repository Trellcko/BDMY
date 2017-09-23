using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerClick : MonoBehaviour {
    Animator MyAnim;
	// Use this for initialization
	void Start () {
        MyAnim = gameObject.GetComponent<Animator>();
	}
    private void OnMouseDown()
    {
        MyAnim.Play("FlowerMove");
    }
}
