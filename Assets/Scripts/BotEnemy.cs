using UniRx;
using UnityEngine;

public class BotEnemy : MonoBehaviour
{
    [SerializeField] private float _shotTime = 3;
    [SerializeField] private float _rotationSpeed = 3;
    [SerializeField] private Gun _gun = null;

    private GameObject _closetBall;
    private BallsList _ballsList;

    public void Init(BallsList ballsList, BallData ballData) {
        _ballsList = ballsList;
        _gun.SetBall(ballData);
        WaitClosetBall();
    }

    private void WaitClosetBall() {
        Debug.Log("WaitClosetBall()");
        Observable.EveryUpdate().TakeWhile(_ => _closetBall == null).Subscribe(l=> {
            _closetBall = _ballsList.GetCloser(_gun.transform.position);
        }, Rotate);
    }

    private void Rotate() {
        Debug.Log("Rotate()");
        var direction = _closetBall.transform.position - _gun.transform.position;
        var targetAngle = Vector3.SignedAngle(_gun.transform.forward, direction, Vector3.up);
        Debug.Log("Angle: " + targetAngle);
        //Observable.EveryUpdate().TakeWhile(_ => Mathf.Abs(targetAngle) > 0).Subscribe(l => {
        //    var tempRot = _rotationSpeed * Time.deltaTime * Mathf.Sign(targetAngle);
        //    if (Mathf.Abs(tempRot) > Mathf.Abs(targetAngle)) {
        //        targetAngle = 0;
        //    }
        //    else {
        //        targetAngle -= tempRot;
        //    }
        //    Debug.Log("For Rotation: " + tempRot);
        //    _gun.Rotate(tempRot);
        //}, Shot);

        _gun.Rotate(targetAngle);
        Shot();
    }

    private void Shot() {
        Debug.Log("Shot()");
        _gun.SetForce(Random.value);
        _gun.Shot();
        _closetBall = null;
        Wait();
    }

    private void Wait() {
        Debug.Log("Wait()");
        Observable.Timer(System.TimeSpan.FromSeconds(Random.Range(1, _shotTime))).Subscribe(_ => WaitClosetBall());
    }
}
