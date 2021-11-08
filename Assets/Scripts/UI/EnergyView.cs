using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class EnergyView : MonoBehaviour
{
    [SerializeField] private Slider _slider = null;

    private Energy _energy;

    public void Init(Energy energy) {
        _energy = energy;
        _slider.maxValue = energy.MaxValue;

        Observable.EveryUpdate().Subscribe(_=> {
            _slider.value = _energy.CurrentValue + _energy.Progress;
        }).AddTo(this);
    }
}
