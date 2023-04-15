using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreBoardScoreManager : MonoBehaviour
{
    public static ScoreBoardScoreManager Instance { get; private set; }
    private ScoreData sd;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            var json = PlayerPrefs.GetString("scores", "{}");
            sd = JsonUtility.FromJson<ScoreData>(json);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public IEnumerable<Score> GetHighScores()
    {
        return sd.scores.OrderBy(x => x.score);
    }

    public void AddScore(Score score)
    {
        sd.scores.Add(score);
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString("scores", json);
    }
}
