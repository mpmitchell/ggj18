using UnityEngine;

public class PickupSpawner : MonoBehaviour {

  public Transform[] earthPickupPrefabs;
  public Transform[] alienPickupPrefabs;

  public float earthCircleRadius = 5.5f;
  public float maxNumberOfEarthPickups;
  public float maxNumberOfAlienPickups;

  public float timeBetweenTicks;

  public float baseChance;
  public float maxChance;
  public float speed;

  float timer = 0f;
  float chance;

  void Start() {
    chance = baseChance;
  }

  void Update() {
    if (timer <= 0f) {
      if (Random.value >= (1f - chance)) {
        if (EarthPickup.count < maxNumberOfEarthPickups) {
          Transform pickup = Instantiate(earthPickupPrefabs[Random.Range(0, earthPickupPrefabs.Length)]);
          pickup.position = Random.insideUnitCircle.normalized * earthCircleRadius;
        }

        if (AlienPickup.count < maxNumberOfAlienPickups) {
          Transform pickup = Instantiate(alienPickupPrefabs[Random.Range(0, alienPickupPrefabs.Length)]);

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

            Collider2D[] colliders = Physics2D.OverlapCircleAll(Vector2.zero, earthCircleRadius, LayerMask.GetMask("Pickup"));

            foreach (Collider2D collider in colliders) {
              if (collider.transform == pickup) {
                done = false;
                break;
              }
            }
          }

        chance = baseChance;
        }

        if (chance <= maxChance) {
          chance += Mathf.Min(maxChance, Time.deltaTime * speed);
        }

        timer = timeBetweenTicks;
      } else {
        timer -= Time.deltaTime;
      }
    }
  }
}
