using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinSpawner : MonoBehaviour
{
    public float TimeOutSpawn = 0.2f;

    public GameObject PinPrefab;
    public GameManager GameManager;
    public Collider2D RotatorCollider2D;

    private float timeSpawn;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.isEndGaming && timeSpawn == 0)
        {
            SpawnPin();
            timeSpawn = TimeOutSpawn;
        }
        else if (Input.GetMouseButtonDown(0) && GameManager.isEndGaming)
        {
            GameManager.StartGameAgain?.Invoke();
        }

        timeSpawn -= Time.deltaTime;
        timeSpawn = Mathf.Clamp(timeSpawn, 0, TimeOutSpawn);
    }

    private void SpawnPin()
    {
        Pin pin = Instantiate(PinPrefab, transform.position, Quaternion.identity).GetComponent<Pin>();
        GameManager.Pins.Add(pin);
        pin.GameManager = GameManager;
        pin.Target = SetTargetForPin(pin.GetComponent<BoxCollider2D>(), pin.GetComponent<CircleCollider2D>(), pin.transform);
    }

    private Vector3 SetTargetForPin(Collider2D spearCollider, Collider2D pinCollider, Transform pinTransform)
    {
        float targetY = RotatorCollider2D.transform.position.y - (RotatorCollider2D.bounds.size.y / 2 + spearCollider.bounds.size.y + pinCollider.bounds.size.y / 2) + 0.1f;
        return new Vector3(pinTransform.position.x, targetY, pinTransform.position.y);
    }
}
