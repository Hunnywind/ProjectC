﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMaker {

    private int _cardKindCount;
    private int _cardCount;
    private bool isUnbalance;

    private List<int> _answerLeftCount = new List<int>();
    private List<int> _answerRightCount = new List<int>();
    private List<int> _numberLeft = new List<int>();
    private List<int> _numberRight = new List<int>();

    private int _remainCardCount;

    public void Init(int cardKindCount, int cardCount)
    {
        _cardKindCount = cardKindCount;
        _cardCount = cardCount;
        _remainCardCount = _cardCount;
       if(cardKindCount % 2 == 0)
        {
            isUnbalance = false;
        }
       else
        {
            isUnbalance = true;
        }

        _answerLeftCount.Add(0);
        _answerLeftCount.Add(0);
        _answerRightCount.Add(0);
        _answerRightCount.Add(0);
        _numberLeft.Add(0);
        _numberLeft.Add(0);
        _numberRight.Add(0);
        _numberRight.Add(0);
    }
    public void Setting()
    {
        UnbalanceSetting();
        AnswerDecision();
        CardNumberDecision();
        while(!Check())
        {
            CardNumberDecision();
        }
    }
    public List<int> GetAnswerCountList(Direction direction)
    {
        if (direction == Direction.LEFT)
            return _answerLeftCount;
        else
            return _answerRightCount;
    }
    public List<int> GetCardNumberList(Direction direction)
    {
        if (direction == Direction.LEFT)
            return _numberLeft;
        else
            return _numberRight;
    }

    private void UnbalanceSetting()
    {
        if (isUnbalance)
        {
            if (Random.Range(0, 2) == 0)
            {
                _answerLeftCount.Add(0);
                _numberLeft.Add(0);
            }
            else
            {
                _answerRightCount.Add(0);
                _numberRight.Add(0);
            }
        }
        else
        {
            _answerLeftCount.Add(0);
            _numberLeft.Add(0);
            _answerRightCount.Add(0);
            _numberRight.Add(0);
        }
    }
    private void AnswerDecision()
    {
        for (int i = 0; i < _answerLeftCount.Count; i++)
        {
            _answerLeftCount[i] = Random.Range(0, _remainCardCount);
            _remainCardCount -= _answerLeftCount[i];
        }
        for (int i = 0; i < _answerRightCount.Count; i++)
        {
            _answerRightCount[i] = Random.Range(0, _remainCardCount);
            if (i == _answerRightCount.Count - 1)
                _answerRightCount[i] = _remainCardCount;
            _remainCardCount -= _answerRightCount[i];
        }
    }
    private void CardNumberDecision()
    {
        for (int i = 0; i < _numberLeft.Count; i++)
        {
            _numberLeft[i] = 0;
        }
        for (int i = 0; i < _numberRight.Count; i++)
        {
            _numberRight[i] = 0;
        }

        for (int i = 0; i < _numberLeft.Count; i++)
        {
            int randomNum = Random.Range(1, 11);
            bool isDuplication = true;
            while(isDuplication)
            {
                randomNum = Random.Range(1, 11);
                isDuplication = false;
                foreach (var item in _numberLeft)
                {
                    if (item == randomNum)
                        isDuplication = true;
                }
                foreach (var item in _numberRight)
                {
                    if (item == randomNum)
                        isDuplication = true;
                }
            }
            _numberLeft[i] = randomNum;
        }
        for (int i = 0; i < _numberRight.Count; i++)
        {
            int randomNum = Random.Range(1, 11);
            bool isDuplication = true;
            while (isDuplication)
            {
                randomNum = Random.Range(1, 11);
                isDuplication = false;
                foreach (var item in _numberLeft)
                {
                    if (item == randomNum)
                        isDuplication = true;
                }
                foreach (var item in _numberRight)
                {
                    if (item == randomNum)
                        isDuplication = true;
                }
            }
            _numberRight[i] = randomNum;
        }
    }
    private bool Check()
    {
        int leftResult = 0, rightResult = 0;

        for (int i = 0; i < _answerLeftCount.Count; i++)
        {
            leftResult += _answerLeftCount[i] * _numberLeft[i];
        }
        for (int i = 0; i < _answerRightCount.Count; i++)
        {
            rightResult += _answerRightCount[i] * _numberRight[i];
        }

        if (leftResult == rightResult)
            return true;
        else
            return false;
    }
}