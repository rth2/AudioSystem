using UnityEngine;
using System.Collections.Generic;

namespace CracklingLeafTools.AudioSystem.Core
{
    public class AudioPool : MonoBehaviour
    {
        [SerializeField] int initialPoolSize = 10;

        List<AudioSource> audioSources = new();

        Transform poolParent;

        #region Initialization
        public void Initialize(Transform parent = null)
        {
            ClearPool();

            poolParent = parent != null ? parent :  transform;

            //todo: prewarm pool with initial pool size amount of audioSources.
        }

        void ClearPool()
        {
            for(int i = 0; i < audioSources.Count; i++)
            {
                if (audioSources[i] != null) { Destroy(audioSources[i].gameObject); }
            }
            audioSources.Clear();
        }

        #endregion

        #region Audio Source Management

        public AudioSource GetAvailableSource()
        {
            // TODO: Find inactive or finished AudioSource.
            // TODO: Expand pool if none available and allowed.
            return null;
        }

        public void ReturnToPool(AudioSource source)
        {
            // TODO: Stop playback, deactivate or reset source.
        }

        #endregion

        #region Editor and Debug

        private void OnValidate()
        {
            if (initialPoolSize < 1) initialPoolSize = 1;
        }

        #endregion
    }
}

