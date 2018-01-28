using UnityEngine;

public class PickupSpawner : MonoBehaviour {

  public static int earthPickupCount = 0;
  public static int alienPickupCount = 0;

  public Transform[] earthPickupPrefabs;
  public Transform[] alienPickupPrefabs;

  public float earthCircleRadius = 5.5f;
  public float maxNumberOfEarthPickups;
  public float maxNumberOfAlienPickups;

  public float timeBetweenTicks;

  public float baseChance;
  public float maxChance;
  public float speed;

  public float timer = 0f;
  public float chance;

  void Start() {
    chance = baseChance;
  }

  void Update() {
    if (timer <= 0f) {
      if (Random.value >= (1f - chance)) {
        if (earthPickupCount < maxNumberOfEarthPickups && alienPickupCount >= maxNumberOfAlienPickups) {
          SpawnEarthPickup();
        } else if (alienPickupCount < maxNumberOfAlienPickups && earthPickupCount >= maxNumberOfEarthPickups) {
            SpawnAlienPickup();
        } else {
          if (Random.value <= 0.5f) {
            SpawnEarthPickup();
          } else {
            SpawnAlienPickup();
          }
        }
        chance = baseChance;
      }
      if (chance <= maxChance) {
        chance = Mathf.Min(maxChance, chance + Time.deltaTime * speed);
      }
      timer = timeBetweenTicks;
    } else {
      timer -= Time.deltaTime;
    }
  }

  void SpawnEarthPickup() {
    Transform pickup = Instantiate(earthPickupPrefabs[Random.Range(0, earthPickupPrefabs.Length)]);
    earthPickupCount++;
    pickup.position = Random.insideUnitCircle.normalized * earthCircleRadius;
  }

  void SpawnAlienPickup() {
    Transform pickup = Instantiate(alienPickupPrefabs[Random.Range(0, alienPickupPrefabs.Length)]);
    alienPickupCount++;

    Vector3 minBounds = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, -30f));
    Vector3 maxBounds = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, -30f));

    bool done = false;

    while (!done) {
      done = true;

      pickup.position = new Vector3(
        Random.Range(minBounds.x, maxBounds.x),
        Random.Range(minBounds.y, maxBounds.y),
        0f
      );

      Collider2D[] colliders = Physics2D.OverlapCircleAll(Vector2.zero, earthCircleRadius * 1.5f, LayerMask.GetMask("Pickup"));

      foreach (Collider2D collider in colliders) {
        if (collider.transform == pickup) {
          done = false;
          break;
        }
      }
    }
  }
}
