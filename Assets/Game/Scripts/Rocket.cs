using UnityEngine;

public class Rocket : MonoBehaviour {

  public AudioClip clip;

  public float speed;
  public float damage;
  public Vector3? target;

  float timer = 0f;

  bool upgraded = false;
  public float highSpeed;
  public float highDamage;

  public void Upgrade() {
    upgraded = true;
  }

  void Update() {
    if (target.HasValue) {
      Vector3 delta = target.Value - transform.position;
      transform.Translate(delta.normalized * (upgraded ? highSpeed : speed) * Time.deltaTime, Space.World);
    } else {
      transform.Translate(transform.right * (upgraded ? highSpeed : speed) * Time.deltaTime, Space.World);
    }
    timer += Time.deltaTime;
    if (timer >= 10f) {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    collision.gameObject.SendMessage("RocketHit", upgraded ? highDamage : damage, SendMessageOptions.DontRequireReceiver);
    Audio.source.PlayOneShot(clip);
    Destroy(gameObject);
  }
}
