using _VC_Racing._Scripts.ControllerRelated;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float carSpeed;

    private float roadHeight;
    private Rigidbody2D _rb;

    [SerializeField] private float slowDownDelay = 3f;
    private float _timer;
    private bool _raceStarted;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private bool _voiceDetected;

    public void ToggleDetectionState(bool state)
    {
        _voiceDetected = state;
    }

    void Update()
    {
        MoveCar();

        if (!_raceStarted)
            return;
        if (!_voiceDetected)
        {
            _timer += Time.deltaTime;
            if (_timer >= slowDownDelay)
            {
                carSpeed -= 0.5f;
                _timer = 0;
            }
        }
        else _timer = 0;
    }

    void OnEnable()
    {
        MainController.GameStateChanged += GameManager_GameStateChanged;
    }

    void OnDisable()
    {
        MainController.GameStateChanged -= GameManager_GameStateChanged;
    }

    void GameManager_GameStateChanged(GameState newState, GameState oldState)
    {
        if (newState == GameState.RaceStarted)
        {
            _raceStarted = true;
        }
    }

    public void EnableMovement()
    {
        //Debug.Log("CALLED FROM SPEECHHH...");
        carSpeed += 1;
        UIController.instance.UpdateSpeedUi(carSpeed);
    }

    void MoveCar()
    {
        _rb.velocity = (Vector2.right * carSpeed);
    }
}