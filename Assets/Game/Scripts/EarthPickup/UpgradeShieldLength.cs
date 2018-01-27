using UnityEngine;

public class UpgradeShieldLength : EarthPickup {

  public float scale;

  void Pickup(EarthController player) {
    player.Scale(scale);
    Destroy(gameObject);
  }
}
