using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    Vector2 startPoint;
    Vector2 endPoint;

    Vector2 grapplePoint;
    private GameObject player;
    public Camera mainCam;
    private LineRenderer lR;
    private DistanceJoint2D dJ;
    
    void Start()
    {
        player = GameObject.Find("PlayerPlaceHolder");
        lR = player.GetComponent<LineRenderer>();
        dJ = player.GetComponent<DistanceJoint2D>();
        disableGrapple();
    }


    void Update()
    {

        startPoint = player.transform.position;
        endPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (!Input.GetMouseButton(0))
        {
            grapplePoint = endPoint;
        }

        if (Input.GetMouseButton(0))
        {
            lR.SetPosition(0,startPoint);
            lR.SetPosition(1,grapplePoint);
            enableGrapple();
            

        }
        else
        {
            lR.SetPosition(0,new Vector2(0,0));
            lR.SetPosition(1,new Vector2(0,0));
            disableGrapple();
        }

    }

    public void enableGrapple()
    {
        dJ.connectedAnchor = grapplePoint;
        dJ.enabled = true;
    }
    
    public void disableGrapple()
    {
        dJ.enabled = false;
    }
}
