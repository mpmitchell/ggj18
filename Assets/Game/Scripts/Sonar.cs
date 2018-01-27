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
    lineRenderer.SetPosition(1, spawn.position + -spawn.forward * 100f);

    RaycastHit2D hit = Physics2D.BoxCast(spawn.position, new Vector2(4f, 2f), transform.rotation.z, transform.InverseTransformDirection(-spawn.forward), Mathf.Infinity, LayerMask.GetMask("AlienPlayer"));

    if (hit.collider != null) {
      hit.collider.SendMessage("SonarHit", SendMessageOptions.DontRequireReceiver);
    }

    timer += Time.deltaTime;
    if (timer >= timeToLive) {
      Destroy(gameObject);
    }
  }
}
