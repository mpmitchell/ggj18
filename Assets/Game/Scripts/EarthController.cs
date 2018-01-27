using UnityEngine;

public class EarthController : MonoBehaviour {

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

  void Start() {
    renderer = satellite.GetComponent<MeshRenderer>();
  }

  void Update() {
    transform.RotateAround(
      Vector3.zero,
      Vector3.forward,
      -Input.GetAxis("EarthHorizontal") * speed * Time.deltaTime
    );

    if (Input.GetButtonDown("Anti-Radiowave")) {
      renderer.material = antiRadiowaveMaterial;
      gameObject.layer = LayerMask.NameToLayer("RadiowaveShield");
    } else if (Input.GetButtonDown("Anti-Rocket")) {
      renderer.material = antiRocketMaterial;
      gameObject.layer = LayerMask.NameToLayer("RocketShield");
    } else if (Input.GetButtonDown("Anti-Laser")) {
      renderer.material = antiLaserMaterial;
      gameObject.layer = LayerMask.NameToLayer("LaserShield");
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
  }
}
