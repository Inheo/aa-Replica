using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isEndGaming = false;
    public Animation cameraAnimation;

    public Text ScoreText;

    public Action EndGame;
    public Action StartGameAgain;

    public static int TimeForSpeed = 1;

    [HideInInspector]
    public List<Pin> Pins;

    private int score;

    private void Start()
    {
        TimeForSpeed = 1;
        EndGame += GameOver;
        StartGameAgain += StartAgain;
    }

    private void GameOver()
    {
        cameraAnimation.Play("GameOver");
        isEndGaming = true;
        TimeForSpeed = 0;
    }

    private void StartAgain()
    {
        cameraAnimation.Play("StartGameAgain");
        isEndGaming = false;
        HidePins();
        StartCoroutine(ResetScore());
        TimeForSpeed = 1;
    }

    private void HidePins()
    {
        Pin[] oldPins = new Pin[Pins.Count];
        for (int i = 0; i < Pins.Count; i++)
        {
            Pins[i].PinCollider.enabled = false;
            Pins[i].SpearCOllider.enabled = false;
            Pins[i].transform.SetParent(null);
            Pins[i].HidePin();
            oldPins[i] = Pins[i];
        }
        Pins.Clear();
        StartCoroutine(DestroyAllPins(oldPins));
    }
    private IEnumerator DestroyAllPins(Pin[] pins)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < pins.Length; i++)
        {
            Destroy(pins[i].gameObject);
        }
    }

    private IEnumerator ResetScore()
    {
        for (int i = score; i >= 0; i--)
        {
            ScoreText.text = i.ToString();
            yield return new WaitForSeconds(0.2f / score);
        }

        score = 0;
    }

    public void UpdateUI(int i)
    {
        score += i;
        ScoreText.text = score.ToString();
    }
}
