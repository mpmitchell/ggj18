using UnityEngine;

public class AlienPickup : MonoBehaviour {

  void OnDestroy() {
    PickupSpawner.alienPickupCount--;
  }
}
