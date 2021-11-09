using UnityEngine;
using UniRx;
using System;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private int _tempLayer = 11;
    [SerializeField] private int _ballLayer = 12;
    [SerializeField] private float _transitionTime = 0.5f;

    private BallsList _ballsList = null;
    private Transform _ballsContainer = null;

    public void Init(BallsList ballsList, Transform ballsContainer) {
        _ballsList = ballsList;
        _ballsContainer = ballsContainer;
    }

    public Rigidbody GetBall(GameObject prefab, Vector3 position, Quaternion rotation) {
        var ball = Instantiate(prefab, position, rotation, _ballsContainer);
        ball.layer = _tempLayer;
        Observable.Timer(TimeSpan.FromSeconds(_transitionTime)).Subscribe(_=> { ball.layer = _ballLayer; });
        _ballsList.Add(ball);
        return ball.GetComponent<Rigidbody>();
    }
}
