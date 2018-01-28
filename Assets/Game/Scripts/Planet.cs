using UnityEngine;

public class Planet : MonoBehaviour {

    public int health;

    new public MeshRenderer renderer;

    void Start()
    {
        health = 400;
    }
    private void Update()
    {
        if (health <= 0)
            Destroy(this.gameObject);

        renderer.material.color = Color.Lerp(Color.red, Color.white, (float)health / 600f);
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
