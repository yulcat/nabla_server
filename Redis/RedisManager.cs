using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using webAPI.Containers;

namespace webAPI.Redis
{
    public class RedisManager
    {
        static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1");
        public static void AddScore(string key, string id, long score)
        {
            IDatabase db = redis.GetDatabase();
            db.SortedSetIncrement(key, id, score);
        }
        public static long GetRank(string key, string id)
        {
            IDatabase db = redis.GetDatabase();
            long? rank = db.SortedSetRank(key, id, Order.Descending);
            if (rank.HasValue)  
            {  
                return rank.Value;  
            }
            return -1;
        }
        public static long GetCount(string key)
        {
            IDatabase db = redis.GetDatabase();
            return db.SortedSetLength(key);
        }
        public static double GetPercentage(string key, string id)
        {
            return (float)GetRank(key, id)/GetCount(key);
        }
        public static IEnumerable<Score> GetRange(string key)
        {
            IDatabase db = redis.GetDatabase();
            return db.SortedSetScan(key).Select(entry => EntryToScore(entry,key));
        }
        static Score EntryToScore(SortedSetEntry entry, string key)
        {
            var score = new Score();
            score.stage = key;
            score.id = entry.Element;
            score.score = (long)entry.Score;
            return score;
        }
    }
}