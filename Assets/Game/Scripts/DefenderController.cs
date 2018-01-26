using UnityEngine;

public class DefenderController : MonoBehaviour {

  public float speed = 90f;

  void Update() {
    transform.RotateAround(
      Vector3.zero,
      Vector3.forward,
      -Input.GetAxis("Horizontal2") * speed * Time.deltaTime
    );
  }
}
