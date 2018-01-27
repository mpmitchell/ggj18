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

    RaycastHit2D hit = Physics2D.Raycast(spawn.position, spawn.right, Mathf.Infinity, LayerMask.GetMask("Planet", "Towers", "EarthPlayer"));

    if (hit.collider != null) {
      lineRenderer.enabled = true;
      lineRenderer.SetPosition(0, position);
      lineRenderer.SetPosition(1, hit.point);
    } else {
      lineRenderer.enabled = false;
    }

    timer += Time.deltaTime;
    if (timer >= timeToLive) {
      Destroy(gameObject);
    }
  }
}
