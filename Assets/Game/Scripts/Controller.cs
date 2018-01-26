using UnityEngine;

public class Controller : MonoBehaviour {

  public float speed = 90f;
  public string inputAxis;

  void Update() {
    transform.RotateAround(
      Vector3.zero,
      Vector3.forward,
      -Input.GetAxis(inputAxis) * speed * Time.deltaTime
    );
  }
}
