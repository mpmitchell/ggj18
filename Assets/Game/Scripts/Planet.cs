﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Planet : MonoBehaviour {

    public GameObject explosion;

    public int health;
    public GameObject tower;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject tower4;
    public GameObject textCounter;
    public TowerCounter towerCounterScript;

    new public MeshRenderer renderer;

    void Start()
    {
        health = 400;
        towerCounterScript = textCounter.GetComponent<TowerCounter>();
    }
    private void Update()
    {
        if (health <= 0)
        {
            EndScreen.sos = false;
            Instantiate(explosion, transform.position, transform.rotation);
            towerCounterScript.remainingTower = 0;
            Destroy(tower.gameObject);
            Destroy(tower2.gameObject);
            Destroy(tower3.gameObject);
            Destroy(tower4.gameObject);
            Destroy(this.gameObject);
            SceneManager.LoadScene(3);
        }

        renderer.material.color = Color.Lerp(Color.red, Color.white, (float)health / 600f);
    }

    void LaserHit(int damagePerTick) {
        health -= damagePerTick;
        Debug.Log(health);
  }

  void RocketHit(int damage) {
        health -= damage;
  }

  void RadiowaveHit() {
    //
  }
}
