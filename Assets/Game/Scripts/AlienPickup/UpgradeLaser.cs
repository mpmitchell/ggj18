﻿using UnityEngine;

public class UpgradeLaser : AlienPickup {

  void Pickup(AlienController player) {
    player.UpgradeLaser();
    Destroy(gameObject);
  }
}
