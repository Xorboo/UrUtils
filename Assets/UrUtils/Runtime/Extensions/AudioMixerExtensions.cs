using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace UrUtils.Extensions
{
    public static class AudioMixerExtensions
    {
        /// <summary>
        /// Set volume using a [0..1] range
        /// </summary>
        public static void SetVolume(this AudioMixer mixer, string volumeParameterName, float volume)
        {
            volume = Mathf.Clamp(volume, 0.0001f, 1);
            mixer.SetFloat(volumeParameterName, Mathf.Log10(volume) * 20);
        }

        /// <summary>
        /// Get volume using a [0..1] range
        /// </summary>
        public static bool GetVolume(this AudioMixer mixer, string volumeParameterName, out float volume)
        {
            if (!mixer.GetFloat(volumeParameterName, out volume))
            {
                Debug.LogError($"Can't get volume on mixer '{mixer.name}' on parameter '{volumeParameterName}'");
                return false;
            }

            volume = Mathf.Pow(10, volume / 20);
            return true;
        }

        public static IEnumerator FadeVolume(this AudioMixer mixer,
            string volumeParameterName, float duration, float targetVolume, Action onFinished = null)
        {
            if (!mixer.GetVolume(volumeParameterName, out var currentVolume))
                yield break;

            float currentTime = 0;
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVolume = Mathf.Lerp(currentVolume, targetValue, currentTime / duration);
                mixer.SetVolume(volumeParameterName, newVolume);
                yield return null;
            }

            onFinished?.Invoke();
        }

        public static IEnumerator FadeFloat(this AudioMixer mixer,
            string parameterName, float duration, float targetValue, Action onFinished = null)
        {
            float currentTime = 0;
            if (!mixer.GetFloat(parameterName, out var currentValue))
            {
                Debug.LogError($"Can't get float '{parameterName}' from mixer '{mixer.name}'");
                yield break;
            }

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newValue = Mathf.Lerp(currentValue, targetValue, currentValue / duration);
                mixer.SetFloat(parameterName, newValue);
                yield return null;
            }

            onFinished?.Invoke();
        }
    }
}
