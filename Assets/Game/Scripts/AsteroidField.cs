using UnityEngine;

public class AsteroidField : MonoBehaviour {

	public GameObject[] prefabs;
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
        GameObject asteroid = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
        asteroid.transform.localScale = asteroid.transform.localScale * Random.Range(0.1f, 1f);
        Vector3 position = Random.onUnitSphere * 100f;
        position.z = 0f;
        asteroid.transform.position = position;

        Vector3 target = Vector2.zero + Random.insideUnitCircle * 30f;
        Vector3 dir = target - position;
        Vector3 velocity = dir * Random.Range(1f, 5f);
        asteroid.GetComponent<Rigidbody2D>().AddForce(velocity);

        chance = baseChance;
      }

      if (chance <= maxChance) {
        chance = Mathf.Min(maxChance, chance + speed + Time.deltaTime);
      }

      timer = timeBetweenTicks;
    } else {
      timer -= Time.deltaTime;
    }
  }
}
