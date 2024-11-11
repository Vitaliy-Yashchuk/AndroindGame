using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondCar : MonoBehaviour
{
    private void OnDestroy()
    {
        PlayerPrefs.SetString("SecondCar", "No");
        SceneManager.LoadScene("Game");
    }
}
