using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour {

    static LinkedList<TowerScript> _towers = new LinkedList<TowerScript>();

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

    new public MeshRenderer renderer;

    bool upgradedHit = false;

    public static TowerScript GetMostDamagedTower() {
        TowerScript lowestTower = _towers.First.Value;
        int lowestHealth = 100;
        foreach (TowerScript tower in _towers) {
            if (tower.health < lowestHealth) {
                lowestTower = tower;
                lowestHealth = tower.health;
            }
        }

        return lowestTower;
    }

    void Awake() {
        _towers.AddLast(this);
    }

    void Start ()
    {
        transmissionOffTimer = 240;
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
            earthControllerScript.speed = upgradedHit ? 75 : 100;
            if (transmissionOffTimer <= 0)
            {
                transmissionOffTimer = 240;
                transmissionOff = false;
                upgradedHit = false;
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

        renderer.material.color = Color.Lerp(Color.red, Color.white, (float)health / 100f);
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

    public void UpgradedRadiowaveHit() {
        RadiowaveHit();
        upgradedHit = true;
    }

    public void Heal(int health) {
        this.health = Mathf.Min(100, this.health + health);
    }

    void OnDestroy() {
        _towers.Remove(this);
    }
}
