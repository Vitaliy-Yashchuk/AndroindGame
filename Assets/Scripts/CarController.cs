using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour {

     public bool rightTurn, leftTurn, moveFromUp;
    public float speed = 12f, force = 50f;
    private Rigidbody _carRb;
    private float _originRotationY, _rotateMultRight = 6f, _rotateMultLeft = 4.5f;
    private Camera _mainCam;
    public LayerMask carsLayer;
    private bool _isMovingFast, _carCrashed;
    [NonSerialized] public bool carPassed;
    [NonSerialized] public static bool isLose;
    public GameObject turnLeftSignal, turnRightSignal;
    public static int countCars=0;

    private void Start() {
        _mainCam = Camera.main;
        _originRotationY = transform.eulerAngles.y;
        _carRb = GetComponent<Rigidbody>();

        if (rightTurn)
            StartCoroutine(TurnSignals(turnRightSignal));
        else if (leftTurn)
            StartCoroutine(TurnSignals(turnLeftSignal));
    }

    IEnumerator TurnSignals(GameObject turnSignal) {
        while (!carPassed) {
            turnSignal.SetActive(!turnSignal.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate() {
        _carRb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Update() {
#if UNITY_EDITOR
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
#else
    if (Input.touchCount == 0)
        return;
    
    Ray ray = mainCam.ScreenPointToRay(Input.GetTouch(0).position);
#endif
    
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, carsLayer)) {
            string carName = hit.transform.gameObject.name;

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !_isMovingFast && gameObject.name == carName) {
#else
        if (Input.GetTouch(0).phase == TouchPhase.Began && !isMovingFast && gameObject.name == carName) {
#endif
        
                
                speed *= 2f;
                _isMovingFast = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car")&& !_carCrashed)
        {
            _carCrashed = true;
            isLose = true;
            speed = 0f;
            other.gameObject.GetComponent<CarController>().speed = 0f;
            if (_isMovingFast)
            {
                force *= 1.5f;    
            }
            _carRb.AddRelativeForce(Vector3.forward * force);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(_carCrashed)
            return;
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
            RotateCar(_rotateMultRight);
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
            RotateCar(_rotateMultLeft, -1);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Car") && other.GetComponent<CarController>().carPassed)
            other.GetComponent<CarController>().speed = speed + 5f;
    }

    private void OnTriggerExit(Collider other) {
        if(_carCrashed)
            return;
        if (other.transform.CompareTag("TriggerPass")) {
            carPassed = true;
            Collider[] colliders = GetComponents<BoxCollider>();
            foreach (Collider col in colliders)
                col.enabled = true;
            countCars++;
        }
        
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
            _carRb.rotation = Quaternion.Euler(0, _originRotationY + 90f, 0);
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
            _carRb.rotation = Quaternion.Euler(0, _originRotationY - 90f, 0);
        else if (other.transform.CompareTag("Delete Trigger"))
            Destroy(gameObject);
        
    }

    private void RotateCar(float speedRotate, int dir = 1) {
        if (_carCrashed)
        {
            return;
        }
        if (dir == -1 && transform.localRotation.eulerAngles.y < _originRotationY - 90f)
            return;
        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f)
            return;
        
        float rotateSpeed = speed * speedRotate * dir;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
        _carRb.MoveRotation(_carRb.rotation * deltaRotation);
    }
    
}
