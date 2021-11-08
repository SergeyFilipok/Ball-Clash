using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Graphic _raycastTarget = null;
    [SerializeField] private float _sensitivity = 1;
    [SerializeField] private float _maxInputMagnitude = 3;

    private float _dpi;
    private Vector2 _startPoint;
    private float _force;
    private float _yAngle;
    private Vector2 _touchDirection;
    private float _distance;

    public event Action<(float force, float yAngle)> Input;
    public event Action Shot;

    private void Awake() {
        _dpi = Screen.dpi;
        _maxInputMagnitude *= _dpi;
    }

    public void Enable() {
        _raycastTarget.enabled = true;
    }

    public void Disable() {
        _raycastTarget.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        _startPoint = eventData.position;
    }

    public void OnDrag(PointerEventData eventData) {
        _touchDirection = eventData.position - _startPoint;
        _yAngle = Vector2.SignedAngle(Vector2.up, _touchDirection) * -1;

        if (eventData.position.y > _startPoint.y) {
            _distance = Vector2.Distance(_startPoint, eventData.position);
            _force = (_distance/ _maxInputMagnitude) * _sensitivity;
            _force = Mathf.Clamp01(_force);
        }
        else {
            _force = 0;
        }
        Input?.Invoke((_force, _yAngle));
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (_force > 0) {
            Shot?.Invoke();
        }
    }
}
