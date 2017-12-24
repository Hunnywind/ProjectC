using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]

public class QuestionCard : SCard {
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Init(int num, Direction d)
    {
        base.Init(num, d);
    }
    public override void AddCardType(CardType type)
    {
        base.AddCardType(type);
        switch (type)
        {
            case CardType.QUESTION_MARK:
                _cardNumberText.text = "?";
                break;
            default:
                break;
        }
    }
    private void OnMouseDown()
    {
        if (!StageMng.GetInstance._isStageStart) return;

        foreach (var item in _cardTypeList)
        {
            if (item == CardType.QUESTION_MARK)
            {
                ScoreMng.GetInstance.AddQuestion(1);
            }
        }
        CardMng.GetInstance.CreateAnswerCard(_cardNumber, _cardDirection, _cardTypeList);
    }
    public override void ReturnCard()
    {
        base.ReturnCard();
        CardMng.GetInstance.ReturnCard(this);
        gameObject.SetActive(false);
    }
}
