using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public Text livesText;
    private int _currentLives;
    private WaveManager _waveManager;
    public CanvasGroup gameOverScreen;

    private void Awake()
    {
        _currentLives = 5;
        _waveManager = GetComponent<WaveManager>();
        UnitMovement.OnReachEnd += ReduceLives;
    }

    private void ReduceLives(bool isBoss)
    {
        _currentLives--;
        livesText.text = "Lives: " + _currentLives;

        if (_currentLives <= 0 || isBoss)
        {
            _waveManager.StopWaves();
            gameOverScreen.alpha = 1;
            gameOverScreen.interactable = true;
            gameOverScreen.blocksRaycasts = true;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnDisable()
    {
        UnitMovement.OnReachEnd -= ReduceLives;
    }
}
