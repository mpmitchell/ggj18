using UnityEngine;

public class ParticlePrefab : MonoBehaviour {

  public float timeToLive;

  void Update() {
    timeToLive -= Time.deltaTime;
    if (timeToLive <= 0f) {
      Destroy(gameObject);
    }
  }
}
