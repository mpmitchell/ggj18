using UnityEngine;

public class Radiowave : MonoBehaviour {

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

    RaycastHit2D hit = Physics2D.Raycast(spawn.position, spawn.right, Mathf.Infinity, LayerMask.GetMask("Planet", "Towers", "RadiowaveShield"));

    if (hit.collider != null) {
      lineRenderer.SetPosition(1, hit.point);
      hit.collider.SendMessage("RadiowaveHit", SendMessageOptions.DontRequireReceiver);
    } else {
      lineRenderer.SetPosition(1, spawn.position + spawn.right * 100f);
    }

    timer += Time.deltaTime;
    if (timer >= timeToLive) {
      Destroy(gameObject);
    }
  }
}
