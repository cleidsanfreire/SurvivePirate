using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUiScript : MonoBehaviour
{
    private static readonly int SCORE_FACTOR = 10;    
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highestScoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        // scoreLabel.text = GameManager.Instance.GetScore().ToString();
        // highestScoreLabel.text = GameManager.Instance.GetHighestScore().ToString();
        // Mais Organizado
        scoreLabel.text = GetScoreString();
        highestScoreLabel.text = GetHighestString();
    }

    // Update is called once per frame
    void Update()
    {
        // scoreLabel.text = GameManager.Instance.GetScore().ToString();
        // highestScoreLabel.text = GameManager.Instance.GetHighestScore().ToString();
        // Mais Organizado
        scoreLabel.text = GetScoreString();
        highestScoreLabel.text = GetHighestString();
    }
    private string GetScoreString() {
        return (GameManager.Instance.GetScore() * SCORE_FACTOR).ToString();
    }
    private string GetHighestString() {
        return (GameManager.Instance.GetHighestScore() * SCORE_FACTOR).ToString();
    }
}
