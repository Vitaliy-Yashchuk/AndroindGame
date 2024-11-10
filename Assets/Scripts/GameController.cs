using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isMainScene;
    public GameObject[] cars;
    public float timeToSpawnFrom = 3f;
    public float timeToSpawnTo = 7.5f;
    private void Start()
    {
        if (isMainScene)
        {
            timeToSpawnFrom = 4f;
            timeToSpawnTo = 6f;
        }
        StartCoroutine(BottomCars());
        StartCoroutine(LeftCars());
        StartCoroutine(RightCars());
        StartCoroutine(UpCars());
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
