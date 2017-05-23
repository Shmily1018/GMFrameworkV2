using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using GoldMantis.Service.Biz.Redis;
using Newtonsoft.Json;

namespace GoldMantis.Web.Core.Session
{
    public class RedisSessionStateStore : SessionStateStoreProviderBase
    {
        public override void Dispose()
        {
            
        }

        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return true;
        }

        public override void InitializeRequest(HttpContext context)
        {
           
        }

        public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId,
            out SessionStateActions actions)
        {
            return this.DoGet(context, id, false, out locked, out lockAge, out lockId, out actions);
        }

        public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge,
            out object lockId, out SessionStateActions actions)
        {
            return this.DoGet(context, id, true, out locked, out lockAge, out lockId, out actions);
        }

        public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
        {
            
        }

        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {
            ISessionStateItemCollection sessionItems = null;
            HttpStaticObjectsCollection staticObjects = null;

            if (item.Items.Count > 0)
                sessionItems = item.Items;
            if (!item.StaticObjects.NeverAccessed)
                staticObjects = item.StaticObjects;

            RedisSessionState state2 = new RedisSessionState(sessionItems, staticObjects, item.Timeout);

            RedisHelper.Set(id, state2.ToJson(), item.Timeout);
        }

        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            RedisHelper.Remove(id);
        }

        public override void ResetItemTimeout(HttpContext context, string id)
        {
           
        }

        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            return CreateLegitStoreData(context, null, null, timeout);
        }

        internal static SessionStateStoreData CreateLegitStoreData(HttpContext context, ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
        {
            if (sessionItems == null)
                sessionItems = new SessionStateItemCollection();
            if (staticObjects == null && context != null)
                staticObjects = SessionStateUtility.GetSessionStaticObjects(context);
            return new SessionStateStoreData(sessionItems, staticObjects, timeout);
        }

        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {
            RedisSessionState state = new RedisSessionState(null, null, timeout);
            RedisHelper.Set<string>(id, state.ToJson(), timeout);
        }

        private SessionStateStoreData DoGet(HttpContext context, string id, bool exclusive, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
        {
            locked = false;
            lockId = null;
            lockAge = TimeSpan.Zero;
            actionFlags = SessionStateActions.None;
            RedisSessionState state = RedisSessionState.FromJson(RedisHelper.Get<string>(id));
            if (state == null)
            {
                return null;
            }
            RedisHelper.SetExpire(id, state.timeout);
            return CreateLegitStoreData(context, state.sessionItems, state.staticObjects, state.timeout);
        }

        public override void EndRequest(HttpContext context)
        {
            
        }

    }


    public sealed class SessionStateItem
    {
        public Dictionary<string, object> Dict;
        public int Timeout;
    }

    public sealed class RedisSessionState
    {
        public ISessionStateItemCollection sessionItems;
        public HttpStaticObjectsCollection staticObjects;
        internal int timeout;

        internal RedisSessionState(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
        {
            this.sessionItems = sessionItems;
            this.staticObjects = staticObjects;
            this.timeout = timeout;
        }

        public string ToJson()
        {
            if (sessionItems == null || sessionItems.Count == 0)
            {
                return null;
            }

            var dict = new Dictionary<string, object>(sessionItems.Count);
            string key;

            var keys = sessionItems.Keys;
            for (int i = 0; i < keys.Count; i++)
            {
                key = keys[i];
                dict.Add(key, sessionItems[key]);
            }

            var item = new SessionStateItem { Dict = dict, Timeout = this.timeout };

            return JsonConvert.SerializeObject(item);
        }

        public static RedisSessionState FromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            try
            {
                var item = JsonConvert.DeserializeObject<SessionStateItem>(json);
                var collections = new SessionStateItemCollection();

                foreach (var kvp in item.Dict)
                {
                    collections[kvp.Key] = kvp.Value;
                }

                return new RedisSessionState(collections, null, item.Timeout);
            }
            catch
            {
                return null;
            }
        }
    }
}