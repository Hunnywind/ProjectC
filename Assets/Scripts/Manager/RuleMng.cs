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
    QUESTION_MARK = 0,
    CONST_LEFT_RIGHT = 1,
}
public class RuleMng : Singleton<RuleMng> {
    private List<bool> _ruleList = new List<bool>();

    void Start()
    {
        _ruleList.Add(false);
        _ruleList.Add(false);
    }

    public void NewRule()
    {
        for (int i = 0; i < _ruleList.Count; i++)
        {
            if(!_ruleList[i])
            {
                _ruleList[i] = true;
                Action<bool> action = a =>
                {
                    StageMng.GetInstance.LobbySetting();
                };

                var data = GameData.GetInstance.GetGameData(DataKind.RULETEXT, i, "Content");
                PopupMng.GetInstance.PopupMessage("New Rule", data, BUTTON_KIND.OK, null, null, action);
                break;
            }
        }
    }
    public bool isRuleBeing(int num)
    {
        if (num >= _ruleList.Count) return false;

        return _ruleList[num];
    }
}
