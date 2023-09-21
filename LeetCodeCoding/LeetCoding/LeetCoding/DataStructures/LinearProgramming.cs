namespace LeetCoding.DataStructures
{
    public class LinearProgramming
    {
        public string LongestPal(string s)
        {
            char[] charArr = s.ToCharArray();
            string finalString = string.Empty;

            if (s.Length == 0 || s.Length == 1)
                return s;

            for (int i = 0; i < charArr.Length; i++)
            {
                string iString = charArr[i].ToString();
                string jString = string.Empty;
                for (int j = i + 1; j < charArr.Length; j++)
                {
                    if (j != 0)
                        jString += charArr[j].ToString();

                    string reversedString = string.Join("", (iString + jString).ToCharArray().Reverse());

                    if (reversedString == iString + jString)
                        if (reversedString.Length > finalString.Length)
                            finalString = reversedString;
                }
            }

            if (finalString.Length == 0 && s.Length > 0)
                return s.ToCharArray()[0].ToString();

            return finalString;
        }

        public bool ContainsDuplicate(int[] nums) =>
            nums.ToHashSet().Count != nums.Length;

        //Contains checks if the Reference is the same and not for Structure.
        public bool IsAnagramMine(string s, string t) =>
            s.Length == t.Length && s.GroupBy(s => s).Where(sGroup => t.GroupBy(t => t).ToList().Contains(sGroup)).Count() == s.ToHashSet().Count();

        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;

            var sGroups = s.GroupBy(s => s).ToDictionary(group => group.Key, group => group.Count());
            var tGroups = t.GroupBy(t => t).ToDictionary(group => group.Key, group => group.Count());

            return sGroups.Count == tGroups.Count && !sGroups.Except(tGroups).Any();
        }

        public bool IsAnagramOptimal(string s, string t)
        {
            if (s.Length != t.Length) return false;

            var count = new int[26];
            for (int i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a']++;
                count[t[i] - 'a']--;
            }

            return count.All(c => c == 0);
        }

        public int[] TopKFrequentV1(int[] nums, int k) =>
            nums.GroupBy(num => num).OrderByDescending(num => num.Count()).Take(k).Select(c => c.Key).ToArray();

        public int[] TopKFrequent(int[] nums, int k)
        {
            if (nums.Length == 1 || nums.Length == 0)
                return nums;

            Dictionary<int, int> numsCount = new();

            foreach (var numList in nums.GroupBy(nums => nums))
                numsCount.Add(numList.Key, numList.Count());

            List<int> sortedList = numsCount.Values.OrderByDescending(nums => nums).ToList();

            return numsCount.Where(numsCount => sortedList.Take(k).Contains(numsCount.Value)).Select(pair => pair.Key).ToArray();
        }

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            if (strs.Length == 0 || strs.Length == 1)
                return new List<IList<string>>() { strs };

            List<IList<string>> anagramList = new();

            ICollection<string> anagramHolder = new List<string>();

            for (int i = 0; i < strs.Length; i++)
            {
                anagramHolder.Clear();
                for (int j = 0; j < strs.Length; j++)
                {
                    if (IsAnagramOptimal(strs[i], strs[j]) && j != i)
                        anagramHolder.Add(strs[j]);
                }

                anagramHolder.Add(strs[i]);
                anagramList.Add(anagramHolder.ToList());
            }

            for (int i = 0; i < anagramList.Count(); i++)
                for (int j = i + 1; j < anagramList.Count(); j++)
                {
                    if (anagramList[i].Except(anagramList[j]).ToList().Count() == 0)
                    {
                        anagramList.RemoveAt(j);
                        j -= 1;
                    }
                }

            return anagramList;
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            List<int> numList = nums.ToList();
            int[] trackNums = new int[nums.Length];
            int trackSumInt = 0;

            return nums.Select((i, index) =>
            {
                numList.RemoveAt(index);
                trackSumInt = numList.Aggregate((total, next) => total * next);
                numList.Insert(index, nums[index]);
                return trackSumInt;
            }).ToArray();
        }

        public int[] ProductExceptSelfTest(int[] nums)
        {
            int index = 0;
            int factorTracker;
            int[] finalIntArry = new int[nums.Length];
            for (int i = 0, j = nums.Length; i < nums.Length; i++)
            {
            }

            return finalIntArry;
        }

        //public static int LongestConsecutive(int[] nums)
        //{
        //    List<int> list = nums.ToList();
        //    list.Sort();

        //    list.Aggregate((total, next) => total + next);
        //}

        public bool IsPalindrome(string s)
        {
            string regexString = new string(s.Where(c => char.IsLetterOrDigit(c)).Select(c => char.ToLower(c)).ToArray()).ToLower();

            return string.Join("", regexString.ToCharArray().Reverse()) == string.Join("", regexString);
        }

        public bool CanArrange(int[] arr, int k)
        {
            if (arr.Length < 2) return false;

            List<List<int>> arrangements = new();

            BuildCanArrange(arr.ToList(), arrangements, k);

            if (arrangements.Count == Math.Floor(Convert.ToDecimal(arr.Length / 2)))
                return true;

            return false;
        }

        private void BuildCanArrange(List<int> intList, List<List<int>> arrangements, int k)
        {
            for (int i = 0; i < intList.Count; i++)
                for (int j = i + 1; j < intList.Count; j++)
                    if ((intList[j] + intList[i]) % k == 0)
                    {
                        arrangements.Add(new List<int>() { intList[j], intList[i] });
                        intList.RemoveAt(j);
                        intList.RemoveAt(i);

                        BuildCanArrange(intList, arrangements, k);
                    }
        }

        public int MaxProfit(int[] prices)
        {
            if (prices.Length < 2)
                return 0;

            int minPrice = prices[0];
            int maxProfit = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] < minPrice)
                    minPrice = prices[i];
                else if (prices[i] - minPrice > maxProfit)
                    maxProfit = prices[i] - minPrice;
            }

            return maxProfit;
        }

        public int LengthOfLongestSubstring(string s)
        {
            if (s.Length == 0) return 0;

            string finalSubString = string.Empty;
            string tempSubString = string.Empty;

            for (int i = 0; i < s.Length; i++)
            {
                if (s.Length - i < finalSubString.Length)
                    return finalSubString.Length;
                tempSubString = string.Empty;
                for (int j = i; j < s.Length; j++)
                {
                    tempSubString += s[j];
                    if (ValidateDuplicates(tempSubString))
                    {
                        if (tempSubString.Length > finalSubString.Length)
                            finalSubString = tempSubString;
                    }
                    else
                    {
                        tempSubString = string.Empty;
                        continue;
                    }
                }
            }

            return finalSubString.Length;
        }

        private bool ValidateDuplicates(string subString) =>
            subString.ToHashSet().Count == subString.Length;
    }
}