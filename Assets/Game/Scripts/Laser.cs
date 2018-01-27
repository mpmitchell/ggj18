using UnityEngine;

public class Laser : MonoBehaviour {

  public Transform spawn;
  public float timeToLive;
  public float timeBetweenTicks;
  public float damagePerTick;
  public Transform particleEffect;

  public float highTimeToLive;
  public float highDamage;

  LineRenderer lineRenderer;
  float timer = 0f;
  float damageTimer = 0f;

  bool upgraded = false;

  public void Upgrade() {
    upgraded = true;
  }

  void Start() {
    lineRenderer = GetComponent<LineRenderer>();
  }

  void Update() {
    Vector3 position = spawn.transform.position;
    lineRenderer.SetPosition(0, position);

    RaycastHit2D laserShieldHit = Physics2D.Raycast(spawn.position, spawn.right, Mathf.Infinity, LayerMask.GetMask("LaserShield"));
    RaycastHit2D hit = Physics2D.Raycast(spawn.position, spawn.right, Mathf.Infinity, LayerMask.GetMask("Planet", "Towers", "LaserShield"));
    RaycastHit2D aimAssistHit = Physics2D.Raycast(spawn.position, spawn.right, Mathf.Infinity, LayerMask.GetMask("AimAssist"));

    if (aimAssistHit.collider != null && laserShieldHit == null) {
      lineRenderer.SetPosition(1, aimAssistHit.transform.parent.position);
      particleEffect.position = aimAssistHit.transform.parent.position;
    } else if (hit.collider != null) {
      lineRenderer.SetPosition(1, hit.point);
      particleEffect.position = hit.point;
    } else {
      lineRenderer.SetPosition(1, spawn.position + spawn.right * 100f);
      particleEffect.position = spawn.position + spawn.right * 100f;
    }

    damageTimer += Time.deltaTime;
    if (damageTimer >= timeBetweenTicks) {
      if (hit.collider != null) {
        hit.collider.gameObject.SendMessage("LaserHit", upgraded ? highDamage : damagePerTick, SendMessageOptions.DontRequireReceiver);
      }
      damageTimer = 0f;
    }

    timer += Time.deltaTime;
    if (timer >= (upgraded ? highTimeToLive : timeToLive)) {
      Destroy(gameObject);
    }
  }
}
