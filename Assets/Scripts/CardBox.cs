using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBox : MonoBehaviour {

    [SerializeField]
    private GameObject _leftBox;
    [SerializeField]
    private GameObject _rightBox;

    public void CardInput(GameObject card, Direction direction)
    {
        if (direction == Direction.LEFT)
            card.transform.SetParent(_leftBox.transform);
        else
            card.transform.SetParent(_rightBox.transform);
    }
    public void Clear()
    {
        foreach (var item in _leftBox.GetComponentsInChildren<SCard>())
        {
            item.ReturnCard();
        }
        foreach (var item in _rightBox.GetComponentsInChildren<SCard>())
        {
            item.ReturnCard();
        }
    }
}
