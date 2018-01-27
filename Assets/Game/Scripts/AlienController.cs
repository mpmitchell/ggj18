using UnityEngine;
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

  MeshRenderer renderer;
  int mask;
  float rocketTimer = 0f;
  float radiowaveTimer = 0f;
  float laserTimer = 0f;

  public float minOpacity;
  public float fadeSpeed;
  float firing = 0f;

  void Start() {
    renderer = GetComponentInChildren<MeshRenderer>();
    mask = LayerMask.GetMask("Planet", "Towers", "EarthPlayer");
  }

  void Update() {
    float horiztontal = -Input.GetAxis("AlienHorizontal") * speed * Time.deltaTime;
    float vertical = -Input.GetAxis("AlienVertical") * speed * Time.deltaTime;
    float rotate = Input.GetAxis("AlienRotation") * rotationalSpeed * Time.deltaTime;
    bool fire = Input.GetButtonDown("AlienFire");

    transform.Translate(new Vector3(horiztontal, vertical, 0f), Space.World);
    Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

    if (position.x >= 0.99f || position.x <= 0.01f ||
      position.y >= 0.99f || position.y <= 0.01f) {
      firing = 1f;
    }

    position.x = Mathf.Clamp01(position.x);
    position.y = Mathf.Clamp01(position.y);
    transform.position = Camera.main.ViewportToWorldPoint(position);
    transform.Rotate(rotate * Vector3.forward);

    if (firing > 0f) {
      firing -= Time.deltaTime;
    }

    if ((horiztontal != 0f || vertical != 0f) && firing <= 0f) {
      Color color = renderer.material.color;
      if (color.a <= 0f) {
        renderer.enabled = false;
      }
      color.a = color.a - 1f * fadeSpeed * Time.deltaTime;
      color.a = Mathf.Max(color.a, minOpacity);
      renderer.material.color = color;
    } else {
      renderer.enabled = true;
      Color color = renderer.material.color;
      color.a = 1f;
      renderer.material.color = color;
    }

    if (rocketTimer >= 0f) rocketTimer -= Time.deltaTime;
    if (radiowaveTimer >= 0f) radiowaveTimer -= Time.deltaTime;
    if (laserTimer >= 0f) laserTimer -= Time.deltaTime;

    if (fire) {
      RaycastHit2D hit;
      hit = Physics2D.Raycast(rocketSpawn.position, rocketSpawn.right, Mathf.Infinity, mask);
      if (rocketTimer <= 0f && hit.collider != null) {
        Rocket rocket = Instantiate(rocketPrefab, rocketSpawn.position, rocketSpawn.rotation).GetComponent<Rocket>();
        RaycastHit2D aimAssitHit = Physics2D.Raycast(rocketSpawn.position, rocketSpawn.right, Mathf.Infinity, LayerMask.GetMask("AimAssist"));
        if (aimAssitHit.collider != null) {
          rocket.target = aimAssitHit.transform.parent.position;
        } else {
          rocket.target = hit.point;
        }
        rocketTimer = rocketCooldown;
        firing = 1f;
      }
      if (radiowaveTimer <= 0f && Physics2D.Raycast(radiowaveSpawn.position, radiowaveSpawn.right, Mathf.Infinity, mask)) {
        Radiowave radiowave = Instantiate(radiowavePrefab, Vector3.zero, radiowaveSpawn.rotation).GetComponent<Radiowave>();
        radiowave.spawn = radiowaveSpawn;
        radiowaveTimer = radiowaveCooldown;
        firing = radiowaveCooldown;
      }
      if (laserTimer <= 0f && Physics2D.Raycast(laserSpawn.position, laserSpawn.right, Mathf.Infinity, mask)) {
        Laser laser = Instantiate(laserPrefab, laserSpawn.position, laserSpawn.rotation).GetComponent<Laser>();
        laser.spawn = laserSpawn;
        laserTimer = laserCooldown;
        firing = laserCooldown;
      }
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Planet") {
      spawner.DestroyCurrent();
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    firing = 1f;
  }
}
