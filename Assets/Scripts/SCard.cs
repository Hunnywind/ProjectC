using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    LEFT,
    RIGHT,
}

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]

public class SCard : MonoBehaviour {
    protected Image _cardImage;
    protected Text _cardNumberText;
    protected int _cardNumber;
    protected Direction _cardDirection;

    public int GetCardNumber() { return _cardNumber; }


    public virtual void Init(int num, Direction d)
    {
        _cardNumber = num;
        _cardDirection = d;
        _cardNumberText.text = _cardNumber.ToString();
    }
    public virtual void Start()
    {
        _cardImage = gameObject.GetComponent<Image>();
        _cardNumberText = gameObject.GetComponentInChildren<Text>();
    }
    public virtual void ReturnCard()
    {

    }
}
