using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicForPortal : MonoBehaviour {
    Animator AnimPor;
    private void Start()
    {
        AnimPor = gameObject.GetComponent<Animator>();
        AnimPor.enabled = false;
    }
    private void OnMouseDown()
    {
        AnimPor.enabled = true;
        AnimPor.Play("Portal");
       
    }
    private void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite.name=="Портал_3")
        {
            AnimPor.Play("PortalClose");
        }
    }
}
