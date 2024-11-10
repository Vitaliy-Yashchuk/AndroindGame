using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject car;
    private void Start()
    {
        StartCoroutine(BottomCars());
    }

    IEnumerator BottomCars()
    {
        while (true)
        {
           GameObject newObj = Instantiate(car, new Vector3(-1.59f, -0.001841247f, -24.97f), Quaternion.Euler(0,180,0)) as GameObject;
            int rand = Random.Range(1, 4);
            switch (rand)
            {
                case 1:
                    newObj.GetComponent<CarController>().rightTurn = true;
                    break;
                case 2:
                    newObj.GetComponent<CarController>().leftTurn = true;
                    break;
                case 3:
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
