using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGameOver { get; private set; }
    [HideInInspector]public float gameWidth = 88f;
    // Constants
    private static readonly string KEY_HIGHEST_SCORE = "HighestScore";

    [Header("Audio")] // Colocar um tipo de nome em uma "lista"
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource gameOverSfx;
    public AudioSource jumpSfx;
    public AudioSource splashSfx;

    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private int highestScore;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        // Score
        score = 0;
        highestScore = PlayerPrefs.GetInt(KEY_HIGHEST_SCORE); ;
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Increment score
            //score += Time.deltaTime * 10;
            score += Time.deltaTime;

            //Update highest score
            if (GetScore() > GetHighestScore())
            {
                highestScore = GetScore();
            }
        }
    }

    public int GetScore()
    {
        return (int)(Mathf.Floor(score));
    }

    public int GetHighestScore()
    {
        return highestScore;
    }

    public void EndGame()
    {
        if (isGameOver) return;

        // Set flag
        isGameOver = true;

        // Stop music
        musicPlayer.Stop();
        // Play SFX
        gameOverSfx.Play();

        // Save Highest score
        PlayerPrefs.SetInt(KEY_HIGHEST_SCORE, GetHighestScore());

        StartCoroutine(ReloadGame(8));
    }

    private IEnumerator ReloadGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
