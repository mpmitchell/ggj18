using UnityEngine;

public class AlienPickup : MonoBehaviour {

  public static int count = 0;

  void Start() {
    count++;
  }

  void OnDestroy() {
    count--;
  }
}
