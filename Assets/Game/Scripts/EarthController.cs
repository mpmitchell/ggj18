using UnityEngine;

public class EarthController : MonoBehaviour {

  public float speed;

  void Update() {
    transform.RotateAround(
      Vector3.zero,
      Vector3.forward,
      -Input.GetAxis("EarthHorizontal") * speed * Time.deltaTime
    );
  }
}
