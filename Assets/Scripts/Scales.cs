using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales : MonoBehaviour {

    private int _leftWeight;
    private int _rightWeight;
    private RectTransform _transform;

    public void Clear()
    {
        _leftWeight = 0;
        _rightWeight = 0;
    }
    private void Start()
    {
        _transform = gameObject.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (_leftWeight == _rightWeight && _leftWeight != 0)
        {
            CardMng.GetInstance.WeightSame();
        }

        int result = _leftWeight - _rightWeight;
        var preRot = _transform.rotation;
        var nextRot = Quaternion.Euler(preRot.x, preRot.y, result);
        _transform.rotation = Quaternion.Slerp(preRot, nextRot, Time.deltaTime);
    }

    public void AddWeight(int weight, Direction direction)
    {
        if (direction == Direction.LEFT)
            _leftWeight += weight;
        else
            _rightWeight += weight;
    }
}
