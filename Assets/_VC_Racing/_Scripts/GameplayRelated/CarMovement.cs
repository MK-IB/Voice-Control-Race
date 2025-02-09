using System;
using System.Collections;
using _VC_Racing._Scripts.ControllerRelated;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float carSpeed;

    private float roadHeight;
    private Rigidbody2D _rb;

    [SerializeField] private float slowDownDelay = 3f;
    [SerializeField] private GameObject confettiFx;
    private float _timer;
    private bool _raceStarted, _canMove;
    private UIController _uiController;
    private AudioSource carAudioSource;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _uiController = UIController.instance;
        carAudioSource = GetComponent<AudioSource>();
        _canMove = true;
    }

    private bool _voiceDetected;

    public void ToggleDetectionState(bool state)
    {
        _voiceDetected = state;
        _timer = 0;
    }

    void Update()
    {
        MoveCar();

        if (!_raceStarted) return;
        if (!_voiceDetected)
        {
            _timer += Time.deltaTime;
            if (_timer >= slowDownDelay && carSpeed > 0)
            {
                carSpeed -= 0.5f;
                ChangePitch(-0.2f);
                _timer = 0;
            }
        }
        else _timer = 0;
        _uiController.UpdateSpeedUi(carSpeed);
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
        if (newState == GameState.RaceStarted && !_raceStarted)
        {
            _raceStarted = true;
            carAudioSource.enabled = true;
        }

        if (newState == GameState.LevelComplete || newState == GameState.LevelFail)
        {
            _canMove = false;
            carAudioSource.enabled = false;
        }
    }

    public void EnableMovement()
    {
        //Debug.Log("CALLED FROM SPEECHHH...");
        carSpeed += 2;
        ChangePitch(0.2f);
    }

    void ChangePitch(float val)
    {
        carAudioSource.pitch += val;
    }

    void MoveCar()
    {
        if (!_canMove) return;
        _rb.velocity = (Vector2.right * carSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Finish"))
        {
            other.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(LevelWin());
        }
    }

    IEnumerator LevelWin()
    {
        _raceStarted = false;
        carSpeed = 0;
        confettiFx.SetActive(true);
        carAudioSource.clip = null;
        yield return new WaitForSeconds(2);
        MainController.instance.SetActionType(GameState.LevelComplete);
    }
}