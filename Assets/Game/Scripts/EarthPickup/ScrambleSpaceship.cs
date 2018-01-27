using UnityEngine;

public class ScrambleSpaceship : EarthPickup {

  void Pickup(EarthController player) {
    AlienController.alien.Invert();
    Destroy(gameObject);
  }
}
