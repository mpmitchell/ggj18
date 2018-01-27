using UnityEngine;

public class EarthController : MonoBehaviour {

  public float speed;
  public Material antiRadiowaveMaterial;
  public Material antiRocketMaterial;
  public Material antiLaserMaterial;

  MeshRenderer renderer;
  ShieldType shield = ShieldType.Rocket;

  enum ShieldType {
    Radiowave, Rocket, Laser
  }

  void Start() {
    renderer = GetComponent<MeshRenderer>();
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
    } else if (Input.GetButtonDown("Anti-Rocket")) {
      shield = ShieldType.Rocket;
      renderer.material = antiRocketMaterial;
    } else if (Input.GetButtonDown("Anti-Laser")) {
      shield = ShieldType.Laser;
      renderer.material = antiLaserMaterial;
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
