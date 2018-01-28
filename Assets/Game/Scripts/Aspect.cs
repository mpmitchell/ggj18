using UnityEngine;

public class Aspect : MonoBehaviour {

  float targetAspect = 4f / 3f;

  void Awake() {
    float height = (float)Screen.height;
    float width = (float)Screen.width;
    float aspect = height / width;
    float targetWidth = height * targetAspect;

    if ((int)(aspect * 100) / 100f == (int)(targetAspect * 100) / 100f) {
      Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
    } else {
      Camera.main.pixelRect = new Rect(
        (width - targetWidth) / 2f,
        0f,
        targetWidth,
        height
      );
    }
  }
}
