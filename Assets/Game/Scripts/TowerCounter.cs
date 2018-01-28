using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerCounter : MonoBehaviour {

    public Text text;
    public int remainingTower;
	
	void Start ()
    {
        text = GetComponent<Text>();
        remainingTower = 4;
	}
	
	
	void Update ()
    {
        text.text = "Radio Towers: " + remainingTower.ToString() + "/4";
	}
}
