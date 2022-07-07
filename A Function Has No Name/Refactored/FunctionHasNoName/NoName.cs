using System.Linq;

namespace FunctionHasNoName
{
    public static class NoName
    {
        private static bool IsOdd(int i) => i % 2 == 1;

        private static (string middleCharacter, int beforeMiddleCharacterIndex, int afterMiddleCharacterIndex) GetMiddleInformations(string input, int index)
            => IsOdd(index) switch
            {
                true => (input[index / 2].ToString(), index / 2 - 1, index / 2 + 1),
                false => ("", index / 2 - 1, index / 2),
            };

        private static string ChooseBestPalindrome(string currentPalindrome, string bestPalindromeSoFar) =>
            currentPalindrome.Length > bestPalindromeSoFar.Length ? currentPalindrome : bestPalindromeSoFar;
        
        public static string FirstLongestPalindrome(string input)
        {
            if (input is null) return null;
            
            var bestPalindromeSoFar = string.IsNullOrEmpty(input) ? string.Empty : input.First().ToString();
            
            for (var i = 1; i < 2 * input.Length; i++)
            {
                var (middlePart, beforeMiddlePartIndex, afterMiddlePartIndex) = GetMiddleInformations(input, i);
                
                var currentPalindrome = middlePart;

                while (beforeMiddlePartIndex > -1 && afterMiddlePartIndex < input.Length)
                {
                    if (input[beforeMiddlePartIndex] == input[afterMiddlePartIndex])
                    {
                        currentPalindrome = input[beforeMiddlePartIndex] + currentPalindrome + input[afterMiddlePartIndex];
                        beforeMiddlePartIndex -= 1;
                        afterMiddlePartIndex += 1;

                        bestPalindromeSoFar  = ChooseBestPalindrome(currentPalindrome, bestPalindromeSoFar);
                    }
                    else break;
                }
            }

            return bestPalindromeSoFar;
        }
    }
}
