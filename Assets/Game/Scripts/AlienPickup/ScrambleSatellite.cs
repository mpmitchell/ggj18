using UnityEngine;

public class ScrambleSatellite : AlienPickup {

  void Pickup(AlienController player) {
    EarthController.earthPlayer.Invert();
    Destroy(gameObject);
  }
}
