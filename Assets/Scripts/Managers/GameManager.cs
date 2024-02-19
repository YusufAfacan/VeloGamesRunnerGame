using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public RunnerMovement RunnerMovement => RunnerMovement.Instance;

    [SerializeField] private int _playerHealth;
    [SerializeField] private float _score;
    [SerializeField] private float _scoreIncreaseAmountPerSec;
    [SerializeField] private float _coinScore;

    [SerializeField] private float highscore;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        highscore = PlayerPrefs.GetFloat("HighScore");
        Time.timeScale = 1f;
    }

    public void HarmPlayer()
    {
        RunnerMovement.BeInvulnerable();
        _playerHealth--;
        healthText.text = _playerHealth.ToString();

        if (_playerHealth <= 0)
        {
            GameOver();
        }
    }

    private void Update()
    {
        _score += _scoreIncreaseAmountPerSec * Time.deltaTime;
        scoreText.text = _score.ToString("F0");

        if (_score > highscore)
        {
            highscore = _score;
            scoreText.color = Color.green;
            PlayerPrefs.SetFloat("HighScore", highscore);
        }
    }

    public void CollectCoin()
    {
        _score += _coinScore;
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
