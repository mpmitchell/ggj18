using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour {

    public int health;
    public Slider slider;
    public float transmissionTimer;

    public GameObject earthController;
    private EarthController earthControllerScript;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    private TowerScript script1;
    private TowerScript script2;
    private TowerScript script3;

    public bool transmissionOff;
    public int transmissionOffTimer;

    void Start ()
    {
        transmissionOffTimer = 300;
        health = 100;
        earthControllerScript = earthController.GetComponent<EarthController>();
        script1 = tower1.GetComponent<TowerScript>();
        script2 = tower2.GetComponent<TowerScript>();
        script3 = tower3.GetComponent<TowerScript>();
    }
	
	
	void Update ()
    {   
        if (transmissionOff == true)
        {
            transmissionOffTimer--;
            earthControllerScript.speed = 100;
            if (transmissionOffTimer <= 0)
            {
                transmissionOffTimer = 300;
                transmissionOff = false;
            }
        }
        else
        {
            earthControllerScript.speed = 200;
        }
        if (health <=0)
        {
            transmissionOff = false;
            Destroy(this.gameObject);
        }
        if (health >= 1 && transmissionOff == false)
        {
            transmissionTimer += Time.deltaTime;
            if (transmissionTimer >= 5f)
            {
                slider.value++;
                transmissionTimer = 0;
            }

        }
        if (slider.value == 100)
        {
            Debug.Log("DEFENDING PLAYER WINS!");
        }
	}

    public void LaserHit(int damagePerTick)
    {
        Debug.Log(this.health);
        Debug.Log("HitByLaser");
        health -= damagePerTick;
    }

    public void RocketHit(int damage)
    {
        Debug.Log(this.health);
        Debug.Log("HitByRocket");
        health -= damage;
    }

    public void RadiowaveHit()
    {
        transmissionOff = true;
        script1.transmissionOff = true;
        script2.transmissionOff = true;
        script3.transmissionOff = true;
    }


}
