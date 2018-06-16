using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour {

    public GameObject TomText;
    public GameObject TommyText;
    public Transform hologram;
    public Transform textSpawnPos;

    GameObject displayText;

    int rowPos;

    Animator anim;

    // Use this for initialization
    void Start () {
        displayText = (GameObject)Instantiate(TomText, textSpawnPos.position, textSpawnPos.rotation, hologram);
        anim = displayText.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ArrowRight()
    {
        switch (rowPos)
        {
            case 0:
                rowPos = -1;
                anim.SetInteger("State", rowPos);
                break;
            case -1:
                rowPos = -2;
                anim.SetInteger("State", rowPos);
                break;
        }
    }

    public void ArrowLeft()
    {
        switch (rowPos)
        {
            case -2:
                rowPos = -1;
                anim.SetInteger("State", rowPos);
                break;
            case -1:
                rowPos = 0;
                anim.SetInteger("State", rowPos);
                break;
        }
    }
}
