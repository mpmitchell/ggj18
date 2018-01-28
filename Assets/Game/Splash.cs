using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

  float firstTimer = 2f;
  float fadeTimer = 1f;

  [SerializeField] Image image;

  void Update() {
    firstTimer -= Time.deltaTime;
    if (firstTimer <= 0f) {
      fadeTimer -= Time.deltaTime;
      image.color = Color.Lerp(Color.black, Color.white, fadeTimer);
      if (fadeTimer <= 0f) {
        SceneManager.LoadScene(1);
      }
    }
  }
}
