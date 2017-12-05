using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]

public class AnswerCard : SCard {
    public override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
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
        if (StageMng.GetInstance._isStageStart)
        {
            ScoreMng.GetInstance.SubtractCard();
            SoundMng.GetInstance.Play(0);
            ReturnCard();
        }
    }
    public override void ReturnCard()
    {
        base.ReturnCard();
        gameObject.transform.parent.parent.GetComponent<Scales>().AddWeight(-_cardNumber, _cardDirection);
        CardMng.GetInstance.ReturnCard(this);
        gameObject.SetActive(false);
    }
}
