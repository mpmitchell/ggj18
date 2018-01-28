using UnityEngine;

public class EarthPickup : MonoBehaviour {

  void OnDestroy() {
    PickupSpawner.earthPickupCount--;
  }
}
