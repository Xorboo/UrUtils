using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace UrUtils.Extensions
{
    public static class AudioMixerExtensions
    {
        public const string ExposedVolumeName = "Volume";


        public static IEnumerator FadeVolume(this AudioMixer mixer, float duration, float targetVolume)
        {
            return mixer.FadeVolume(ExposedVolumeName, duration, targetVolume);
        }

        public static IEnumerator FadeVolume(this AudioMixer mixer,
            string volumeParameterName, float duration, float targetVolume)
        {
            float currentTime = 0;
            mixer.GetFloat(volumeParameterName, out var currentVolume);
            currentVolume = Mathf.Pow(10, currentVolume / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVol = Mathf.Lerp(currentVolume, targetValue, currentTime / duration);
                mixer.SetFloat(volumeParameterName, Mathf.Log10(newVol) * 20);
                yield return null;
            }
        }
    }
}
