using UnityEngine;

public class Rocket : MonoBehaviour {

  public float speed;
  public float damage;
  public Vector3 target;

  float timer = 0f;

  void Update() {
    Vector3 delta = target - transform.position;
    transform.Translate(delta.normalized * speed * Time.deltaTime, Space.World);
    // transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    timer += Time.deltaTime;
    if (timer >= 10f) {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    collision.gameObject.SendMessage("RocketHit", damage, SendMessageOptions.DontRequireReceiver);
    Destroy(gameObject);
  }
}
