using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //This is used only for binding to the spawners, to set the camera position and respawn position
    public Vector3 camVec;
    private Camera mainCam;
    public bool isUsed;
    private SpriteRenderer sR;
    private GameObject gM;
    GrapplingHook grapplingHook;
    void Start()
    {
        gM = GameObject.Find("GameManager");
        grapplingHook = gM.GetComponent<GrapplingHook>();
        mainCam = Camera.main;
        sR = GetComponent<SpriteRenderer>();
        sR.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsed)
        {
            sR.color = Color.red;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isUsed = true;
            grapplingHook.respawnPoint = this.transform.position;
            mainCam.transform.position = camVec;
        }
    }
}
