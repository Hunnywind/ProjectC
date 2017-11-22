using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]

public class QuestionCard : SCard {
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
        //CardMng.GetInstance.CreateMovingCard(_cardNumber, _cardDirection);
        CardMng.GetInstance.CreateAnswerCard(_cardNumber, _cardDirection, _cardTypeList);
    }
    public override void ReturnCard()
    {
        base.ReturnCard();
        CardMng.GetInstance.ReturnCard(this);
        gameObject.SetActive(false);
    }
}
