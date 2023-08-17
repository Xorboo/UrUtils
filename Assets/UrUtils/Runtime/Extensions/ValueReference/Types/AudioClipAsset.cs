using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "AudioClip")]
    public class AudioClipAsset : ValueAsset<AudioClip>
    {
        public AudioClipAsset() { }
        public AudioClipAsset(AudioClip value) : base(value) { }
    }

    [Serializable]
    public class AudioClipReference : ValueReference<AudioClip, AudioClipAsset>
    {
        public AudioClipReference() { }

        public AudioClipReference(AudioClip value) : base(value) { }
    }
}
