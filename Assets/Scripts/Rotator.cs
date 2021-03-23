using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float Speed = 100;

    private void Update()
    {
        transform.Rotate(Vector3.forward * Speed * Time.deltaTime * GameManager.TimeForSpeed);
    }
}
