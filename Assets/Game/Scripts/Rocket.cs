using UnityEngine;

public class Rocket : MonoBehaviour {

  public float speed;
  public float damage;

  float timer = 0f;

  void Update() {
    transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    timer += Time.deltaTime;
    if (timer >= 10f) {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    collision.gameObject.SendMessage("RocketHit", damage, SendMessageOptions.DontRequireReceiver);
    Debug.Log(collision.gameObject);
    Destroy(gameObject);
  }
}
