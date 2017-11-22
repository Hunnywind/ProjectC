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
    private int _cardRemoveScore = 100;

    private int _goalScore = 0;
    private int _myScore = 0;
    private int _showScore = 0;
    private int _preStageScore = 0;
    private int _levelScore = 0;

    private int _difficulty;
    private float _time;

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

        _levelScore = 0;
        _preStageScore = 0;

        TestInit();
    }

    private void TestInit()
    {
        _goalScore = 10000;
    }
    private void Update()
    {
        //_scoreText.text = _showScore.ToString();
        _stageScoreText.text = "Obtain Score: " + _levelScore;
        _percentText.text = "New Rule " + ((int)(((float)_myScore / _goalScore) * 100)).ToString() + "%";
    }

    public void Test()
    {
        _percentText.gameObject.SetActive(true);
        _stageScoreText.gameObject.SetActive(true);
        CoroutineManager.instance.StartCoroutine(ScoreUpdate());
        _revision = _scoreCalTime / _levelScore;
    }
    public void SetDifficult(int d)
    {
        _difficulty = d;
    }
    public void SetTime(float t)
    {
        _time = t;
    }
    public void StageClear()
    {
        float timeValue = 0;
        if (_time < 10.0f)
            timeValue = 2;
        else if (_time < 30.0f)
            timeValue = 1;
        else if (_time < 60.0f)
            timeValue = 0.5f;
        else
            timeValue = 0.1f;

        var value = timeValue * _difficulty * 10;
        AddScore((int)value);
        _preStageScore = Mathf.Max(_preStageScore, 0);
        Debug.Log(_preStageScore);
        _levelScore += _preStageScore;
        _preStageScore = 0;
    }
    public void SubtractCard()
    {
        _preStageScore -= _cardRemoveScore;
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
            if(_levelScore > 0)
            {
                _myScore++;
                _levelScore--;
                if(_levelScore > 10)
                {
                    _myScore += 10;
                    _levelScore -= 10;
                }
                yield return new WaitForFixedUpdate();
                if(_myScore >= _goalScore)
                {
                    _myScore = _goalScore;
                    _levelScore = 0;
                    yield return new WaitForSeconds(1f);
                    RuleMng.GetInstance.NewRule();
                }
                //yield return new WaitForSeconds(_revision);
            }
            else
            {
                // score cal end
                yield return new WaitForSeconds(2f);
                StageMng.GetInstance.LobbySetting();
                Init();
                yield break;
            }
        }
    }
}
