using UnityEngine;

public class PoolWatcher : MonoBehaviour
{
    PooledAudioSource pooled;
    System.Action<PooledAudioSource> callback;

    public void BeginWatching(PooledAudioSource pooled, System.Action<PooledAudioSource> callback)
    {
        this.pooled = pooled;
        this.callback = callback;
        StartCoroutine(Watch());
    }

    System.Collections.IEnumerator Watch()
    {
        //wait until audio is done
        yield return new WaitUntil(() =>
        pooled.Source == null ||
        !pooled.Source.isPlaying ||
        !pooled.Source.gameObject.activeInHierarchy);

        Debug.Log($"Watcher done");
        callback?.Invoke(pooled);
        Destroy(this);  //remove component
    }

}
