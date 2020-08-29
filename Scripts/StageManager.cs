using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    private CheckPointManager _checkPoint;

    private PlayingStatus _status;

    [SerializeField]
    UnityEvent onStart = new UnityEvent();

    [SerializeField]
    UnityEvent onGameOver = new UnityEvent();

    [SerializeField]
    UnityEvent onContinue = new UnityEvent();

    private void Start()
    {
        _status = FindObjectOfType<PlayingStatus>();

        player = GameObject.FindWithTag("Player");
        Debug.Assert(player != null);

        player.GetComponent<PlayerHealth>()
            .GetHP()
            .Where(hp => hp <= 0)
            .Subscribe(_ => GameOver());

        _checkPoint = FindObjectOfType<CheckPointManager>();

        GameStart();
    }

    public async void GameStart()
    {
        _status.SetPlaying(true);
        onStart.Invoke();
    }

    public async void GameOver()
    {
        _status.SetPlaying(false);
        onGameOver.Invoke();
    }

    public void Continue()
    {
        _status.SetPlaying(true);
        onContinue.Invoke();

        var continuePosition = _checkPoint.GetNewestPoint();
        player.transform.position = continuePosition;

        var health = player.GetComponent<PlayerHealth>();
        health.SetHp(health.MaxHp);

        SceneManager.LoadScene("Main");
    }
}