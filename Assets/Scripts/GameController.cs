using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] private BallsConfig _ballsConfig = null;
    [SerializeField] private Energy _energy = null;
    [SerializeField] private BallFactory _ballFactory = null;
    [SerializeField] private BallsList _ballsList = null;
    [SerializeField] private Transform _ballsContainer = null;

    [Space]
    [Header("Player")]
    [SerializeField] private Gun _playerGun = null;
    [SerializeField] private Pocket _playerPocket = null;

    [Space]
    [Header("Enemy")]
    [SerializeField] private Gun _enemyGun = null;
    [SerializeField] private Pocket _enemyPocket = null;
    [SerializeField] private BotEnemy _botEnemy = null;

    [Space]
    [Header("UI")]
    [SerializeField] private EnergyView _energyView = null;
    [SerializeField] private InputPanel _inputPanel = null;
    [SerializeField] private BallsSelectionPanel _ballsSelector = null;
    [SerializeField] private TMPro.TMP_Text _playerScoreText = null;
    [SerializeField] private TMPro.TMP_Text _enemyScoreText = null;

    private int _playerScore = 0;
    private int _enemyScore = 0;

    private void Awake() {
        Application.targetFrameRate = 60;

        _energyView.Init(_energy);
        _ballsSelector.Init(_ballsConfig.Balls);
        _ballFactory.Init(_ballsList, _ballsContainer);

        _playerGun.Init(_ballFactory);
        _enemyGun.Init(_ballFactory);

        _playerGun.SetBall(_ballsSelector.CurrentBall);
        _enemyGun.SetBall(_ballsSelector.CurrentBall);

        _playerPocket.BallEntered += OnBallEnterInPlayerPocket;
        _enemyPocket.BallEntered += OnBallEnterInEnemyPocket;

        _inputPanel.Input += OnInput;
        _inputPanel.Shot += OnShot;

        _energy.ValueChanged += CheckEnergy;
        _ballsSelector.BallChanged += OnBallChanged;

        _botEnemy.Init(_ballsList, _ballsSelector.CurrentBall);
    }

    private void OnBallChanged() {
        var ballData = _ballsSelector.CurrentBall;
        _playerGun.SetBall(ballData);
        CheckEnergy();
    }

    private void OnBallEnterInPlayerPocket(GameObject ball) {
        DestroyBall(ball);
        _enemyScore++;
        _enemyScoreText.text = _enemyScore.ToString();
    }

    private void OnBallEnterInEnemyPocket(GameObject ball) {
        DestroyBall(ball);
        _playerScore++;
        _playerScoreText.text = _playerScore.ToString();
    }

    private void DestroyBall(GameObject ball) {
        _ballsList.Remove(ball);
        Destroy(ball);
    }

    private void CheckEnergy() {
        bool hasNextShot = _energy.CurrentValue >= _ballsSelector.CurrentBall.EnergyCost;
        if (hasNextShot) {
            _inputPanel.Enable();
        }
        else {
            _inputPanel.Disable();
        }
    }

    private void OnShot() {
        var ballData = _ballsSelector.CurrentBall;
        if (_energy.TrySpend(ballData.EnergyCost)) {
            _playerGun.Shot();
        }
    }

    private void OnInput((float force, float yAngle) input) {
        _playerGun.SetForce(input.force);
        _playerGun.Rotate(input.yAngle);
    }
}
