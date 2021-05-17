using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 grapplePoint;
    public Vector2 respawnPoint;
    
    public GameObject objectPoint;
    public Rigidbody2D playerRB;
    public Rigidbody2D objectRB;
    private GameObject player;
    public Camera mainCam;
    
    //Used for the grapple hook
    private LineRenderer lR;
    private DistanceJoint2D dJ;
    public float shortenSpeed;
    public GameObject mouse;
    public bool canGrapple;
    public bool canDrag;
    public bool dragging = false;
    void Start()
    {
        respawnPoint = Vector2.zero;
        player = GameObject.Find("PlayerPlaceHolder");
        lR = player.GetComponent<LineRenderer>();
        dJ = player.GetComponent<DistanceJoint2D>();
        disableGrapple();
        playerRB = player.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        startPoint = player.transform.position;
        //Getting mouse position
        endPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //For the mouse grapple and drag function
        mouseDetect();
        //Press R to respawn
        respawn();
    }

    public void enableGrapple()
    {
        dJ.connectedAnchor = grapplePoint;
        dJ.enabled = true;
    }
    
    public void disableGrapple()
    {
        dJ.enabled = false;
        dragging = false;
        canGrapple = false;
        canDrag = false;
    }


    public void respawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerRB.velocity = new Vector2(0,0);
            player.transform.position = respawnPoint;
        }
    }
    public void mouseDetect()
    {

        if (!dragging)
        {
            mouse.transform.position = endPoint;
        }
        if (canGrapple)//If it's a wall for grappling, which is the green one
        {
            //mouse.transform.position = endPoint;
            if (!Input.GetMouseButton(0))
            {
                grapplePoint = endPoint;
            }
            if (Input.GetMouseButton(0))
            {
                //draw grapple
                lR.SetPosition(0,startPoint);
                lR.SetPosition(1,grapplePoint);
                enableGrapple();
                //Grappling function, shorten the rope
                if (Input.GetMouseButton(1))
                {
                    print("rmb");
                    dJ.distance -= shortenSpeed;
                }
            }
            else
            {
                //Reset
                lR.SetPosition(0,new Vector2(0,0));
                lR.SetPosition(1,new Vector2(0,0));
                disableGrapple();
            }
        }

        else if(canDrag)//If it's box 
        {
            if (!Input.GetMouseButton(0))
            {
                grapplePoint = endPoint;
            }
            if (Input.GetMouseButton(0))
            {
                //Draw the grapple
                lR.SetPosition(0,startPoint);
                lR.SetPosition(1,objectPoint.transform.position);
                
                if (Input.GetMouseButton(1))
                {
                    //actual grappling force
                    playerRB.AddForce((objectPoint.transform.position-player.transform.position),ForceMode2D.Force);
                    objectRB.AddForce((player.transform.position-objectPoint.transform.position)*3f,ForceMode2D.Force);
                }
            }
            else
            {
                //Reset
                lR.SetPosition(0,new Vector2(0,0));
                lR.SetPosition(1,new Vector2(0,0));
                disableGrapple();
            }
        }
    }
}
