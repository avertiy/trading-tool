using System;
using System.Collections.Generic;
using System.Linq;

namespace AVS.BinanceApi.Services
{
    public class BinancePairUsageService
    {
        private readonly Dictionary<string, int> _dict = new Dictionary<string, int>();

        public DateTime DueDate => DateTime.Now.AddDays(-45);

        public List<string> GetRecentPairs(int minTradesCount = 0)
        {
            var pairs = _dict.Where(x=> x.Value >= minTradesCount).OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            return pairs;
        }

        public void Update(string pair, int count)
        {
            if(!_dict.ContainsKey(pair))
                _dict.Add(pair, count);
            else
                _dict[pair] = count;
        }

        public void Init(string[] pairs)
        {
            foreach (var pair in pairs)
                _dict.Add(pair, 0);
        }
    }
}