using UnityEngine;

public class Asteroids : MonoBehaviour {

	void Update() {
    if (transform.position.magnitude > 100f) {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    // todo: add explosion
    Destroy(gameObject);
  }
}
