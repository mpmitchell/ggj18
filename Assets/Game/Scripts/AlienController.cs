using UnityEngine;
using UnityEngine.UI;
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

    public Image rocketCD;
    public Image laserCD;
    public Image radioCD;

    new Rigidbody2D rigidbody;
  public float repulsiveForce;

  new MeshRenderer renderer;
  float rocketTimer = 0f;
  float radiowaveTimer = 0f;
  float laserTimer = 0f;

  public float minOpacity;
  public float fadeSpeed;
  float firing = 0f;

  public Transform pivot;

  public float speedDownMultiplier;
  public float speedDownTime;
  float fullSpeed;
  float speedDownTimer = 0f;

  void Start() {
    renderer = GetComponentInChildren<MeshRenderer>();
    rigidbody = GetComponent<Rigidbody2D>();
    fullSpeed = speed;
  }

  void Update() {
    float horiztontal = -Input.GetAxis("AlienHorizontal") * speed * Time.deltaTime;
    float vertical = -Input.GetAxis("AlienVertical") * speed * Time.deltaTime;
    float rotate = Input.GetAxis("AlienRotation") * rotationalSpeed * Time.deltaTime;
    bool fire = Input.GetButtonDown("AlienFire");

    Vector3 dir = Vector3.zero - transform.position;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    transform.Translate(new Vector3(horiztontal, vertical, 0f), Space.World);
    Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

    if (position.x >= 0.99f || position.x <= 0.01f ||
      position.y >= 0.99f || position.y <= 0.01f) {
      firing = 1f;
    }

    position.x = Mathf.Clamp01(position.x);
    position.y = Mathf.Clamp01(position.y);
    transform.position = Camera.main.ViewportToWorldPoint(position);
    pivot.Rotate(rotate * Vector3.forward, Space.World);

    if (firing > 0f) {
      firing -= Time.deltaTime;
    }

    if ((horiztontal != 0f || vertical != 0f) && firing <= 0f && speedDownTimer <= 0f) {
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

        if (rocketTimer >= 0f)
        {
            rocketTimer -= Time.deltaTime;
            rocketCD.GetComponent<Image>().color = new Color(0.2f,0.2f,0.2f,1f) ;
        }
        else
        {
            rocketCD.GetComponent<Image>().color = Color.white;
        }
        if (radiowaveTimer >= 0f)
        {
            radiowaveTimer -= Time.deltaTime;
            radioCD.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1f);
        }
        else
        {
            radioCD.GetComponent<Image>().color = Color.white;
        }
        if (laserTimer >= 0f)
        {
            laserTimer -= Time.deltaTime;
            laserCD.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1f);
        }
        else
        {
            laserCD.GetComponent<Image>().color = Color.white;
        }

        if (fire) {
      float r = rocketSpawn.position.magnitude;
      float rw = radiowaveSpawn.position.magnitude;
      float l = laserSpawn.position.magnitude;

      if (rocketTimer <= 0f && r <= rw && r <= l) {
        Rocket rocket = Instantiate(rocketPrefab, rocketSpawn.position, rocketSpawn.rotation).GetComponent<Rocket>();
        Debug.DrawRay(rocketSpawn.position, rocketSpawn.right * 100f, Color.white, 3f);
        RaycastHit2D aimAssitHit = Physics2D.Raycast(rocketSpawn.position, rocketSpawn.right, Mathf.Infinity, LayerMask.GetMask("AimAssist"));
        if (aimAssitHit.collider != null) {
          rocket.target = aimAssitHit.transform.parent.position;
        } else {
          rocket.target = null;
        }
        rocketTimer = rocketCooldown;
        firing = 1f;
      } else if (radiowaveTimer <= 0f && rw <= r && rw <= l) {
        Radiowave radiowave = Instantiate(radiowavePrefab, Vector3.zero, radiowaveSpawn.rotation).GetComponent<Radiowave>();
        radiowave.spawn = radiowaveSpawn;
        radiowaveTimer = radiowaveCooldown;
        firing = radiowaveCooldown;
      } else if (laserTimer <= 0f && l <= r && l <= rw) {
        Laser laser = Instantiate(laserPrefab, laserSpawn.position, laserSpawn.rotation).GetComponent<Laser>();
        laser.spawn = laserSpawn;
        laserTimer = laserCooldown;
        firing = laserCooldown;
      }
    }

    if (speedDownTimer > 0f) {
      speedDownTimer -= Time.deltaTime;
      float magnitude = (Random.value <= 0.5f ? -1f : 1f) * Random.Range(90f, 180f);
      pivot.Rotate(magnitude * Vector3.forward, Space.World);
      if (speedDownTimer <= 0f) {
        speed = fullSpeed;
      }
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    firing = 1f;
    if (collider.gameObject.tag == "OrbitDeadly") {
      rigidbody.AddForce(transform.position.normalized * repulsiveForce, ForceMode2D.Impulse);
    }
  }

  void SonarHit(Vector3 origin) {
    if (speedDownTimer <= 0f) {
      speed = speed * speedDownMultiplier;
      speedDownTimer = speedDownTime;

      rigidbody.AddForce((transform.position - origin).normalized * 30f, ForceMode2D.Impulse);
    }
  }
}
