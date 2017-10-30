using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]
public class SCard : MonoBehaviour {
    protected Image _cardImage;

    public virtual void Start()
    {
        _cardImage = gameObject.GetComponent<Image>();
    }
}
