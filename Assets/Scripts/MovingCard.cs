using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]
public class MovingCard : SCard {
    [SerializeField]
    private float _speed = 100f;
    [SerializeField]
    private RectTransform _rTransform;

    private Vector2 _destination;

    public void Init(int num, Direction direction, Vector2 start, GameObject des)
    {
        base.Init(num, direction);
        _destination = des.GetComponent<RectTransform>().anchoredPosition;
        _rTransform.anchoredPosition = start;
    }
    private void FixedUpdate()
    {
        //var target = Vector2.MoveTowards(_rTransform.anchoredPosition, _destination, _speed * Time.deltaTime);
        //_rTransform.anchoredPosition = target;
        //if ((_rTransform.anchoredPosition - _destination).sqrMagnitude < 100.0f)
        //{
        //    CardMng.GetInstance.CreateAnswerCard(_cardNumber, _cardDirection);
        //    gameObject.SetActive(false);
        //}
    }
}
