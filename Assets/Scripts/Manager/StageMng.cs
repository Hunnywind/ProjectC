using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStates;
using UnityEngine.UI;

public class StageMng : Singleton<StageMng> {

    

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
    public int _stageNum { private set; get; }

    private float _time;
    
    private void Start()
    {
        Init();
        _levelText.text = "Level  " + (_stageNum + 1);
    }
    private void Update()
    {
        //_levelText.text = "Level  " + _stageNum;
    }
    private void FixedUpdate()
    {
        if (_isStageStart)
        {
            _time += Time.deltaTime;
            //int second = (int)_time;
            //_timeText.text = second.ToString();
            _timeText.text = _time.ToString("F2");
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
            SoundMng.GetInstance.PlayBGM(0);
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
        RuleMng.GetInstance.Setting();
        RuleMng.GetInstance.SetCurrentRule(_stageNum);
        _levelText.text = "Level  " + (_stageNum + 1);
        _time = 0f;
        GameMng.GetInstance.ChangeState(new PlayState());
        _stageUseObject.SetActive(true);
        _stageNotUseObject.SetActive(false);
        _mainGamePanel.SetActive(true);
        _levelClear.SetActive(false);

        _isStageStart = true;
        int id = RuleMng.GetInstance.RuleCount * 7 + _stageNum;
        var count = GameData.GetInstance.GetGameData(DataKind.NORMALSTAGE, id, "CardCount");
        int cardCount = int.Parse(count);
        var kind = GameData.GetInstance.GetGameData(DataKind.NORMALSTAGE, id, "CardKind");
        int cardKind = int.Parse(kind);

        CardMng.GetInstance.CardSetting(cardKind, cardCount);
        ScoreMng.GetInstance.SetDifficult(cardCount * cardKind);
        _scales.Clear();
    }
    public void StageClear()
    {
        CoroutineManager.instance.StartCoroutine(StageClearCoroutine());
    }
    public void LevelClear()
    {
        // Cheat
        _levelClear.SetActive(true);
        ScoreMng.GetInstance.AddScore(100000);
        ScoreMng.GetInstance.StageClear();
        CoroutineManager.instance.StartCoroutine(LevelClearCoroutine());
    }
    public IEnumerator StageClearCoroutine()
    {
        _isStageStart = false;
        _stageNum++;
        ScoreMng.GetInstance.SetTime(_time);
        ScoreMng.GetInstance.StageClear();
        if (_stageNum > 6)
        {
            _levelClear.SetActive(true);
            CoroutineManager.instance.StartCoroutine(LevelClearCoroutine());
        }
        else
        {
            SoundMng.GetInstance.PauseBGM();
            SoundMng.GetInstance.Play(2);
            _stageClear.SetActive(true);
            yield return new WaitForSeconds(2f);
            SoundMng.GetInstance.UnpauseBGM();
            yield return new WaitForSeconds(1f);
            _stageClear.SetActive(false);
            StageStart();
        }
    }
    
    IEnumerator LevelClearCoroutine()
    {
        SoundMng.GetInstance.StopBGM();
        SoundMng.GetInstance.Play(1);
        GameMng.GetInstance.ChangeState(new LevelClearState());
        _isStageStart = false;
        yield return new WaitForSeconds(3f);
        _stageNum = 0;
        _levelText.gameObject.SetActive(false);
        _mainGamePanel.SetActive(false);
        ScoreMng.GetInstance.Test();
    }
}
