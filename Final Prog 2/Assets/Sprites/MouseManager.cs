using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    //public Camera mainCam;
    //public float rayLen;
    // public NoteManager notManager;
    // Start is called before the first frame update
    public PlayerController playController;
    public GrapplingHook grapplingHook;
    public SpriteRenderer sR;
    void Start()
    {
        sR = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Below was first used for mouse detection, but I used easier way to do that.
        //Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin,ray.direction * rayLen, Color.red);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (hit.collider != null)
        //   {
        //        Debug.Log("hit: " + hit.collider);
//                if (hit.collider.tag == "Box")
//                {
//                    Debug.Log("It's a box");
//                }
//                else if (hit.collider.tag == "Sphere")
//                {
//                    Debug.Log("It's a Sphere");
//                }
        //    else
          //    {
          //        Debug.Log("some others");
          //    }
            //}
          //else
          //{
          //    Debug.Log("hit nothing");
          //}
        //}
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other == null)
        {
            sR.color = Color.red;
        }
        //Detection for if the object can be dragged or grappled
        if (other.tag == "Gable")
        {
            Debug.Log("Grapple");
            grapplingHook.canGrapple = true;
            grapplingHook.canDrag = false;
            sR.color = Color.magenta;
        }

        else if (other.tag == "Dable")
        {
            grapplingHook.canDrag = true;
            grapplingHook.canGrapple = false;
            print(other.gameObject);
            grapplingHook.objectPoint = other.gameObject;
            grapplingHook.objectRB = other.gameObject.GetComponent<Rigidbody2D>();
            
            
        }
        else
        {
            grapplingHook.canDrag = false;
            sR.color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        grapplingHook.canGrapple = false;
        sR.color = Color.red;
    }
}
