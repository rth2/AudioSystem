using CracklingLeafTools.AudioSystem.Core;
using UnityEngine;

public class TestAudioPlayer : MonoBehaviour
{
    [SerializeField] SoundLibrary library;
    [SerializeField] SoundLibrary dragonLibrary;
    [SerializeField] SoundLibrary goblinLibrary;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { AudioManager.Instance.PlaySound(SoundKey.AllSounds_DragonBreath); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { AudioManager.Instance.PlaySound(SoundKey.AllSounds_Goblin); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { AudioManager.Instance.PlaySound(SoundKey.AllSounds_Pickup_Gold); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { AudioManager.Instance.PlaySound(SoundKey.AllSounds_CastSpell); }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) { AudioManager.Instance.PlaySound(SoundKey.Dragon_DragonBreath); }

        if(Input.GetKeyDown(KeyCode.D)) { AudioManager.Instance.AddSceneLibrary(dragonLibrary); }
        if(Input.GetKeyDown(KeyCode.A)) { AudioManager.Instance.AddSceneLibrary(library); }
        if (Input.GetKeyDown(KeyCode.G)) { AudioManager.Instance.AddSceneLibrary(goblinLibrary); }

        if (Input.GetKeyDown(KeyCode.E)) { AudioManager.Instance.RemoveSceneLibrary(dragonLibrary); }
        if(Input.GetKeyDown(KeyCode.Q)) { AudioManager.Instance.RemoveSceneLibrary(library); }
        if (Input.GetKeyDown(KeyCode.T)) { AudioManager.Instance.RemoveSceneLibrary(goblinLibrary); }
        if (Input.GetKeyDown(KeyCode.C)) { AudioManager.Instance.ClearSceneLibraries(); }
    }
}
