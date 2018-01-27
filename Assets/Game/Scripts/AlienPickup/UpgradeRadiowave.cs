using UnityEngine;

public class UpgradeRadiowave : AlienPickup {

  void Pickup(AlienController player) {
    player.UpgradeRadiowave();
    Destroy(gameObject);
  }
}
