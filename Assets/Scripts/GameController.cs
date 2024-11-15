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
    public GameObject[] maps;
    public GameObject horn;
    public AudioSource turnSignal;
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("NowMap") == 2)
        {
            Destroy(maps[0]);
            maps[1].SetActive(true);
            Destroy(maps[2]);
        }else if (PlayerPrefs.GetInt("NowMap") == 3)
        {
            Destroy(maps[0]);
            Destroy(maps[1]);
            maps[2].SetActive(true);
        }else
        {
            maps[0].SetActive(true);
            Destroy(maps[1]);
            Destroy(maps[2]);
        }
        if (isMainScene)
        {
            timeToSpawnFrom = 4f;
            timeToSpawnTo = 6f;
        }
        _bottomCars = StartCoroutine(BottomCars());
        _leftCars = StartCoroutine(LeftCars());
        _rightCars = StartCoroutine(RightCars());
        _upCars = StartCoroutine(UpCars());
        StartCoroutine(CreateHorn());
    }

    private void Update()
    {
        
        if (CarController.isLose && !_isLoseOnce)
        {
            StopCoroutine(_bottomCars);
            StopCoroutine(_leftCars);
            StopCoroutine(_rightCars);
            StopCoroutine(_upCars);
            nowScore.text = "<color=#F14D4E>Score: </color> "+ CarController.countCars.ToString();
            //nowScore.text = "<color=#F14D4E>Best Score: </color> "+ CarController.countCars.ToString();
            if(PlayerPrefs.GetInt("Score") < CarController.countCars)
            {
                PlayerPrefs.SetInt("Score", CarController.countCars);
            }
            topScore.text = "Best score: " + PlayerPrefs.GetInt("Score");
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

    IEnumerator CreateHorn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f,9f));
            if (PlayerPrefs.GetString("music")!="No")
                Instantiate(horn,Vector3.zero,Quaternion.identity);
        }
    }

    void StopSound()
    {
        turnSignal.Stop();
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
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying)
                {
                    turnSignal.Play();
                    Invoke("StopSound",2f);
                }
                break;
            case 3:
            case 4:
                newObj.GetComponent<CarController>().leftTurn = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying)
                {
                    turnSignal.Play();
                    Invoke("StopSound",2f);
                }
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
