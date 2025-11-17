using System.Collections.Generic;
using CracklingLeafTools.AudioSystem.Core;
using NUnit.Framework;
using UnityEngine;

namespace CracklingLeafTools.AudioSystem.Core
{
    [CreateAssetMenu(
    fileName = "SoundLibrary",
    menuName = "CracklingLeafTools/Audio System/SoundLibrary")]
    public class SoundLibrary : ScriptableObject
    {
        public string libraryName;
        public List<SoundDefinition> sounds = new();

        Dictionary<string, SoundDefinition> lookup;

        public SoundDefinition Get(string guid)
        {
            lookup ??= BuildLookup();
            return lookup.TryGetValue(guid, out SoundDefinition def) ? def : null;
        }


        Dictionary<string, SoundDefinition> BuildLookup()
        {
            Dictionary<string, SoundDefinition> dict = new();
            foreach(SoundDefinition def in sounds)
            {
                if(def != null) { dict[def.Guid] = def; }
            }
            return dict;
        }
    }
}
