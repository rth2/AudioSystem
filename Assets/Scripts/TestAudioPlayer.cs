using CracklingLeafTools.AudioSystem.Core;
using UnityEngine;

public class TestAudioPlayer : MonoBehaviour
{
    [SerializeField] SoundLibrary library;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) { PlayIndex(0); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { PlayIndex(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { PlayIndex(2); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { PlayIndex(3); }
    }

    void PlayIndex(int index)
    {
        Debug.Log($"[TestAudioPlayer] Index: {index}");
        if(index < 0 || index >= library.sounds.Count) { return; }

        SoundDefinition def = library.sounds[index];
        if(def == null) { Debug.Log($"No sound definition at index {index}"); return; }
        AudioManager.Instance.PlaySound(def);
    }

}
