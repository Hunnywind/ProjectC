using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBox : MonoBehaviour {

    [SerializeField]
    private GameObject[] _leftBox;
    [SerializeField]
    private GameObject[] _rightBox;

    private int _leftCount = 0;
    private int _rightCount = 0;

    public void CardInput(GameObject card, Direction direction)
    {
        if (direction == Direction.LEFT)
        {
            if (_leftCount < 5)
                card.transform.SetParent(_leftBox[0].transform);
            else
                card.transform.SetParent(_leftBox[1].transform);
            _leftCount++;
        }
        else
        {
            if(_rightCount < 5)
                card.transform.SetParent(_rightBox[0].transform);
            else
                card.transform.SetParent(_rightBox[1].transform);
            _rightCount++;
        }
    }
    public void Clear()
    {
        for (int i = 0; i < _leftBox.Length; i++)
        {
            foreach (var item in _leftBox[i].GetComponentsInChildren<SCard>())
            {
                item.ReturnCard();
            }
        }
        for (int i = 0; i < _rightBox.Length; i++)
        {
            foreach (var item in _rightBox[i].GetComponentsInChildren<SCard>())
            {
                item.ReturnCard();
            }
        }
        _leftCount = 0;
        _rightCount = 0;
    }
    public void DeleteCard(Direction direction)
    {
        if (direction == Direction.LEFT)
            _leftCount--;
        else
            _rightCount--;
    }
    //public GameObject GetObject(Direction direction)
    //{
    //    if (direction == Direction.LEFT)
    //        return _leftBox;
    //    else
    //        return _rightBox;
    //}
}
