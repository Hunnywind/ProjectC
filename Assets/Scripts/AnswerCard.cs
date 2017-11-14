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
    private void OnMouseDown()
    {
        if(StageMng.GetInstance._isStageStart)
            ReturnCard();
    }
    public override void ReturnCard()
    {
        gameObject.transform.parent.parent.GetComponent<Scales>().AddWeight(-_cardNumber, _cardDirection);
        CardMng.GetInstance.ReturnCard(this);
        gameObject.SetActive(false);
    }
}
