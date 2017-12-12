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
    private int _ruleCount = 0;

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

        GoalScoreSetting();
    }
    private void GoalScoreSetting()
    {
        if (RuleMng.GetInstance.IsAllRuleOpen) return;
        _goalScore = int.Parse(GameData.GetInstance.GetGameData(DataKind.RULESCORE, _ruleCount, "Score"));
    }

    private void Update()
    {
        //_scoreText.text = _showScore.ToString();
        _stageScoreText.text = "Obtain Score: " + _levelScore;

        if (!RuleMng.GetInstance.IsAllRuleOpen)
            _percentText.text = "New Rule " + ((int)(((float)_myScore / _goalScore) * 100)).ToString() + "%";
        else
            _percentText.text = "All Rule Open";
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
        int baseScore = 50;
        int stageNum = StageMng.GetInstance._stageNum;
        int stageRuleCount = RuleMng.GetInstance.PreStageRuleCount;
        float timeValue = 0;

        if (_time < 30.0f)
            timeValue = (30.0f - _time) * 40 + (60.0f - 30) * 20 + (120.0f - 60) * 10 + baseScore;
        else if (_time < 60.0f)
            timeValue = (60.0f - _time) * 20 + (120.0f - 60) * 10 + baseScore;
        else if (_time < 120.0f)
            timeValue = (120.0f - _time) * 10 + baseScore;
        else
            timeValue = baseScore;
        int score = (int)((timeValue * (stageNum + 1)) + (stageRuleCount * 100));
        AddScore(score);
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
                if(_levelScore > 100)
                {
                    SoundMng.GetInstance.Play(3);
                    _myScore += 100;
                    _levelScore -= 100;
                }
                if(_levelScore > 1000)
                {
                    _myScore += 1000;
                    _levelScore -= 1000;
                }
                yield return new WaitForFixedUpdate();
                if(_myScore >= _goalScore && !RuleMng.GetInstance.IsAllRuleOpen)
                {
                    _myScore = _goalScore;
                    _levelScore = 0;
                    yield return new WaitForSeconds(1f);
                    RuleMng.GetInstance.NewRule();
                    _myScore = 0;
                    _ruleCount++;
                    GoalScoreSetting();
                    Init();
                    yield break;
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
