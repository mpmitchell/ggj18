using UnityEngine;

public class Radiowave : MonoBehaviour {

  // public Transform spawn;
  // public float timeToLive;

  public float speed;
  public Vector3? target;

  // LineRenderer lineRenderer;
  float timer = 0f;

  bool upgraded = false;
  public float highSpeed;

  public void Upgrade() {
    upgraded = true;
  }

  void Start() {
    // lineRenderer = GetComponent<LineRenderer>();
  }

  void Update() {
    // Vector3 position = spawn.transform.position;
    // lineRenderer.SetPosition(0, position);

    // RaycastHit2D hit = Physics2D.Raycast(spawn.position, spawn.right, Mathf.Infinity, LayerMask.GetMask("Planet", "Towers", "RadiowaveShield"));

    // if (hit.collider != null) {
    //   lineRenderer.SetPosition(1, hit.point);
    //   hit.collider.SendMessage("RadiowaveHit", SendMessageOptions.DontRequireReceiver);
    // } else {
    //   lineRenderer.SetPosition(1, spawn.position + spawn.right * 100f);
    // }

    // timer += Time.deltaTime;
    // if (timer >= timeToLive) {
    //   Destroy(gameObject);
    // }

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
    collision.gameObject.SendMessage(upgraded ? "UpgradedRadiowaveHit" : "RadiowaveHit", SendMessageOptions.DontRequireReceiver);
    Destroy(gameObject);
  }
}
