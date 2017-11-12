using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales : MonoBehaviour {

    [SerializeField]
    private float _revision = 0.1f;

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

        var result = _leftWeight - _rightWeight;
        var objects = 20;
        if (_leftWeight < _rightWeight)
        {
            objects *= -1;
            result *= -1;
        }
        else if (_leftWeight == _rightWeight)
        {
            objects = 0;
            result = 10;
        }
        var preRot = _transform.rotation;
        var nextRot = Quaternion.Euler(preRot.x, preRot.y, objects);
        _transform.rotation = Quaternion.Slerp(preRot, nextRot, Time.deltaTime * result * _revision);
    }

    public void AddWeight(int weight, Direction direction)
    {
        if (direction == Direction.LEFT)
            _leftWeight += weight;
        else
            _rightWeight += weight;
    }
}
