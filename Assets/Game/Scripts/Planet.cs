using UnityEngine;

public class Planet : MonoBehaviour {

    public int health;

    void Start()
    {
        health = 600;
    }
    private void Update()
    {
        if (health <= 0)
            Destroy(this.gameObject);
    }

    void LaserHit(int damagePerTick) {
        health -= damagePerTick;
        Debug.Log(health);
  }

  void RocketHit(int damage) {
        health -= damage;
  }

  void RadiowaveHit() {
    //
  }
}
