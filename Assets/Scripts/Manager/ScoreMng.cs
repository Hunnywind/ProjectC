using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreMng : Singleton<ScoreMng> {
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _percentText;
    [SerializeField]
    private Text _stageScoreText;

    [SerializeField]
    private float _scoreCalTime = 3f;
    [SerializeField]
    private int _cardRemoveScore = 10;

    private int _goalScore = 0;
    private int _myScore = 0;
    private int _showScore = 0;
    private int _preStageScore = 0;

    private int _difficulty;

    private float _revision;

    private void Start()
    {
        Init();
    }
    void Init()
    {
        //_scoreText.gameObject.SetActive(true);
        _stageScoreText.gameObject.SetActive(false);
        _percentText.gameObject.SetActive(true);

        TestInit();
    }

    private void TestInit()
    {
        _goalScore = 500;
    }
    private void Update()
    {
        //_scoreText.text = _showScore.ToString();
        _stageScoreText.text = "Obtain Score: " + _preStageScore;
        _percentText.text = "New Rule " + ((int)(((float)_myScore / _goalScore) * 100)).ToString() + "%";
    }

    public void Test()
    {
        _percentText.gameObject.SetActive(true);
        _stageScoreText.gameObject.SetActive(true);
        CoroutineManager.instance.StartCoroutine(ScoreUpdate());
        _revision = _scoreCalTime / _preStageScore;
    }
    public void AddScore(int score)
    {
        _preStageScore += score;
    }
    IEnumerator ScoreUpdate()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if(_preStageScore > 0)
            {
                _myScore++;
                _preStageScore--;
                yield return new WaitForSeconds(_revision);
            }
            else
            {
                // score cal end
                yield return new WaitForSeconds(2f);
                StageMng.GetInstance.LobbySetting();
                Init();
                break;
            }
        }
        yield return null;
    }
}
