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
    private float _scoreCalTime = 3f;
    [SerializeField]
    private int _cardRemoveScore = 100;

    [SerializeField]
    private Text[] _scoreTexts;

    private int _goalScore = 0;
    private int _myScore = 0;
    private int _showScore = 0;
    private int _preStageScore = 0;
    private int _levelScore = 0;
    private int _ruleCount = 0;

    private int _finalObtainScore = 0;
    private int _finalScore = 0;
    private int _finalQuestionCount = 0;
    private int _finalRemoveCardCount = 0;

    private int _difficulty;
    private float _time;

    private float _revision;

    private int _obtainedScores = 0;
    private int _highScore = 0;

    private void Start()
    {
        Init();
    }
    void Init()
    {
        LoadGame();
        _scoreText.gameObject.SetActive(true);
        foreach (var item in _scoreTexts)
        {
            item.gameObject.SetActive(false);
        }
        _percentText.gameObject.SetActive(true);

        _finalQuestionCount = 0;
        _finalScore = 0;
        _finalRemoveCardCount = 0;
        _levelScore = 0;
        _preStageScore = 0;
        _scoreText.text = "Score " + _levelScore;

        GoalScoreSetting();
    }
    private void LoadGame()
    {
        _obtainedScores = GameMng.GetInstance.LoadGame("ObtainedScore");
        _highScore = GameMng.GetInstance.LoadGame("HighScore");
        _myScore = GameMng.GetInstance.LoadGame("PreRuleScore");
        _ruleCount = RuleMng.GetInstance.RuleCount;
    }
    private void GoalScoreSetting()
    {
        if (RuleMng.GetInstance.IsAllRuleOpen) return;
        _goalScore = int.Parse(GameData.GetInstance.GetGameData(DataKind.RULESCORE, _ruleCount, "Score"));
    }

    private void Update()
    {
        _scoreTexts[0].text = "Obtain Score	: " + _finalObtainScore;
        _scoreTexts[1].text = "? Score			: " + "10000 x " + _finalQuestionCount;
        _scoreTexts[2].text = "Remove Card	: -100 x " + _finalRemoveCardCount;
        _scoreTexts[3].text = "Final Score		: " + _finalScore;

        if (!RuleMng.GetInstance.IsAllRuleOpen)
            _percentText.text = "New Rule " + ((int)(((float)_myScore / _goalScore) * 100)).ToString() + "%";
        else
            _percentText.text = "All Rule Open";
    }
    public void SetActivePreRuleScore(bool value)
    {
        _percentText.gameObject.SetActive(value);
    }
    public void Test()
    {
        _percentText.gameObject.SetActive(true);
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
        _scoreText.text = "Score " + _levelScore;
    }
    public void SubtractCard()
    {
        _finalRemoveCardCount++;
    }
    public void AddScore(int score)
    {
        _preStageScore += score;
    }
    public void AddQuestion(int count)
    {
        _finalQuestionCount += count;
    }
    IEnumerator ScoreUpdate()
    {
        _finalObtainScore = _levelScore;
        _finalScore = _finalObtainScore + _finalQuestionCount * 10000 - (_finalRemoveCardCount * 100);
        _levelScore += _finalQuestionCount * 10000 - (_finalRemoveCardCount * 100);
        if (GPGSMng.GetInstance != null)
            GPGSMng.GetInstance.ReportHighScore(_levelScore);

        if (GameMng.GetInstance.LoadGame("HighScore") < _levelScore)
        {
            GameMng.GetInstance.SaveGame("HighScore", _levelScore);
            _highScore = _levelScore;
        }
        GameMng.GetInstance.SaveGame("ObtainedScore", _levelScore + GameMng.GetInstance.LoadGame("ObtainedScore"));
        _obtainedScores += _levelScore;
        foreach (var item in _scoreTexts)
        {
            SoundMng.GetInstance.Play(4);
            item.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        if(RuleMng.GetInstance.IsAllRuleOpen)
        {
            yield return new WaitForSeconds(3f);
            StageMng.GetInstance.LobbySetting();
            Init();
            yield break;
        }
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
                    GameMng.GetInstance.SaveGame("PreRuleScore", _myScore);
                    _ruleCount++;
                    GoalScoreSetting();
                    Init();
                    yield break;
                }
                //yield return new WaitForSeconds(_revision);
            }
            else
            {
                GameMng.GetInstance.SaveGame("PreRuleScore", _myScore);
                // score cal end
                yield return new WaitForSeconds(2f);
                StageMng.GetInstance.LobbySetting();
                Init();
                yield break;
            }
        }
    }
}
