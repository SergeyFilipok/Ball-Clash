using UnityEngine;
using System;


public class Energy : MonoBehaviour
{
    [SerializeField] private float _startValue = 5;
    [SerializeField] private float _increaseValue = 1;
    [SerializeField] private float _maxValue = 10;

    private float _currentValue = 0;
    private float _tempTime = 0;

    public event Action ValueChanged;

    public float Progress => _tempTime;
    public float CurrentValue {
        get => _currentValue; set {
            _currentValue = Mathf.Clamp(value, 0, MaxValue);
            ValueChanged?.Invoke();
        }
    }
    public float MaxValue { get => _maxValue; }

    private void Awake() {
        _currentValue = _startValue;
    }

    public bool TrySpend(int value) {
        var result = CurrentValue >= value;
        CurrentValue -= value;
        return result;
    }

    private void Update() {
        _tempTime += Time.deltaTime;

        if (_tempTime >= 1) {
            _tempTime = 0;
            CurrentValue++;
        }
    }
}
