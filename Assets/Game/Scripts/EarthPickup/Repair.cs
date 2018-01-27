using UnityEngine;

public class Repair : EarthPickup {

  public int healAmount;

  void Pickup(EarthController player) {
    TowerScript[] towers = TowerScript.GetTowers();
    if (towers.Length > 0) {
      TowerScript tower = towers[Random.Range(0, towers.Length)];
      tower.Heal(healAmount);
    }
    Destroy(gameObject);
  }
}
