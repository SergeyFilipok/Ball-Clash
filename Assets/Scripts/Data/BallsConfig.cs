using UnityEngine;

[CreateAssetMenu(fileName = "Balls Config", menuName = "Configs/Balls Config")]
public class BallsConfig : ScriptableObject
{
    [SerializeField] private BallData[] balls = new BallData[0];

    public BallData[] Balls { get => balls; }
}
