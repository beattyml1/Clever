using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clever.Collection;

namespace Clever.Mapping
{
    public static class Parser
    {
        public static string[] GetWords(string identifier)
        {
            return identifier
                .Aggregate(new string[0], ProcessChar)
                .Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }


        public static string[] ProcessChar(string[] words, char currentChar)
        {
            var mostRecentChar = GetMostRecentChar(words);

            return ShouldStartNewAndKeep(mostRecentChar, currentChar) ? words.Concat(currentChar.ToString()).ToArray() :
                   ShoutStartNewAndDiscard(mostRecentChar, currentChar) ? words.Concat("").ToArray() : 
                   AddCharToMostRecentWord(words, currentChar);
        }

        private static string[] AddCharToMostRecentWord(string[] words, char currentChar)
        {
            if (words.Length == 0)
                words = new[] { "" };
            words[words.Length- 1] = string.Concat(words.LastOrDefault() ?? "", currentChar);
            return words;
        }

        public static char GetMostRecentChar(string[] words)
        {
            return words.FirstOrDefault()?.FirstOrDefault() ?? '_';
        }

        public static bool ShouldStartNewAndKeep(char prev, char cur)
        {
            return char.IsUpper(cur) && !char.IsUpper(prev);
        }

        public static bool ShoutStartNewAndDiscard(char prev, char cur)
        {
            return cur == '_' || (char.IsNumber(cur) && !char.IsNumber(prev));
        }
    }
}
