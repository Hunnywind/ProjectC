using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 룰을 정의합니다
/// 카드 범위, 특수 카드 여부 등등
/// </summary>
public enum RuleType
{
    QUESTION_MARK = 1,
    CONST_LEFT_RIGHT = 2,
    BIG_NUMBER = 4,
}
public class RuleMng : Singleton<RuleMng> {
    private List<bool> _ruleList = new List<bool>();
    private Dictionary<RuleType, bool> _currentStageRuleList = new Dictionary<RuleType, bool>();

    public int RuleCount { private set; get; }
    public int PreStageRuleCount { private set; get; }

    public bool IsAllRuleOpen
    {
        private set; get;
    }

    private void Start()
    {
        IsAllRuleOpen = false;
        _ruleList.Add(false);
        _ruleList.Add(false);
        _ruleList.Add(false);

        _currentStageRuleList.Add(RuleType.QUESTION_MARK, false);
        _currentStageRuleList.Add(RuleType.CONST_LEFT_RIGHT, false);
        _currentStageRuleList.Add(RuleType.BIG_NUMBER, false);

        LoadGame();
    }
    private void LoadGame()
    {
        RuleCount = GameMng.GetInstance.LoadGame("PreRuleCount");
        for (int i = 0; i < RuleCount; i++)
        {
            NewRule(false);
        }
    }
    /// <summary>
    /// 스테이지 시작시마다 호출 해야하는 함수
    /// 현재 오픈한 룰 수를 정합니다
    /// </summary>
    public void Setting()
    {
        int openRules = 0;
        foreach (var item in _ruleList)
        {
            if (item) openRules++;
        }
        RuleCount = openRules;
    }
    public void NewRule(bool isShowPopup = true)
    {
        for (int i = 0; i < _ruleList.Count; i++)
        {
            if(!_ruleList[i])
            {
                _ruleList[i] = true;
                RuleCheck();
                if (isShowPopup)
                {
                    RuleCount++;
                    GameMng.GetInstance.SaveGame("PreRuleCount", i + 1);
                    Action<bool> action = a =>
                    {
                        StageMng.GetInstance.LobbySetting();
                    };
                    var data = GameData.GetInstance.GetGameData(DataKind.RULETEXT, i, "Content");
                    PopupMng.GetInstance.PopupMessage("New Rule", data, BUTTON_KIND.OK, null, null, action);
                }
                break;
            }
        }
    }
    public void SetCurrentRule(int stageNum)
    {
        PreStageRuleCount = 0;
        int id = RuleCount * 7 + stageNum;
        int rules = int.Parse(GameData.GetInstance.GetGameData(DataKind.NORMALSTAGE, id, "Rules"));

        _currentStageRuleList[RuleType.QUESTION_MARK] = false;
        _currentStageRuleList[RuleType.CONST_LEFT_RIGHT] = false;
        _currentStageRuleList[RuleType.BIG_NUMBER] = false;

        if (rules >= 4)
        {
            rules -= 4;
            _currentStageRuleList[RuleType.BIG_NUMBER] = true;
            PreStageRuleCount++;
        }
        if(rules >= 2)
        {
            rules -= 2;
            _currentStageRuleList[RuleType.CONST_LEFT_RIGHT] = true;
            PreStageRuleCount++;
        }
        if(rules >= 1)
        {
            rules -= 1;
            _currentStageRuleList[RuleType.QUESTION_MARK] = true;
            PreStageRuleCount++;
        }
    }
    public bool isRuleBeing(RuleType type)
    {
        return _currentStageRuleList[type];
    }
    void RuleCheck()
    {
        bool isAll = false;
        foreach (var item in _ruleList)
        {
            if (!item) isAll = true;
        }
        if (isAll) IsAllRuleOpen = false;
        else IsAllRuleOpen = true;
    }
}
