using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

namespace CracklingLeafTools.AudioSystem.Core.Editor
{

    public static class SoundKeyGenerator
    {
        const string GeneratedFolder = "Assets/CracklingLeafTools/AudioSystem/Core/Generated/";
        const string FileName = "SoundKey.cs";

        [MenuItem("CracklingLeafTools/Audio System/ Generate SoundKey Enum")]
        public static void GenerateEnum()
        {   //find all sound definitions
            string[] guids = AssetDatabase.FindAssets("t:SoundDefinition");
            //create directory if it doesn't exist.
            if((!Directory.Exists(GeneratedFolder))) { Directory.CreateDirectory(GeneratedFolder); }

            //begin writing file
            StringBuilder sb = new();
            sb.AppendLine($"namespace CracklingLeafTools.AudioSystem.Core");
            sb.AppendLine("{");
            sb.AppendLine($"    public enum SoundKey");
            sb.AppendLine("    {");
            sb.AppendLine($"        None = 0,");

            foreach(string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SoundDefinition def = AssetDatabase.LoadAssetAtPath<SoundDefinition>(path);
                if(def == null) { continue; }

                string enumName = MakeEnumSafe(def.soundKey);
                sb.AppendLine($"        {enumName},");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            //write the file
            string fullPath = Path.Combine(GeneratedFolder, FileName);
            File.WriteAllText(fullPath, sb.ToString(), Encoding.UTF8);

            //refresh unity
            AssetDatabase.Refresh();
            Debug.Log($"SoundKey enum generated with {guids.Length} entries.");
        }

        //converts a definition name into a valid enum name
        static string MakeEnumSafe(string key)
        {
            if(string.IsNullOrWhiteSpace(key)) { return "_UnknownKey"; }
            //replace separators with underscores
            string safe = key.Replace(" ", "_").Replace("-", "_");
            //remove invalid characters
            StringBuilder sb = new();
            foreach(char c in safe)
            {
                if(char.IsLetterOrDigit(c) || c == '_') { sb.Append(c); }
            }

            safe = sb.ToString();
            //ensure it begins with a letter for valid c# identifier
            if(!char.IsLetter(safe, 0)) { safe = "_" + safe; }
            return safe;
        }
    }

}


