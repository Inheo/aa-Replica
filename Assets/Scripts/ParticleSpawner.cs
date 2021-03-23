using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public ParticleSystem ClickParticle;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 positionParticle = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionParticle.z = 0;
            Instantiate(ClickParticle.gameObject, positionParticle, Quaternion.identity);
        }
    }
}
