using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlowTime : MonoBehaviour
{
    public Sprite btn, btnPressed;
    private Image _image;
    void Start()
    {
        StartCoroutine(ExecuteForFiveSeconds());
    }

    IEnumerator ExecuteForFiveSeconds()
    {
        float duration = 5.0f; 
        float elapsedTime = 0.0f; 

        while (elapsedTime < duration)
        {
            
            Debug.Log("Функція виконується: " + elapsedTime);

            elapsedTime += Time.deltaTime; 
            yield return null;
        }

        SetDefaultBtn();
        Debug.Log("Функція завершила виконання");
    }
    public void SetPressedBtn()
    {
        _image.sprite = btnPressed;
    }
    public void SetDefaultBtn()
    {
        _image.sprite = btn;
    }
}
