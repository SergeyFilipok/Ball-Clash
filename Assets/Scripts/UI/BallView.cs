using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name = null;
    [SerializeField] private TMP_Text _cost = null;
    [SerializeField] private Image _icon = null;

    public void Show(BallData data) {
        _name.text = data.Name;
        _cost.text = data.EnergyCost.ToString();
        _icon.color = data.Color;
    }
}
