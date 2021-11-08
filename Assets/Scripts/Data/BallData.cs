using UnityEngine;

[CreateAssetMenu(fileName = "Ball", menuName = "Configs/Ball")]
public class BallData : ScriptableObject
{
    [SerializeField] private string _name = "";
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private int _energyCost = 2;
    [SerializeField] private GameObject _prefab = null;

    public string Name { get => _name; }
    public Color Color { get => _color; }
    public int EnergyCost { get => _energyCost; }
    public GameObject Prefab { get => _prefab; }
}
