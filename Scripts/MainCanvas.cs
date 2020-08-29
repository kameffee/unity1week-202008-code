using Pause;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public void Pause()
    {
        FindObjectOfType<PauseManager>().Pause();
    }
}