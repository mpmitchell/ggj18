using UnityEngine;

public class Rocket : MonoBehaviour {

  public float speed;
  public float damage;
  public Vector3? target;

  float timer = 0f;

  void Update() {
    if (target.HasValue) {
      Vector3 delta = target.Value - transform.position;
      transform.Translate(delta.normalized * speed * Time.deltaTime, Space.World);
    } else {
      transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }
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
