using UnityEngine;

public class Repair : EarthPickup {

  public int healAmount;

  void Pickup(EarthController player) {
    TowerScript tower = TowerScript.GetMostDamagedTower();
    tower.Heal(healAmount);
    Destroy(gameObject);
  }
}
