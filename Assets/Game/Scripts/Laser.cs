using UnityEngine;

public class Laser : MonoBehaviour {

  public float speed;

  Transform origin;
  Rigidbody rigidbody;

  void Start() {
    origin = transform;
    rigidbody = GetComponent<Rigidbody>();
  }

  void Update() {
    Vector3 transformation = transform.up * speed * Time.deltaTime;
    rigidbody.MovePosition(transform.position + transform.TransformDirection(transformation));
  }

  void OnTriggetEnter(Collider other) {
    if (other.tag == "Planet") {
      speed = 0f;
    }
  }
}
