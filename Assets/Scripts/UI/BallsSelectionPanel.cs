using System;
using UnityEngine;

public class BallsSelectionPanel : MonoBehaviour
{
    [SerializeField] private BallView _current = null;
    [SerializeField] private BallViewSelectable[] _views = new BallViewSelectable[0];

    public event Action BallChanged;

    public BallData CurrentBall { get; set; }

    private BallData[] _ballDatas;
    private BallViewSelectable _currentView;

    public void Init(BallData[] ballDatas) {
        _ballDatas = ballDatas;
        CurrentBall = ballDatas[0];

        for (int i = 0; i < ballDatas.Length; i++) {
            var view = _views[i];
            view.Show(ballDatas[i]);
            view.Click += () => BallSelected(view);
            view.Deselect();
        }

        _current.Show(CurrentBall);
        _currentView = _views[0];
        _currentView.Select();
    }

    private void BallSelected(BallViewSelectable view) {
        if (_currentView == view) return;

        _currentView.Deselect();
        var index = Array.IndexOf(_views, view);
        CurrentBall = _ballDatas[index];
        view.Select();
        _currentView = view;
        _current.Show(CurrentBall);
        BallChanged?.Invoke();
    }
}
