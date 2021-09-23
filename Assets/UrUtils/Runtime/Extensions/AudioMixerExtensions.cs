using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace UrUtils.Extensions
{
    public static class AudioMixerExtensions
    {
        public static IEnumerator FadeVolume(this AudioMixer mixer,
            string volumeParameterName, float duration, float targetVolume, Action onFinished = null)
        {
            float currentTime = 0;
            mixer.GetFloat(volumeParameterName, out var currentVolume);
            currentVolume = Mathf.Pow(10, currentVolume / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVolume = Mathf.Lerp(currentVolume, targetValue, currentTime / duration);
                mixer.SetFloat(volumeParameterName, Mathf.Log10(newVolume) * 20);
                yield return null;
            }

            onFinished?.Invoke();
        }

        public static IEnumerator FadeFloat(this AudioMixer mixer,
            string parameterName, float duration, float targetValue, Action onFinished = null)
        {
            float currentTime = 0;
            mixer.GetFloat(parameterName, out var currentValue);

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
