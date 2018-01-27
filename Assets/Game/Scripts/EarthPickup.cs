using UnityEngine;

public class EarthPickup : MonoBehaviour {

  public static int count = 0;

  void Start() {
    count++;
  }

  void OnDestroy() {
    count--;
  }
}
