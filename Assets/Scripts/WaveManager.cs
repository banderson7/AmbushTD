using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public Text countdownUI;
    public List<Wave> waves;
    private int _currentWave = 0;
    private int _levelMultiplier;
    public int timeBetweenWaves = 10;
    private float _waveCountdown;
    public GameObject skipButton;
    
    private Spawner _spawner;
    private int _spawnsThisWave;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        _spawnsThisWave = 0;
        _levelMultiplier = 0;
        _waveCountdown = timeBetweenWaves;
        StartCoroutine(BetweenWaveStart());
    }

    public void StopWaves()
    {
        StopAllCoroutines();
    }

    public void SkipToNextWave()
    {
        StopWaves();
        countdownUI.enabled = false;
        skipButton.SetActive(false);

        if (_currentWave == 9)
        {
            _spawner.SpawnUnit(waves[_currentWave].unit, _levelMultiplier);
        }
        else
        {
            StartWave(waves[_currentWave]);
            _currentWave++;
        }
    }
    
    private void StartWave(Wave wave)
    {
        if (_currentWave % 3 == 0)
        {
            _levelMultiplier++;
        }
        ToggleCountdownUI(false);
        StartCoroutine(SpawnWave(wave));
    }
    
    private IEnumerator SpawnWave(Wave wave)
    {
        _spawnsThisWave++;
        yield return new WaitForSeconds(wave.timeBetweenSpawns);
        _spawner.SpawnUnit(wave.unit, _levelMultiplier);
        
        if (_spawnsThisWave < wave.unitsThisWave)
        {
            StartCoroutine(SpawnWave(wave));
        }
        else
        {
            _spawnsThisWave = 0;
            StartCoroutine(BetweenWaveStart());
        }
    }

    private void ToggleCountdownUI(bool setTo)
    {
        skipButton.SetActive(setTo);
        countdownUI.enabled = setTo;
    }

    private IEnumerator BetweenWaveStart()
    {
        ToggleCountdownUI(true);
        _waveCountdown = timeBetweenWaves;
        while (_waveCountdown > 0)
        {
            _waveCountdown -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(_waveCountdown % 60);
            countdownUI.text = "Next wave in: " + seconds;
            yield return null;
        }
        ToggleCountdownUI(false);

        if (_currentWave == 9)
        {
            _spawner.SpawnUnit(waves[_currentWave].unit, _levelMultiplier);
            yield break;
        }
        
        StartWave(waves[_currentWave]);
        _currentWave++;
    }
}
