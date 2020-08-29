using System;
using Arbor;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region singleton

        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
                if (_instance == null) _instance = Create();
                return _instance;
            }
        }

        private static AudioManager Create()
        {
            var asset = Resources.Load<AudioManager>("AudioManager");
            var manager = Instantiate(asset);
            DontDestroyOnLoad(manager);
            return manager;
        }

        #endregion

        [SerializeField]
        private AudioMixer mixer;

        private float GetValue(string key)
        {
            float vol;
            mixer.GetFloat(key, out vol);
            return vol;
        }

        public float GetSeDecibel() => GetValue("SE");

        public void SetSeDecibel(float decibel)
        {
            mixer.SetFloat("SE", decibel);
        }
        
        public float GetBgmDecibel() => GetValue("BGM");

        public void SetBgmDecibel(float decibel)
        {
            mixer.SetFloat("BGM", decibel);
        }

        public float GetMasterDecibel() => GetValue("Master");

        public void SetMasterDecibel(float decibel)
        {
            mixer.SetFloat("Master", decibel);
        }
    }
}