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

  MeshRenderer[] renderers;
  MeshRenderer renderer;
  int mask;
  public float rocketTimer = 0f;
  public float radiowaveTimer = 0f;
  public float laserTimer = 0f;

  void Start() {
    renderers = GetComponentsInChildren<MeshRenderer>();
    renderer = GetComponentInChildren<MeshRenderer>();
    mask = LayerMask.GetMask("Planet", "Towers", "EarthPlayer");
  }

  void Update() {
    float horiztontal = -Input.GetAxis("AlienHorizontal") * speed * Time.deltaTime;
    float vertical = -Input.GetAxis("AlienVertical") * speed * Time.deltaTime;
    float rotate = Input.GetAxis("AlienRotation") * rotationalSpeed * Time.deltaTime;
    bool fire = Input.GetButtonDown("AlienFire");

    transform.Translate(new Vector3(horiztontal, vertical, 0f), Space.World);
    transform.Rotate(rotate * Vector3.forward);

    if ((horiztontal != 0f || vertical != 0f) && !fire) {
      renderer.enabled = false;
      // StartCoroutine(FadeOut());
    } else {
      renderer.enabled = true;
      // foreach (MeshRenderer renderer in renderers) {
      //   Color color = renderer.material.color;
      //   color.a = 1f;
      //   renderer.material.color = color;
      // }
      // StopAllCoroutines();
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
    while (Input.GetAxis("AlienHorizontal") != 0f || Input.GetAxis("AlienVertical") != 0f) {
      foreach (MeshRenderer renderer in renderers) {
        Color color = renderer.material.color;
        color.a = color.a - 0.1f;
        renderer.material.color = color;
      }
      yield return new WaitForSeconds(0.1f);
    }
  }
}
