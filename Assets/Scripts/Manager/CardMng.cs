using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    QUESTION_MARK = 0,
}

public class CardMng : Singleton<CardMng> {

    [SerializeField]
    private ObjectPool _qCardObjectPool;
    [SerializeField]
    private ObjectPool _aCardObjectPool;
    //[SerializeField]
    //private ObjectPool _mCardObjectPool;
    [SerializeField]
    private CardBox _answerBox;
    [SerializeField]
    private CardBox _questionBox;
    [SerializeField]
    private GameObject[] _mustObjects;
    [SerializeField]
    private Text _mustUseCardCountText;
    [SerializeField]
    private Text _mustUseLeftCardCountText;
    [SerializeField]
    private Text _mustUseRightCardCountText;


    private List<SCard> _questionCardList = new List<SCard>();

    private int _mustUseCardCount;
    private int _mustUseLeftCardCount;
    private int _mustUseRightCardCount;

    private bool _constLeftRight;

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        _mustUseCardCountText.text = "x" + _mustUseCardCount.ToString();
        _mustUseLeftCardCountText.text = "x" + _mustUseLeftCardCount.ToString();
        _mustUseRightCardCountText.text = "x" + _mustUseRightCardCount.ToString();
    }
    void Init()
    {
        _qCardObjectPool.Init();
        _aCardObjectPool.Init();
        //_mCardObjectPool.Init();
    }
    public void CardSetting(int cardKindCount, int cardCount)
    {
        _constLeftRight = false;
        _answerBox.Clear();
        _questionBox.Clear();

        QuestionMaker maker = new QuestionMaker();
        maker.Init(cardKindCount, cardCount);
        maker.Setting();

        foreach (var item in maker.GetCardNumberList(Direction.LEFT))
        {
            CreateQuestionCard(item, Direction.LEFT);
        }
        foreach (var item in maker.GetCardNumberList(Direction.RIGHT))
        {
            CreateQuestionCard(item, Direction.RIGHT);
        }
        _mustUseCardCount = cardCount;

        _mustUseLeftCardCount = 0;
        foreach (var item in maker.GetAnswerCountList(Direction.LEFT))
        {
            _mustUseLeftCardCount += item;
        }
        _mustUseRightCardCount = 0;
        foreach (var item in maker.GetAnswerCountList(Direction.RIGHT))
        {
            _mustUseRightCardCount += item;
        }

        RuleSetting();
    }
    public void CreateQuestionCard(int num, Direction d)
    {
        GameObject qCard = _qCardObjectPool.GetObject();
        SCard cardScript = qCard.GetComponent<SCard>();
        cardScript.Init(num, d);
        _questionBox.CardInput(qCard, d);
        _questionCardList.Add(cardScript);
    }
    public void CreateMovingCard(int num, Direction d)
    {
        //GameObject mCard = _mCardObjectPool.GetObject();
        //MovingCard movingCard = mCard.GetComponent<MovingCard>();
        //movingCard.Init(num, d, _questionBox.GetObject(d).GetComponent<RectTransform>().anchoredPosition
        //    + _questionBox.gameObject.GetComponent<RectTransform>().anchoredPosition
        //    , _answerBox.GetObject(d));
    }
    public void CreateAnswerCard(int num, Direction d, List<CardType> list)
    {
        if (_mustUseCardCount < 1) return;
        if (_constLeftRight)
        {
            if (_mustUseLeftCardCount == 0 && d == Direction.LEFT) return;
            if (_mustUseRightCardCount == 0 && d == Direction.RIGHT) return;
        }

        SoundMng.GetInstance.Play(0);
        _mustUseCardCount--;
        if (d == Direction.LEFT)
            _mustUseLeftCardCount--;
        else
            _mustUseRightCardCount--;

        GameObject aCard = _aCardObjectPool.GetObject();
        SCard cardScript = aCard.GetComponent<SCard>();
        cardScript.Init(num, d);
        _answerBox.CardInput(aCard, d);
        _answerBox.gameObject.GetComponent<Scales>().AddWeight(num, d);
        for (int i = 0; i < list.Count; i++)
        {
            cardScript.AddCardType(list[i]);
        }
    }
    public void ReturnCard(AnswerCard card)
    {
        _mustUseCardCount++;
        if (card.GetDirection() == Direction.LEFT)
            _mustUseLeftCardCount++;
        else
            _mustUseRightCardCount++;

        card.gameObject.transform.SetParent(_aCardObjectPool.gameObject.transform);
    }
    public void ReturnCard(QuestionCard card)
    {
        card.gameObject.transform.SetParent(_qCardObjectPool.gameObject.transform);
    }
    public void WeightSame()
    {
        if (_mustUseCardCount > 0 || !StageMng.GetInstance._isStageStart) return;
        if (_constLeftRight && (_mustUseLeftCardCount != 0 || _mustUseRightCardCount != 0)) return;

        StageMng.GetInstance.StageClear();
        Debug.Log("Stage Clear");
    }

    public void RuleSetting()
    {
        _mustObjects[0].SetActive(true);
        _mustObjects[1].SetActive(false);
        _mustObjects[2].SetActive(false);
        if (RuleMng.GetInstance.isRuleBeing((int)RuleType.QUESTION_MARK))
        {
            int ran = Random.Range(0, 10);
            if (ran > 3)
            {
                int ranNum = Random.Range(0, _questionCardList.Count);
                _questionCardList[ranNum].AddCardType(CardType.QUESTION_MARK);
            }
        }
        if(RuleMng.GetInstance.isRuleBeing((int)RuleType.CONST_LEFT_RIGHT))
        {
            int ranNum = Random.Range(0, 10);
            if (ranNum > 6)
            {
                _constLeftRight = true;
                _mustObjects[0].SetActive(false);
                _mustObjects[1].SetActive(true);
                _mustObjects[2].SetActive(true);
            }
        }
    }
}