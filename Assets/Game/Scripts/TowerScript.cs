using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour {

    public int health;
    public Slider slider;
    public float transmissionTimer;

    void Start ()
    {
        health = 100;
	}
	
	
	void Update ()
    {   
        if (health <=0)
        {
            Destroy(this.gameObject);
        }
        if (health >= 1)
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


}
