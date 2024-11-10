using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject car;
    public float timeToSpawn = 3f;
    private void Start()
    {
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
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator LeftCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-89.4f, -0.001841247f, 3.5f),270f);
            yield return new WaitForSeconds(timeToSpawn);
        }
    } 
    IEnumerator RightCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(26.8f, -0.001841247f, 10.2f),90f);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator UpCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-9f, -0.001841247f, 71.7f),0f);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    private void SpawnCar(Vector3 pos, float rotationY, bool isMoveFromUp = false)
    {
        
        GameObject newObj = Instantiate(car, pos, Quaternion.Euler(0,rotationY,0)) as GameObject;
        int rand = Random.Range(1, 7);
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
