using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasBtn : MonoBehaviour
{
  public Sprite btn, btnPressed;
  private Image _image;

  void Start()
  {
    CarController.isLose = false;
    CarController.countCars = 0;
    _image = GetComponent<Image>();
  }

  public void PlayGame()
  {
    StartCoroutine(LoadScene("Game"));
  }
  
  public void SetPressedBtn()
  {
    _image.sprite = btnPressed;
    transform.GetChild(0).localPosition -= new Vector3(0, 5f, 0);
  }
  public void SetDefaultBtn()
  {
    _image.sprite = btn;
    transform.GetChild(0).localPosition += new Vector3(0, 5f, 0);
  }

  IEnumerator LoadScene(string name)
  {
    float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);
    yield return new WaitForSeconds(fadeTime);
    SceneManager.LoadScene(name);
  }
}
