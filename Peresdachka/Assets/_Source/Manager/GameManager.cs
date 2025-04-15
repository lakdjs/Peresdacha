using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float Score { get; private set; }
    public float PlayTime { get; private set; }

    private float _scorePerSecond = 10f;

    private enum SaveType { PlayerPrefs, JSON }
    [SerializeField] private SaveType saveType;

    private ISaver _saver;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _saver = saveType == SaveType.PlayerPrefs ? new PlayerPrefsSaver() : new JSONsaver();
    }

    private void Update()
    {
        PlayTime += Time.deltaTime;
        UImanager.Instance.UpdateTime(PlayTime);
    }

    public void AddScore(float value)
    {
        Score += value;
        UImanager.Instance.UpdateScore(Score);
    }

    public void SaveGame()
    {
        _saver.Save(new GameData { score = Score, playTime = PlayTime });
    }

    public void LoadGame()
    {
        GameData data = _saver.Load();
        Score = data.score;
        PlayTime = data.playTime;
    }

    public void SetScorePerSecond(float score)
    {
        _scorePerSecond = score;
    }

    public float GetScorePerSecond()
    {
        return _scorePerSecond;
    }

    public void ResetGameData()
    {
        _saver.ClearSave();

        Score = 0;
        PlayTime = 0;

        UImanager.Instance.UpdateScore(Score);
        UImanager.Instance.UpdateTime(PlayTime);
    }
}
