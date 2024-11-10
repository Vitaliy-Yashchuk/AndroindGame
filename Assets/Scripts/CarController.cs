using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody _carRb;
    public bool rightTurn, leftTurn,moveFromUp;
    private float _originRotationY, rotateMultRight = 6f, rotateMultLeft = 5f;

    private void Start()
    {
        _originRotationY = transform.eulerAngles.y;
        _carRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _carRb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("TurnBlock Right")&& rightTurn)
        {
            RotateCar(rotateMultRight);
        }
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
        {
            RotateCar(rotateMultLeft, -1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("TurnBlock Right")&& rightTurn)
        {
            _carRb.rotation = Quaternion.Euler(0,_originRotationY + 90f,0);
        }
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
        {
            _carRb.rotation = Quaternion.Euler(0,_originRotationY - 90f,0);
        }
    }

    private void RotateCar(float speedRotate, int dir=1)
    {
        if (dir == -1 && transform.localRotation.eulerAngles.y == _originRotationY - 90f)
        {
            return;
        }

        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f)
        {
            return;
        }
        float rotateSpeed = speed * speedRotate * dir;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, rotateSpeed, 0f)* Time.fixedDeltaTime);
        _carRb.MoveRotation(_carRb.rotation * deltaRotation);

    }
}
