using UnityEngine;

public class UpgradeMovementSpeed : EarthPickup {

  public float speedMultiplier;

  void Pickup(EarthController player) {
    player.SpeedIncrease(speedMultiplier);
    Destroy(gameObject);
  }
}
