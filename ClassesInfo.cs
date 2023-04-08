using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class ClassesInfo
    {
        private List<CharacterClass> classList = new List<CharacterClass>();
        private Dictionary<string, CharacterClass> stringParseDict = new Dictionary<string, CharacterClass>();
        private Random random = new Random();
        StringBuilder stringBuilder = new StringBuilder(string.Empty);

        public ClassesInfo()
        {
            foreach (CharacterClass characterClass in Enum.GetValues(typeof(CharacterClass)))
            {
                classList.Add(characterClass);
                stringParseDict.Add($"{(int)characterClass}", characterClass);
            }
            classList.Sort((a, b) => ((int)a).CompareTo((int)b));
        }

        public CharacterClass GetRandomClass()
        {
            int randomIndex = random.Next(0, classList.Count);
            return classList[randomIndex];
        }

        public string GetReadableClassList(string separator)
        {
            stringBuilder.Clear();
            for (int index = 0; index < classList.Count; index++)
            {
                CharacterClass characterClass = classList[index];
                stringBuilder.AppendFormat("[{0}] {1}", (int)characterClass, characterClass);
                if (index < classList.Count - 1)
                    stringBuilder.Append(separator);
            }
            return stringBuilder.ToString();
        }

        public bool IsValidClassString(string stringToParse)
        {
            return stringParseDict.ContainsKey(stringToParse);
        }

        public CharacterClass ParseValidString(string validString)
        {
            return stringParseDict[validString];
        }
    }
}