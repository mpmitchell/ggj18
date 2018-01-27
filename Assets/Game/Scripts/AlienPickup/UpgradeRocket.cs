using UnityEngine;

public class UpgradeRocket : AlienPickup {

  void Pickup(AlienController player) {
    player.UpgradeRocket();
    Destroy(gameObject);
  }
}
