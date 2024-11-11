using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool isMainScene;
    public GameObject[] cars;
    public float timeToSpawnFrom = 3f;
    public float timeToSpawnTo = 7.5f;
    private int _counter;
    private Coroutine _bottomCars,_upCars,_leftCars,_rightCars;
    private bool _isLoseOnce;
    public GameObject canvasLosePanel;
    public Text nowScore,topScore,coinsScore;
    private void Start()
    {
        if (isMainScene)
        {
            timeToSpawnFrom = 4f;
            timeToSpawnTo = 6f;
        }
        _bottomCars = StartCoroutine(BottomCars());
        _leftCars = StartCoroutine(LeftCars());
        _rightCars = StartCoroutine(RightCars());
        _upCars = StartCoroutine(UpCars());
    }

    private void Update()
    {
        if (CarController.isLose && !_isLoseOnce)
        {
            StopCoroutine(_bottomCars);
            StopCoroutine(_leftCars);
            StopCoroutine(_rightCars);
            StopCoroutine(_upCars);
            nowScore.text = "<color=#F14D4E>Score:</color> "+ CarController.countCars.ToString();
            nowScore.text = "<color=#F14D4E>Best Score:</color> "+ CarController.countCars.ToString();
            if(PlayerPrefs.GetInt("Score") < CarController.countCars)
            {
                PlayerPrefs.SetInt("Score", CarController.countCars);
            }
            topScore.text = "Best score" + PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins") + CarController.countCars);
            coinsScore.text = PlayerPrefs.GetInt("Coins").ToString();
            canvasLosePanel.SetActive(true);
            _isLoseOnce = true;
        }
    }

    IEnumerator BottomCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-1.59f, -0.001841247f, -24.97f),180f);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator LeftCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-89.4f, -0.001841247f, 3.5f),270f);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    } 
    IEnumerator RightCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(26.8f, -0.001841247f, 10.2f),90f);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator UpCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-9f, -0.001841247f, 71.7f),0f);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    private void SpawnCar(Vector3 pos, float rotationY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0,cars.Length)], pos, Quaternion.Euler(0,rotationY,0)) as GameObject;
        newObj.name = "Car - " + ++_counter;
        if (isMainScene)
        {
            newObj.GetComponent<CarController>().speed = 13f;
        }
        int rand = isMainScene ? 1 : Random.Range(1, 7);
        switch (rand)
        {
            case 1:
            case 2:
                newObj.GetComponent<CarController>().rightTurn = true;
                break;
            case 3:
            case 4:
                newObj.GetComponent<CarController>().leftTurn = true;
                if (isMoveFromUp)
                {
                    newObj.GetComponent<CarController>().moveFromUp = true;
                }
                break;
            case 5:
                break;
        }
    }
}
