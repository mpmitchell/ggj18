using UnityEngine;

public class DefenderController : MonoBehaviour {

  public float speed;

  void Update() {
    transform.RotateAround(
      Vector3.zero,
      Vector3.forward,
      -Input.GetAxis("EarthHorizontal") * speed * Time.deltaTime
    );
  }
}
