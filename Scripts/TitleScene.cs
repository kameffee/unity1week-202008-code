using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public async void OpenMenu()
    {
        GameManager.Instance.SceneTransition.Load("Intro");
    }

    public async void OpenSettings()
    {
        GameManager.Instance.Settingses.Open();
    }
}