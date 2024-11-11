using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour {

     public bool rightTurn, leftTurn, moveFromUp;
    public float speed = 15f;
    private Rigidbody carRb;
    private float originRotationY, rotateMultRight = 6f, rotateMultLeft = 4.5f;
    private Camera mainCam;
    public LayerMask carsLayer;
    private bool isMovingFast;
    [NonSerialized] public bool carPassed;
    public GameObject turnLeftSignal, turnRightSignal;

    private void Start() {
        mainCam = Camera.main;
        originRotationY = transform.eulerAngles.y;
        carRb = GetComponent<Rigidbody>();

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
        carRb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Update() {
#if UNITY_EDITOR
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
#else
        if (Input.touchCount == 0)
            return;
        
        Ray ray = mainCam.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, carsLayer)) {
            string carName = hit.transform.gameObject.name;

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !isMovingFast && gameObject.name == carName) {
#else
            if (Input.GetTouch(0).phase == TouchPhase.Began && !isMovingFast && gameObject.name == carName) {
#endif
            
                speed *= 2f;
                isMovingFast = true;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
            RotateCar(rotateMultRight);
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
            RotateCar(rotateMultLeft, -1);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Car") && other.GetComponent<CarController>().carPassed)
            other.GetComponent<CarController>().speed = speed + 5f;
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.CompareTag("TriggerPass")) {
            carPassed = true;
            Collider[] colliders = GetComponents<BoxCollider>();
            foreach (Collider col in colliders)
                col.enabled = true;
        }
        
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
            carRb.rotation = Quaternion.Euler(0, originRotationY + 90f, 0);
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
            carRb.rotation = Quaternion.Euler(0, originRotationY - 90f, 0);
        else if(other.transform.CompareTag("Delete Trigger"))
            Destroy(gameObject);
    }

    private void RotateCar(float speedRotate, int dir = 1) {
        if (dir == -1 && transform.localRotation.eulerAngles.y < originRotationY - 90f)
            return;
        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f)
            return;
        
        float rotateSpeed = speed * speedRotate * dir;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
        carRb.MoveRotation(carRb.rotation * deltaRotation);
    }
    
}
