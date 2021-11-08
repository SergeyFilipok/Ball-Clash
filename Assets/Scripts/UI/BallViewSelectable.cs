using System;
using UnityEngine;
using UnityEngine.UI;

public class BallViewSelectable : BallView {
    [SerializeField] private Image _selector = null;
    [SerializeField] private Button _button = null;

    public event Action Click;

    private void Awake() {
        _button.onClick.AddListener(OnClick);
    }

    public void Select() {
        _selector.enabled = true;
    }

    public void Deselect() {
        _selector.enabled = false;
    }

    private void OnClick() {
        Click?.Invoke();
    }
}
