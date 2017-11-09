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
    private void OnMouseDown()
    {
        CardMng.GetInstance.CreateAnswerCard(_cardNumber, _cardDirection);
    }
    public override void ReturnCard()
    {
        CardMng.GetInstance.ReturnCard(this);
        gameObject.SetActive(false);
    }
}
