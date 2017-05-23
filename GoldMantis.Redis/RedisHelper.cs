using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;

namespace GoldMantis.Service.Biz.Redis
{

    public class RedisHelper : IDisposable
    {
        private static RedisConfigInfo redisConfigInfo = RedisConfigInfo.GetConfig();
        private static PooledRedisClientManager prcm = CreateManager();


        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static PooledRedisClientManager CreateManager()
        {
            string[] writeServerList = SplitString(redisConfigInfo.WriteServerList, ";");
            string[] readServerList = SplitString(redisConfigInfo.ReadServerList, ";");

            // 支持读写分离，均衡负载 
            return  new PooledRedisClientManager(readServerList, writeServerList,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = redisConfigInfo.MaxWritePoolSize,
                                 MaxReadPoolSize = redisConfigInfo.MaxReadPoolSize,
                                 AutoStart = redisConfigInfo.AutoStart,
                                 
                             });
        }

        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }


        #region 暂时用不到的redis方法
        ///// <summary>
        ///// 获取key,返回string格式
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static string getValueString(string key)
        //{

        //    string value = redisCli.GetValue(key);
        //    return value;


        //}
        ///// <summary>
        ///// 获得某个hash型key下的所有字段
        ///// </summary>
        ///// <param name="hashId"></param>
        ///// <returns></returns>
        //public static List<string> GetHashFields(string hashId)
        //{
        //    List<string> hashFields = redisCli.GetHashKeys(hashId);
        //    return hashFields;
        //}
        ///// <summary>
        ///// 获得某个hash型key下的所有值
        ///// </summary>
        ///// <param name="hashId"></param>
        ///// <returns></returns>
        //public static List<string> GetHashValues(string hashId)
        //{
        //    List<string> hashValues = redisCli.GetHashKeys(hashId);
        //    return hashValues;
        //}
        ///// <summary>
        ///// 获得hash型key某个字段的值
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="field"></param>
        //public static string GetHashField(string key, string field)
        //{
        //    string value = redisCli.GetValueFromHash(key, field);
        //    return value;
        //}
        ///// <summary>
        ///// 设置hash型key某个字段的值
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        //public static void SetHashField(string key, string field, string value)
        //{
        //    redisCli.SetEntryInHash(key, field, value);
        //}
        ///// <summary>
        /////使某个字段增加
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="field"></param>
        ///// <returns></returns>
        //public static void SetHashIncr(string key, string field, int incre)
        //{
        //    redisCli.IncrementValueInHash(key, field, incre);

        //}
        ///// <summary>
        ///// 向list类型数据添加成员，向列表底部(右侧)添加
        ///// </summary>
        ///// <param name="Item"></param>
        ///// <param name="list"></param>
        //public static void AddItemToListRight(string list, string item)
        //{
        //    redisCli.AddItemToList(list, item);
        //}
        ///// <summary>
        ///// 从list类型数据读取所有成员
        ///// </summary>
        //public static List<string> GetAllItems(string list)
        //{
        //    List<string> listMembers = redisCli.GetAllItemsFromList(list);
        //    return listMembers;
        //}
        ///// <summary>
        ///// 从list类型数据指定索引处获取数据，支持正索引和负索引
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public static string GetItemFromList(string list, int index)
        //{
        //    string item = redisCli.GetItemFromList(list, index);
        //    return item;
        //}
        ///// <summary>
        ///// 向列表底部（右侧）批量添加数据
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="values"></param>
        //public static void GetRangeToList(string list, List<string> values)
        //{
        //    redisCli.AddRangeToList(list, values);
        //}
        ///// <summary>
        ///// 向集合中添加数据
        ///// </summary>
        ///// <param name="item"></param>
        ///// <param name="set"></param>
        //public static void GetItemToSet(string item, string set)
        //{
        //    redisCli.AddItemToSet(item, set);
        //}
        ///// <summary>
        ///// 获得集合中所有数据
        ///// </summary>
        ///// <param name="set"></param>
        ///// <returns></returns>
        //public static HashSet<string> GetAllItemsFromSet(string set)
        //{
        //    HashSet<string> items = redisCli.GetAllItemsFromSet(set);
        //    return items;
        //}
        ///// <summary>
        ///// 获取fromSet集合和其他集合不同的数据
        ///// </summary>
        ///// <param name="fromSet"></param>
        ///// <param name="toSet"></param>
        ///// <returns></returns>
        //public static HashSet<string> GetSetDiff(string fromSet, params string[] toSet)
        //{
        //    HashSet<string> diff = redisCli.GetDifferencesFromSet(fromSet, toSet);
        //    return diff;
        //}
        ///// <summary>
        ///// 获得所有集合的并集
        ///// </summary>
        ///// <param name="set"></param>
        ///// <returns></returns>
        //public static HashSet<string> GetSetUnion(params string[] set)
        //{
        //    HashSet<string> union = redisCli.GetUnionFromSets(set);
        //    return union;
        //}
        ///// <summary>
        ///// 获得所有集合的交集
        ///// </summary>
        ///// <param name="set"></param>
        ///// <returns></returns>
        //public static HashSet<string> GetSetInter(params string[] set)
        //{
        //    HashSet<string> inter = redisCli.GetIntersectFromSets(set);
        //    return inter;
        //}
        ///// <summary>
        ///// 向有序集合中添加元素
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <param name="score"></param>
        //public static void AddItemToSortedSet(string set, string value, long score)
        //{
        //    redisCli.AddItemToSortedSet(set, value, score);
        //}
        ///// <summary>
        ///// 获得某个值在有序集合中的排名，按分数的降序排列
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static long GetItemIndexInSortedSetDesc(string set, string value)
        //{
        //    long index = redisCli.GetItemIndexInSortedSetDesc(set, value);
        //    return index;
        //}
        ///// <summary>
        ///// 获得某个值在有序集合中的排名，按分数的升序排列
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static long GetItemIndexInSortedSet(string set, string value)
        //{
        //    long index = redisCli.GetItemIndexInSortedSet(set, value);
        //    return index;
        //}
        ///// <summary>
        ///// 获得有序集合中某个值得分数
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static double GetItemScoreInSortedSet(string set, string value)
        //{
        //    double score = redisCli.GetItemScoreInSortedSet(set, value);
        //    return score;
        //}
        ///// <summary>
        ///// 获得有序集合中，某个排名范围的所有值
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="beginRank"></param>
        ///// <param name="endRank"></param>
        ///// <returns></returns>
        //public static List<string> GetRangeFromSortedSet(string set, int beginRank, int endRank)
        //{
        //    List<string> valueList = redisCli.GetRangeFromSortedSet(set, beginRank, endRank);
        //    return valueList;
        //}
        ///// <summary>
        ///// 获得有序集合中，某个分数范围内的所有值，升序
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="beginScore"></param>
        ///// <param name="endScore"></param>
        ///// <returns></returns>
        //public static List<string> GetRangeFromSortedSet(string set, double beginScore, double endScore)
        //{
        //    List<string> valueList = redisCli.GetRangeFromSortedSetByHighestScore(set, beginScore, endScore);
        //    return valueList;
        //}
        ///// <summary>
        ///// 获得有序集合中，某个分数范围内的所有值，降序
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="beginScore"></param>
        ///// <param name="endScore"></param>
        ///// <returns></returns>
        //public static List<string> GetRangeFromSortedSetDesc(string set, double beginScore, double endScore)
        //{
        //    List<string> vlaueList = redisCli.GetRangeFromSortedSetByLowestScore(set, beginScore, endScore);
        //    return vlaueList;
        //}
        #endregion

        public static void Hash_SetExpire(string key, DateTime datetime)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.ExpireEntryAt(key, datetime);
            }
        }

        public static int Hash_GetCount(string key)
        {
            using (IRedisClient redis = prcm.GetReadOnlyClient())
            {
                return redis.GetHashCount(key);
            }
        }

        public static bool Set<T>(string key, T t, int timeout)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Set(key, t, new TimeSpan(0, timeout, 0));
            }

        }

        public static T Get<T>(string key) where T : class
        {
            using (IRedisClient redis = prcm.GetReadOnlyClient())
            {
                return redis.Get<T>(key);
            }
        }

        public static bool SetExpire(string key, int timeout)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.ExpireEntryIn(key, new TimeSpan(0, timeout, 0));
            }
        }

        public static bool Remove(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Remove(key);
            }
        }

        /// <summary>
        /// 存储整张列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="list"></param>
        public static void SetList<T>(string key, IList<T> list)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.Set(key, list, DateTime.Now.AddSeconds(redisConfigInfo.LocalCacheTime));
            }
        }
        /// <summary>
        /// 根据key获取整张列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IList<T> GetList<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Get<IList<T>>(key);
            }
        }

        public void Dispose()
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.Dispose();
            }
        }

    }

}
