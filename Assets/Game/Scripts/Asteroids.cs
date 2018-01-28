using UnityEngine;

public class Asteroids : MonoBehaviour {

	void Update() {
    if (transform.position.magnitude > 100f) {
      Destroy(gameObject);
    }
  }
}
