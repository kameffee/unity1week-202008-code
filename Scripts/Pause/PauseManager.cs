using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Pause
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseCanvasPrefab;

        public bool isPause { get; private set; } = false;

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.P))
                .Subscribe(_ =>
                {
                    if (!isPause) Pause();
                    else Resume();
                });
        }

        public async void Pause()
        {
            isPause = true;
            Time.timeScale = 0;

            var canvas = Instantiate(pauseCanvasPrefab);
            await canvas.GetComponent<PauseCanvas>().Open();
        }

        public async void Resume()
        {
            var canvas = FindObjectOfType<PauseCanvas>();
            if (canvas != null)
            {
                await canvas.Close();
            }

            isPause = false;
            Time.timeScale = 1;
        }
    }
}