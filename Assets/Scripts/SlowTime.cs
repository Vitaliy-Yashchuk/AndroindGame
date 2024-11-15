using UnityEngine;
using UnityEngine.UI;

public class SlowTime : MonoBehaviour {

    private Text countSlows;
    public Sprite noSlowTime;
    private Image _image;
    private AudioSource _audioSource;
    
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        
        if (!PlayerPrefs.HasKey("SlowTime")) {
            PlayerPrefs.SetInt("SlowTime", 3);
        }

        countSlows = transform.GetChild(0).GetComponent<Text>();
        countSlows.text = PlayerPrefs.GetInt("SlowTime").ToString();
        
        _image = GetComponent<Image>();
        if (PlayerPrefs.GetInt("SlowTime") == 0)
            _image.sprite = noSlowTime;
    }

    public void SetSlowTime() {
        if (PlayerPrefs.GetInt("SlowTime") == 0 || Time.timeScale == 0.5f)
            return;
        Time.timeScale = 0.5f;
        
        PlayerPrefs.SetInt("SlowTime", PlayerPrefs.GetInt("SlowTime") - 1);

        Invoke("RemoveSlowTime", 5f);
        
        countSlows.text = PlayerPrefs.GetInt("SlowTime").ToString();
        
        if (PlayerPrefs.GetInt("SlowTime") == 0)
            _image.sprite = noSlowTime;
        
        if (PlayerPrefs.GetString("music") != "No")
            _audioSource.Play();
    }

    void RemoveSlowTime() {
        Time.timeScale = 1;
    }
    
}
