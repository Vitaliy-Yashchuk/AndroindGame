using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteHorn : MonoBehaviour
{
    public float TimeToDelete=2f;
    void Start()
    {
        Destroy(gameObject, TimeToDelete);
    }

}
