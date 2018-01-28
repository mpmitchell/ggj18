using UnityEngine;

public class Asteroids : MonoBehaviour {

  public GameObject explosion;

	void Update() {
    if (transform.position.magnitude > 100f) {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    Instantiate(explosion, transform.position, transform.rotation);
    Destroy(gameObject);
  }
}
