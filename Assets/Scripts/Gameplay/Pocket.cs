using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Pocket : MonoBehaviour
{
    [SerializeField] private Collider _collider = null;
    [SerializeField] private ObservableTriggerTrigger _trigger = null;

    public event Action<GameObject> BallEntered;

    private void Awake() {
        _trigger.OnTriggerEnterAsObservable().Subscribe(OnTrigger).AddTo(this);
    }

    private void OnTrigger(Collider other) {
        if (other.tag == "Ball") {
            BallEntered?.Invoke(other.attachedRigidbody.gameObject);
        }
    }

    private void Reset() {
        if (_collider == false) {
            _collider = GetComponentInChildren<Collider>();
            _collider.isTrigger = true;
            _trigger = _collider.gameObject.AddComponent<ObservableTriggerTrigger>();
        }
    }
}
