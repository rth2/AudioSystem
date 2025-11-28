using UnityEngine;

namespace CracklingLeafTools.AudioSystem.Core
{
    public class PooledAudioSource
    {
        public AudioSource Source { get; private set; }
        public bool IsActive { get; private set; }

        System.Action<PooledAudioSource> onFinished;

        public PooledAudioSource(AudioSource source, System.Action<PooledAudioSource> callback)
        {
            Source = source;
            onFinished = callback;
        }

        public AudioSource Activate()
        {
            IsActive = true;
            Source.gameObject.SetActive(true);

            Source.Stop();
            Source.clip = null;
            Source.volume = 1f;
            Source.pitch = 1f;
            Source.loop = false;
            Source.outputAudioMixerGroup = null;

            //make sure no old watcher exists
            PoolWatcher existing = Source.GetComponent<PoolWatcher>();
            if (existing != null)
            {
                Object.Destroy(existing);
            }
            //add a fresh watcher
            //Source.gameObject.AddComponent<PoolWatcher>().BeginWatching(this, onFinished);
            return Source;
        }

        public void StartWatcher() { Source.gameObject.AddComponent<PoolWatcher>().BeginWatching(this, onFinished); }

        public void ReturnToPool()
        {
            IsActive = false;

            PoolWatcher watcher = Source.GetComponent<PoolWatcher>();
            if (watcher != null)
            {
                Object.Destroy(watcher);
            }

            if (Source != null)
            {
                Source.Stop();
                Source.clip = null;
                Source.gameObject.SetActive(false);
            }
        }
    }
}
