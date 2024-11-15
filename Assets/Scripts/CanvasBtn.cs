using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasBtn : MonoBehaviour
{
  public Sprite btn, btnPressed, musicOn, musicOff;
  private Image _image;
  private Coroutine changeBackCoroutine;
 

  void Start()
  {
    CarController.isLose = false;
    CarController.countCars = 0;
    _image = GetComponent<Image>();
    if (gameObject.name == "MusicBtn")
    {
      if (PlayerPrefs.GetString("music") == "No")
      {
        transform.GetChild(0).GetComponent<Image>().sprite = musicOff;  
      }
    }
  }

  public void MusicButton()
  {
    if (PlayerPrefs.GetString("music") == "No")
    {
     PlayerPrefs.SetString("music", "Yes");
     transform.GetChild(0).GetComponent<Image>().sprite = musicOn;
    }
    else
    {
      PlayerPrefs.SetString("music", "No");
      transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
    }

    PlayBtnSound();
    
  }

  public void ShopScene()
  {
    StartCoroutine(LoadScene("Shop"));
    PlayBtnSound();
  }
  public void ExitShopScene()
  {
    StartCoroutine(LoadScene("Main"));
    PlayBtnSound();
  }

  public void PlayGame() {
    if(PlayerPrefs.GetString("First Game") == "No")
      StartCoroutine(LoadScene("Game"));
    else 
      StartCoroutine(LoadScene("Study"));
  }

  public void RestartGame() {
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

    changeBackCoroutine = null;
  }

  private void PlayBtnSound()
  {
    if (PlayerPrefs.GetString("music")!="No")
    {
      GetComponent<AudioSource>().Play();
    }
  }
}
