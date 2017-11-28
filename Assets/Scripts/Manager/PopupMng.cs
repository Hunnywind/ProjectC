using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BUTTON_KIND
{
    YES = 1 << 0,
    NO = 1 << 1,
    OK = 1 << 2,
}

public struct MESSAGE_POPUP_INFO
{
    public string _title;
    public string _msg;
    public BUTTON_KIND _kind;
    public Action<bool> _yesAction;
    public Action<bool> _noAction;
    public Action<bool> _okAction;


    public MESSAGE_POPUP_INFO(string title, string msg, BUTTON_KIND kind, Action<bool> yes, Action<bool> no, Action<bool> ok)
    {
        _title = title;
        _msg = msg;
        _kind = kind;
        _yesAction = yes;
        _noAction = no;
        _okAction = ok;
    }
}

public class PopupMng : Singleton<PopupMng> {
    [SerializeField]
    private GameObject _popupPanel = null;
    [SerializeField]
    private Text _titleText = null;
    [SerializeField]
    private Text _popupText = null;
    [SerializeField]
    private GameObject _yesButton = null;
    [SerializeField]
    private GameObject _noButton = null;
    [SerializeField]
    private GameObject _okButton = null;

    private Action<bool> _yesAction;
    private Action<bool> _noAction;
    private Action<bool> _okAction;

    private Stack<Action<bool>> _popupOnStack = new Stack<Action<bool>>();
    private Queue<MESSAGE_POPUP_INFO> _messageQueue = new Queue<MESSAGE_POPUP_INFO>();

    private bool _isMessagePopup;

    public void PopupMessage(string title, string msg, BUTTON_KIND kind, Action<bool> yesAction = null, Action<bool> noAction = null, Action<bool> okAction = null)
    {
        _messageQueue.Enqueue(new MESSAGE_POPUP_INFO(title, msg, kind, yesAction, noAction, okAction));

        if (_messageQueue.Count == 1)
            CoroutineManager.instance.StartCoroutine(MessagePopupProc());
    }
    IEnumerator MessagePopupProc()
    {
        MESSAGE_POPUP_INFO info = _messageQueue.Dequeue();
        _yesButton.SetActive((info._kind & BUTTON_KIND.YES) != 0);
        _noButton.SetActive((info._kind & BUTTON_KIND.NO) != 0);
        _okButton.SetActive((info._kind & BUTTON_KIND.OK) != 0);

        _titleText.text = info._title;
        _popupText.text = info._msg;
        _yesAction = info._yesAction;
        _noAction = info._noAction;
        _okAction = info._okAction;
        _popupPanel.SetActive(true);
        AddCloseFunc(CloseMessagePopup);

        _isMessagePopup = true;
        while (_isMessagePopup)
            yield return null;

        if (_messageQueue.Count > 0)
            CoroutineManager.instance.StartCoroutine(MessagePopupProc());
    }

    IEnumerator CloseProc()
    {
        yield return new WaitForEndOfFrame();
        _isMessagePopup = false;
    }

    public void ClickYes(string param)
    {
        DeleteCloseFunc();
        _popupPanel.SetActive(false);
        if (null != _yesAction)
            _yesAction(true);

        if (_messageQueue.Count <= 0)
            CoroutineManager.instance.StartCoroutine(CloseProc());
    }

    public void ClickNo(string param)
    {
        DeleteCloseFunc();
        _popupPanel.SetActive(false);
        if (null != _noAction)
            _noAction(true);

        if (_messageQueue.Count <= 0)
            CoroutineManager.instance.StartCoroutine(CloseProc());
    }

    public void ClickOk(string param)
    {
        DeleteCloseFunc();
        _popupPanel.SetActive(false);
        if (null != _okAction)
            _okAction(true);

        if (_messageQueue.Count <= 0)
            CoroutineManager.instance.StartCoroutine(CloseProc());
    }

    public void DeleteCloseFunc()
    {
        if (_popupOnStack.Count > 0)
            _popupOnStack.Pop();
    }
    void CloseMessagePopup(bool value)
    {
        _popupPanel.SetActive(false);
        if (null != _okAction)
            _okAction(true);

        if (null != _noAction)
            _noAction(true);

        if (_messageQueue.Count <= 0)
            CoroutineManager.instance.StartCoroutine(CloseProc());
    }
    public void AddCloseFunc(Action<bool> action)
    {
        _popupOnStack.Push(action);
    }
    public void AllClearCloseFunc()
    {
        _popupOnStack.Clear();
    }

}
