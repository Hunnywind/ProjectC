using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardMng : Singleton<CardMng> {
    /*
     * 왼쪽, 오른쪽 카드 구분 필요
     * QuestionCard 터치시 AnswerCard 활성
     * AnswerCard 터치시 AnswerCard 비활성
     */
    [SerializeField]
    private ObjectPool _qCardObjectPool;
    [SerializeField]
    private ObjectPool _aCardObjectPool;

    private void Start()
    {
        Init();
    }
    void Init()
    {
        _qCardObjectPool.Init();
        _aCardObjectPool.Init();
    }
}
