using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasBtn : MonoBehaviour
{
  public Sprite btn, btnPressed;
  private Image _image;
  private Coroutine changeBackCoroutine;
  public AudioSource audio;

  void Start()
  {
    CarController.isLose = false;
    CarController.countCars = 0;
    _image = GetComponent<Image>();
  }

  public void ShopScene()
  {
    StartCoroutine(LoadScene("Shop"));
  }
  public void ExitShopScene()
  {
    StartCoroutine(LoadScene("Main"));
  }

  public void PlayGame()
  {
    if (PlayerPrefs.GetString("First Game") == "No")
      StartCoroutine(LoadScene("Game"));
      
    else
    {
      PlayerPrefs.SetString("First Game", "No");
      StartCoroutine(LoadScene("Study"));
      
    }
    CarController.BtnPressed = false;
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
  public void OnPointerDown()
  {

    _image.sprite = btnPressed;
    CarController.BtnPressed = true;
    audio.Play();
    if (changeBackCoroutine != null)
    {
      StopCoroutine(changeBackCoroutine);
    }
    
    changeBackCoroutine = StartCoroutine(ChangeBackAfterDelay(5.0f));
    
    
  }
  private IEnumerator ChangeBackAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    
    _image.sprite = btn;
    CarController.BtnPressed = false;
    changeBackCoroutine = null;
  }
}
