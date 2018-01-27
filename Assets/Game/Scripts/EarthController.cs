using UnityEngine;

public class EarthController : MonoBehaviour {

  public float speed;
  public GameObject satellite;
  public Material antiRadiowaveMaterial;
  public Material antiRocketMaterial;
  public Material antiLaserMaterial;

    MeshRenderer renderer;
  ShieldType shield = ShieldType.Rocket;

  enum ShieldType {
    Radiowave, Rocket, Laser
  }

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
      shield = ShieldType.Radiowave;
      renderer.material = antiRadiowaveMaterial;
      gameObject.layer = LayerMask.NameToLayer("RadiowaveShield");
    } else if (Input.GetButtonDown("Anti-Rocket")) {
      shield = ShieldType.Rocket;
      renderer.material = antiRocketMaterial;
      gameObject.layer = LayerMask.NameToLayer("RocketShield");
    } else if (Input.GetButtonDown("Anti-Laser")) {
      shield = ShieldType.Laser;
      renderer.material = antiLaserMaterial;
      gameObject.layer = LayerMask.NameToLayer("LaserShield");
    }
  }

  void LaserHit(float damage) {
    if (shield == ShieldType.Laser) {
      //
    } else {
      //
    }
  }

  void RocketHit(float damage) {
    if (shield == ShieldType.Rocket) {
      //
    } else {
      //
    }
  }

  void RadiowaveHit() {
    if (shield == ShieldType.Radiowave) {
      //
    } else {
      //
    }
  }
}
