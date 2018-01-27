﻿using UnityEngine;
using System.Collections;

public class AlienController : MonoBehaviour {

	public float speed;
  public float rotationalSpeed;
  public float rocketCooldown;
  public float radiowaveCooldown;
  public float laserCooldown;
  public AlienSpawner spawner;
  public GameObject rocketPrefab;
  public GameObject radiowavePrefab;
  public GameObject laserPrefab;
  public Transform rocketSpawn;
  public Transform radiowaveSpawn;
  public Transform laserSpawn;

  Renderer renderer;
  int mask;
  public float rocketTimer = 0f;
  public float radiowaveTimer = 0f;
  public float laserTimer = 0f;

  void Start() {
    renderer = GetComponent<Renderer>();
    mask = LayerMask.GetMask("Planet", "Towers", "EarthPlayer");
  }

  void Update() {
    float horiztontal = -Input.GetAxis("AlienHorizontal") * speed * Time.deltaTime;
    float vertical = -Input.GetAxis("AlienVertical") * speed * Time.deltaTime;
    float rotate = Input.GetAxis("AlienRotation") * rotationalSpeed * Time.deltaTime;
    bool fire = Input.GetButtonDown("AlienFire");

    transform.Translate(new Vector3(horiztontal, vertical, 0f), Space.World);
    transform.Rotate(rotate * Vector3.forward);

    // if ((horiztontal != 0f || vertical != 0f) && !fire) {
    //   StartCoroutine(FadeOut());
    // } else {
    //   StopAllCoroutines();
    // }

    if (rocketTimer >= 0f) rocketTimer -= Time.deltaTime;
    if (radiowaveTimer >= 0f) radiowaveTimer -= Time.deltaTime;
    if (laserTimer >= 0f) laserTimer -= Time.deltaTime;

    if (fire) {
      if (rocketTimer <= 0f && Physics2D.Raycast(rocketSpawn.position, rocketSpawn.right, Mathf.Infinity, mask)) {
        Instantiate(rocketPrefab, rocketSpawn.position, rocketSpawn.rotation);
        rocketTimer = rocketCooldown;
      }
      if (radiowaveTimer <= 0f && Physics2D.Raycast(radiowaveSpawn.position, radiowaveSpawn.right, Mathf.Infinity, mask)) {
        Radiowave radiowave = Instantiate(radiowavePrefab, Vector3.zero, radiowaveSpawn.rotation).GetComponent<Radiowave>();
        radiowave.spawn = radiowaveSpawn;
        radiowaveTimer = radiowaveCooldown;
      }
      if (laserTimer <= 0f && Physics2D.Raycast(laserSpawn.position, laserSpawn.right, Mathf.Infinity, mask)) {
        Laser laser = Instantiate(laserPrefab, laserSpawn.position, laserSpawn.rotation).GetComponent<Laser>();
        laser.spawn = laserSpawn;
        laserTimer = laserCooldown;
      }
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Planet") {
      spawner.DestroyCurrent();
    }
  }

  IEnumerator FadeOut() {
    Color color = renderer.material.color;
    while (color.a > 0f) {
      color.a -= 0.1f;
      renderer.material.color = color;
      yield return new WaitForSeconds(0.1f);
    }
  }
}
