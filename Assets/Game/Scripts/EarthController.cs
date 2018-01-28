using UnityEngine;

public class EarthController : MonoBehaviour {

  public static EarthController earthPlayer;

  public GameObject particles;

  public Transform shield;

  public float speed;
  public GameObject satellite;
  public Material antiRadiowaveMaterial;
  public Material antiRocketMaterial;
  public Material antiLaserMaterial;

  new MeshRenderer renderer;

  public GameObject sonarPrefab;
  public Transform sonarSpawn;
  public float sonarCooldown;
  float sonarTimer = 0f;

  float speedMultiplier = 1f;
  float speedIncreaseTimer = 0f;
  public float speedIncreaseTime;

  public float scaleTime;
  float scaleTimer = 0f;

  bool inverted = false;
  public float invertedTime;
  float invertedTimer = 0f;

  float baseScale;

  void Start() {
    earthPlayer = this;
    baseScale = shield.localScale.x;
    renderer = satellite.GetComponent<MeshRenderer>();
  }

  void Update() {
    transform.RotateAround(
      Vector3.zero,
      Vector3.forward,
      -Input.GetAxis("EarthHorizontal") * (inverted ? -1f : 1f) * speedMultiplier * speed * Time.deltaTime
    );

    if (Input.GetButtonDown("Anti-Radiowave")) {
      renderer.material = antiRadiowaveMaterial;
      gameObject.layer = LayerMask.NameToLayer("RadiowaveShield");
      shield.gameObject.layer = LayerMask.NameToLayer("RadiowaveShield");
    } else if (Input.GetButtonDown("Anti-Rocket")) {
      renderer.material = antiRocketMaterial;
      gameObject.layer = LayerMask.NameToLayer("RocketShield");
      shield.gameObject.layer = LayerMask.NameToLayer("RocketShield");
    } else if (Input.GetButtonDown("Anti-Laser")) {
      renderer.material = antiLaserMaterial;
      gameObject.layer = LayerMask.NameToLayer("LaserShield");
      shield.gameObject.layer = LayerMask.NameToLayer("LaserShield");
    }

    if (sonarTimer <= 0f) {
      if (Input.GetButtonDown("EarthFire")) {
        Sonar sonar = Instantiate(sonarPrefab).GetComponent<Sonar>();
        sonar.spawn = sonarSpawn;
        sonarTimer = sonarCooldown;
      }
    } else {
      sonarTimer -= Time.deltaTime;
    }

    if (speedIncreaseTimer > 0f) {
      speedIncreaseTimer -= Time.deltaTime;
      if (speedIncreaseTimer <= 0f) {
        speedMultiplier = 1f;
      }
    }

    if (scaleTimer > 0f) {
      scaleTimer -= Time.deltaTime;
      if (scaleTimer <= 0f) {
        Vector3 scale = shield.localScale;
        scale.x = baseScale;
        shield.localScale = scale;
      }
    }

    if (invertedTimer > 0f) {
      invertedTimer -= Time.deltaTime;
      if (invertedTimer <= 0f) {
        inverted = false;
      }
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "EarthPickup") {
      Instantiate(particles, transform.position, transform.rotation);
      collider.gameObject.SendMessage("Pickup", this);
    }
  }

  public void SpeedIncrease(float speedMultiplier) {
    if (speedIncreaseTimer <= 0f) {
      speedIncreaseTimer = speedIncreaseTime;
      this.speedMultiplier = speedMultiplier;
    }
  }

  public void Scale(float newScale) {
    if (scaleTimer <= 0f) {
      scaleTimer = scaleTime;
      Vector3 scale = shield.localScale;
      scale.x = newScale;
      shield.localScale = scale;
    }
  }

  public void Invert() {
    if (invertedTimer <= 0f) {
      invertedTimer = invertedTime;
      inverted = true;
    }
  }
}
