using UnityEngine;

public class Sonar : MonoBehaviour {

  public Transform spawn;
  public float timeToLive;

  LineRenderer lineRenderer;
  float timer = 0f;

  void Start() {
    lineRenderer = GetComponent<LineRenderer>();
  }

  void Update() {
    Vector3 position = spawn.transform.position;
    lineRenderer.SetPosition(0, position);
    lineRenderer.SetPosition(1, spawn.position + spawn.up * 100f);

    // RaycastHit2D hit = Physics2D.Raycast(spawn.position, spawn.up, Mathf.Infinity, LayerMask.GetMask("AlienPlayer"));
    // RaycastHit2D hit2 = Physics2D.Raycast(spawn.position - spawn.right * 2, spawn.up, Mathf.Infinity, LayerMask.GetMask("AlienPlayer"));
    // RaycastHit2D hit3 = Physics2D.Raycast(spawn.position + spawn.right * 2, spawn.up, Mathf.Infinity, LayerMask.GetMask("AlienPlayer"));
    // RaycastHit2D hit4 = Physics2D.Raycast(spawn.position - spawn.right, spawn.up, Mathf.Infinity, LayerMask.GetMask("AlienPlayer"));
    // RaycastHit2D hit5 = Physics2D.Raycast(spawn.position + spawn.right, spawn.up, Mathf.Infinity, LayerMask.GetMask("AlienPlayer"));

    RaycastHit2D boxhit = Physics2D.BoxCast(spawn.position, new Vector2(4f, 1f), transform.rotation.z, spawn.up, Mathf.Infinity, LayerMask.GetMask("AlienPlayer"));

    if (boxhit.collider != null) {
      boxhit.collider.SendMessage("SonarHit", spawn.transform.position, SendMessageOptions.DontRequireReceiver);
    }
    // if (hit.collider != null) {
    //   hit.collider.SendMessage("SonarHit", spawn.transform.position, SendMessageOptions.DontRequireReceiver);
    // } else if (hit2.collider != null) {
    //   hit2.collider.SendMessage("SonarHit", spawn.transform.position, SendMessageOptions.DontRequireReceiver);
    // } else if (hit3.collider != null) {
    //   hit3.collider.SendMessage("SonarHit", spawn.transform.position, SendMessageOptions.DontRequireReceiver);
    // } else if (hit4.collider != null) {
    //   hit4.collider.SendMessage("SonarHit", spawn.transform.position, SendMessageOptions.DontRequireReceiver);
    // } else if (hit5.collider != null) {
    //   hit5.collider.SendMessage("SonarHit", spawn.transform.position, SendMessageOptions.DontRequireReceiver);
    // }

    timer += Time.deltaTime;
    if (timer >= timeToLive) {
      Destroy(gameObject);
    }
  }
}
