using UnityEngine;

public class Planet : MonoBehaviour {

    public int health;
    public GameObject tower;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject tower4;

    new public MeshRenderer renderer;

    void Start()
    {
        health = 400;
    }
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Destroy(tower.gameObject);
            Destroy(tower2.gameObject);
            Destroy(tower2.gameObject);
            Destroy(tower3.gameObject);
        }

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
