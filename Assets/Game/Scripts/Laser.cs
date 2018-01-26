using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

  public float speed;

  Vector3 originPosition;
  Vector3 originRight;
  Rigidbody2D rigidbody;

  public float damagePerTick;
  public float timeBetweenTicks;
  public float timeToLive;

  float timer = 0f;

  void Start() {
    originPosition = transform.position;
    originRight = transform.right;
    rigidbody = GetComponent<Rigidbody2D>();
    StartCoroutine(Damage());
  }

  void Update() {
    transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    timer += Time.deltaTime;
    if (timer > timeToLive) {
      Destroy(gameObject);
    }
  }

  IEnumerator Damage() {
    while (timer <= timeToLive) {
      RaycastHit2D hit = Physics2D.Raycast(originPosition, originRight, Mathf.Infinity, LayerMask.GetMask("Planet", "Towers", "EarthPlayer"));

      if (hit.collider != null) {
        hit.collider.gameObject.SendMessage("LaserHit", damagePerTick, SendMessageOptions.DontRequireReceiver);
        Debug.Log(hit.collider.gameObject);
      }
      yield return new WaitForSeconds(timeBetweenTicks);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    speed = 0f;
    Destroy(rigidbody);
  }
}
