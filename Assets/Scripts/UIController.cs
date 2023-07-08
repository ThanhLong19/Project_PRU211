using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] public Slider healthBar;
    [SerializeField] private float slidingTime;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnHealthChanged(float health)
    {
        // healthBar.value = (health - healthBar.minValue) / (healthBar.maxValue - healthBar.minValue) *
        // healthBar.maxValue;
        healthBar.DOValue(
            health,
            slidingTime).SetEase(Ease.InOutSine);
    }

    private void OnScoreChanged(int score)
    {
        scoreText.DOCounter(int.Parse(scoreText.text), score, slidingTime);
    }

    private void OnEnable()
    {
        if (player != null)
        {
            player.GetComponent<Health>().OnHealthChanged += OnHealthChanged;
        }

        GameController.OnScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.GetComponent<Health>().OnHealthChanged -= OnHealthChanged;
        }

        GameController.OnScoreChanged -= OnScoreChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}