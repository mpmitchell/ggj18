using UnityEngine;
using System.Collections;

public class AlienSpawner : MonoBehaviour {

  public float spawnTimer;
  public GameObject alienPrefab;
  public GameObject current;

  public void DestroyCurrent() {
    Destroy(current);
    StartCoroutine(Spawn());
  }

  IEnumerator Spawn() {
    yield return new WaitForSeconds(spawnTimer);
    current = Instantiate<GameObject>(alienPrefab, transform.position, Quaternion.identity);
    current.GetComponent<AlienController>().spawner = this;
  }
}
