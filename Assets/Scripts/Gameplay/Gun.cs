using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _pointer = null;
    [SerializeField] private Transform _pointerForForce = null;
    [SerializeField] private LineRenderer _forceLine = null;
    [SerializeField] private float _maxForce = 100;

    private Vector3 _forceLineSecondPosition = Vector3.zero;

    private BallFactory _ballFactory = null;
    private GameObject _currentPrefab = null;
    private float _currentForce = 0;

    private float _yStartAngle = 0;

    public void Init(BallFactory ballFactory) {
        _ballFactory = ballFactory;
        _forceLine.positionCount = 2;
        _forceLine.enabled = false;

        _yStartAngle = transform.eulerAngles.y;
    }

    public void Shot() {
        var body = _ballFactory.GetBall(_currentPrefab, _pointer.position, _pointer.rotation);
        var forece = body.transform.forward * _maxForce * _currentForce;
        body.velocity = Vector3.zero;
        body.AddForce(forece, ForceMode.Impulse);
        _currentForce = 0;
        DisplayForce();
    }

    public void SetBall(BallData ballData) {
        _currentPrefab = ballData.Prefab;
    }

    public void SetForce(float force) {
        _currentForce = force;
        DisplayForce();
    }

    public void Rotate(float yAngle) {
        yAngle = Mathf.Clamp(yAngle, -90, 90);
        yAngle += _yStartAngle;

        transform.rotation = Quaternion.Euler(0, yAngle, 0);

        DisplayForce();
    }

    private void DisplayForce() {
        var needDisplay = _currentForce > 0;
        _forceLine.enabled = needDisplay;

        if (needDisplay) {
            _forceLineSecondPosition = Vector3.Lerp(_pointer.position, _pointerForForce.position, _currentForce);
            _forceLine.SetPosition(0, _pointer.position);
            _forceLine.SetPosition(1, _forceLineSecondPosition);
        }
    }
}
