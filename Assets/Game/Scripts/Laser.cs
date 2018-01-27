using UnityEngine;

public class Laser : MonoBehaviour {

  public Transform spawn;
  public float timeToLive;
  public float timeBetweenTicks;
  public float damagePerTick;

  LineRenderer lineRenderer;
  float timer = 0f;
  float damageTimer = 0f;

  void Start() {
    lineRenderer = GetComponent<LineRenderer>();
  }

  void Update() {
    Vector3 position = spawn.transform.position;

    RaycastHit2D hit = Physics2D.Raycast(spawn.position, spawn.right, Mathf.Infinity, LayerMask.GetMask("Planet", "Towers", "EarthPlayer"));

    if (hit.collider != null) {
      lineRenderer.enabled = true;
      lineRenderer.SetPosition(0, position);
      lineRenderer.SetPosition(1, hit.point);
    } else {
      lineRenderer.enabled = false;
    }

    damageTimer += Time.deltaTime;
    if (damageTimer >= timeBetweenTicks) {
      if (hit.collider != null) {
        hit.collider.gameObject.SendMessage("LaserHit", damagePerTick, SendMessageOptions.DontRequireReceiver);
      }
      damageTimer = 0f;
    }

    timer += Time.deltaTime;
    if (timer >= timeToLive) {
      Destroy(gameObject);
    }
  }
}
