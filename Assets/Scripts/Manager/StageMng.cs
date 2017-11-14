using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStates;
using UnityEngine.UI;

public class StageMng : Singleton<StageMng> {

    private int _stageNum;

    [SerializeField]
    private GameObject _stageUseObject;
    [SerializeField]
    private GameObject _stageNotUseObject;
    [SerializeField]
    private GameObject _mainGamePanel;
    [SerializeField]
    private GameObject _levelClear;
    [SerializeField]
    private GameObject _stageClear;
    [SerializeField]
    private Text _levelText;
    [SerializeField]
    private Text _timeText;

    [SerializeField]
    private Scales _scales;

    public bool _isStageStart { private set; get; }
    private float _time;
    
    private void Start()
    {
        Init();
        
    }
    private void Update()
    {
        _levelText.text = "Level  " + _stageNum;
    }
    private void FixedUpdate()
    {
        if (_isStageStart)
        {
            _time += Time.deltaTime;
            int second = (int)_time;
            _timeText.text = second.ToString();
        }
    }
    void Init()
    {
        _stageNum = 0;
        _isStageStart = false;
        LobbySetting();
    }
    public void Touch()
    {
        if(GameMng.GetInstance.GetStageName().Equals("LOBBY"))
        {
            StageStart();
        }
    }
    public void LobbySetting()
    {
        _stageUseObject.SetActive(false);
        _stageNotUseObject.SetActive(true);
        _stageClear.SetActive(false);
        _levelClear.SetActive(false);
        _levelText.gameObject.SetActive(true);
        GameMng.GetInstance.ChangeState(new LobbyState());
    }
    public void StageStart()
    {
        _time = 0f;
        GameMng.GetInstance.ChangeState(new PlayState());
        _stageUseObject.SetActive(true);
        _stageNotUseObject.SetActive(false);
        _mainGamePanel.SetActive(true);
        _levelClear.SetActive(false);

        _isStageStart = true;
        var value = GameData.GetInstance.GetGameData(DataKind.NORMALSTAGE,_stageNum,"CardCount");
        int cardCount = int.Parse(value);
        value = GameData.GetInstance.GetGameData(DataKind.NORMALSTAGE, _stageNum, "CardKind");
        int cardKind = int.Parse(value);

        CardMng.GetInstance.CardSetting(cardKind, cardCount);
        _scales.Clear();
    }

    public IEnumerator StageClear()
    {
        _isStageStart = false;
        _stageNum++;

        if(_stageNum > 5)
        {
            _levelClear.SetActive(true);
            CoroutineManager.instance.StartCoroutine(LevelClear());
        }
        else
        {
            _stageClear.SetActive(true);
            yield return new WaitForSeconds(3f);
            _stageClear.SetActive(false);
            StageStart();
        }
    }
    
    IEnumerator LevelClear()
    {
        GameMng.GetInstance.ChangeState(new LevelClearState());
        _isStageStart = false;
        yield return new WaitForSeconds(3f);
        _stageNum = 0;
        _levelText.gameObject.SetActive(false);
        _mainGamePanel.SetActive(false);
        ScoreMng.GetInstance.AddScore(150);
        ScoreMng.GetInstance.Test();
    }
}
