using System.Collections.Generic;
using UnityEngine;

public class BallsList : MonoBehaviour
{
    private List<GameObject> _balls = new List<GameObject>(20);

    public void Add(GameObject ball) => _balls.Add(ball);
    public void Remove(GameObject ball) => _balls.Remove(ball);
    public GameObject GetCloser(Vector3 point) {
        if (_balls.Count == 0) return null;

        GameObject ball = _balls[0];
        float distance = Vector3.Distance(point, ball.transform.position);

        for (int i = 1; i < _balls.Count; i++) {
            var b = _balls[i];
            if (Vector3.Distance(point, b.transform.position) < distance) {
                ball = b;
            }
        }

        return ball;
    }
}
