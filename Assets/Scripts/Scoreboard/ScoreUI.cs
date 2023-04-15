using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreUI : MonoBehaviour
{
    public RowUi rowUi;

    void Start()
    {
        // scoreManager.AddScore(new Score("hehe", 100));
        // scoreManager.AddScore(new Score("hhohoho", 1000));
        //scoreManager.AddScore(new Score("lalalla", 9000));

        var scores = ScoreBoardScoreManager.Instance.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i ++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<RowUi>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.score.text = scores[i].score.ToString();
        }
    }
}
