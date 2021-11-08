using UnityEngine;

public class BallFactory : MonoBehaviour
{
    private BallsList _ballsList = null;
    private Transform _ballsContainer = null;

    public void Init(BallsList ballsList, Transform ballsContainer) {
        _ballsList = ballsList;
        _ballsContainer = ballsContainer;
    }

    public Rigidbody GetBall(GameObject prefab, Vector3 position, Quaternion rotation) {
        var ball = Instantiate(prefab, position, rotation, _ballsContainer);
        _ballsList.Add(ball);
        return ball.GetComponent<Rigidbody>();
    }
}
