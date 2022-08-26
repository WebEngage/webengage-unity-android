using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebEngageBridge
{
    public delegate void callback(string pushData);
#if (UNITY_ANDROID)
    public sealed class WEPushNotificationCallback: AndroidJavaProxy {
        private callback recCallbackObj = null;
        private callback clickCallbackObj = null;
        public static WEPushNotificationCallback instance = null;
        private static readonly object padlock = new object();

        public static WEPushNotificationCallback Instance
          {
             get
            {
             lock (padlock)
                {
                 if (instance == null)
                 {
                    instance = new WEPushNotificationCallback();
                 }
                 return instance;
                }
            }
         }

        public void setPushReceivedCallBackObj(callback obj) {
            recCallbackObj = obj;
        }

        public void setPushClickCallBackObj(callback obj) {
            clickCallbackObj = obj;
        }


        private WEPushNotificationCallback() : base("com.webengage.sdk.android.unity.WEUnityPushCallbacks") {
            Debug.Log("WEPushNotificationCallback");
            new AndroidJavaObject("com.webengage.sdk.android.unity.WEUnityCallbacksPushImpl", this);
        }

        public string onPushNotificationReceived(string jsonString) {
            Debug.Log("WEPushNotificationCallback onPushNotificationReceived: " + jsonString);
            if (recCallbackObj != null) {
                recCallbackObj(jsonString);
            }
            return jsonString;
        }

        public bool onPushNotificationClicked(string jsonString) {
            Debug.Log("WEPushNotificationCallback onPushNotificationClicked: " + jsonString);
            if (clickCallbackObj != null) {
                clickCallbackObj(jsonString);
                return true;
            }
            return false;
        }
    }
#endif

#if (UNITY_ANDROID)
    public class WEInAppNotificationCallback: AndroidJavaProxy {
        private callback callbackObjPrepared = null;
        private callback callbackObjShown = null;
        private callback callbackObjClicked = null;
        private callback callbackObjDismissed = null;
        public static WEInAppNotificationCallback instance = null;
        private static readonly object padlock2 = new object();

        public static WEInAppNotificationCallback Instance
          {
             get
            {
             lock (padlock2)
                {
                 if (instance == null)
                 {
                    instance = new WEInAppNotificationCallback();
                 }
                 return instance;
                }
            }
         }


        public void setInAppPrepared(callback obj) {
            callbackObjPrepared = obj;
        }

        public void setInAppDismissed(callback obj) {
            callbackObjDismissed = obj;
        }
        public void setInAppShown(callback obj) {
            callbackObjShown = obj;
        }
        public void setInAppClicked(callback obj) {
            callbackObjClicked = obj;
        }

        private WEInAppNotificationCallback() : base("com.webengage.sdk.android.unity.WEUnityInAppCallbacks") {
            new AndroidJavaObject("com.webengage.sdk.android.unity.WEUnityCallbacksInAppImpl", this);
        }

        public string onInAppNotificationPrepared(string jsonString) {
            Debug.Log("WEInAppNotificationCallback onInAppNotificationPrepared: " + jsonString);
            if (callbackObjPrepared != null) {
                callbackObjPrepared(jsonString);
            }
            return jsonString;
        }

        public void onInAppNotificationShown(string jsonString) {
            Debug.Log("WEInAppNotificationCallback onInAppNotificationShown: " + jsonString);
            if (callbackObjShown != null) {
                callbackObjShown(jsonString);
            }
        }

         public bool onInAppNotificationClicked(string jsonString) {
            Debug.Log("WEInAppNotificationCallback onInAppNotificationClicked: " + jsonString);
            if (callbackObjClicked != null) {
                callbackObjClicked(jsonString);
                return true;
            }
            return false;
        }

         public void onInAppNotificationDismissed(string jsonString) {
            Debug.Log("WEInAppNotificationCallback onInAppNotificationDismissed: " + jsonString);
            if (callbackObjDismissed != null) {
                callbackObjDismissed(jsonString);
            }
        }
    }
    
#endif

    public class WebEngage
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        const string webengageClassPath = "com.webengage.sdk.android.WebEngage";
        private static AndroidJavaClass webEngageClass = null;

        private static AndroidJavaClass GetWebEngageClass()
        {
            if (webEngageClass == null)
            {
                webEngageClass = new AndroidJavaClass(webengageClassPath);
            }
            return webEngageClass;
        }

        public static AndroidJavaObject GetWebEngage()
        {
            return GetWebEngageClass().CallStatic<AndroidJavaObject>("get");
        }

        private static AndroidJavaObject GetAnalytics()
        {
            return GetWebEngage().Call<AndroidJavaObject>("analytics");
        }

        private static AndroidJavaObject GetUser()
        {
            return GetWebEngage().Call<AndroidJavaObject>("user");
        }
#endif

         public static void setPushClickCallBack(callback obj)
        {
            Debug.Log("setPushClickCallBack");
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //pushClickCallBack(obj);
            } else if (Application.platform == RuntimePlatform.Android){
                var call = WEPushNotificationCallback.Instance;
                call.setPushClickCallBackObj(obj);
            }
        }

        public static void setPushReceivedCallBack(callback obj)
        {
            Debug.Log("setPushReceivedCallBack");
            if (Application.platform == RuntimePlatform.Android){
                 var call = WEPushNotificationCallback.Instance;
                call.setPushReceivedCallBackObj(obj);
            }
        }

        public static void setInAppPreparedCallBack(callback obj)
        {
                        Debug.Log("setInAppPreparedCallBack");

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
               // InAppPreparedCallBack(obj);
            } else if (Application.platform == RuntimePlatform.Android){
                var inApp = WEInAppNotificationCallback.Instance;
                inApp.setInAppPrepared(obj);
            }
        }
        public static void setInAppShownCallBack(callback obj)
        {
                        Debug.Log("setInAppShownCallBack");

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
               // InAppShownCallBack(obj);
            } else if (Application.platform == RuntimePlatform.Android){
                 var inApp = WEInAppNotificationCallback.Instance;
                inApp.setInAppShown(obj);
            }
        }

        public static void setInAppClickedCallBack(callback obj)
        {
            Debug.Log("setInAppClickedCallBack");
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //InAppCLickedCallBack(obj);
            } else if (Application.platform == RuntimePlatform.Android){
                var inApp = WEInAppNotificationCallback.Instance;
                inApp.setInAppClicked(obj);
            }
        }
        public static void setInAppDismissedCallBack(callback obj)
        {
                        Debug.Log("setInAppDismissedCallBack");

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
               // InAppDismissedCallBack(obj);
            } else if (Application.platform == RuntimePlatform.Android){
                var inApp = WEInAppNotificationCallback.Instance;
                inApp.setInAppDismissed(obj);
            }
        }

        public static void Engage()
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

            GetWebEngageClass().CallStatic("engage", context);

            var x = new AndroidJavaObject("com.webengage.sdk.android.WebEngageActivityLifeCycleCallbacks", activity);
#endif
        }

        public static void Engage(string licenseCode, bool isDebug)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaObject configBuilder = new AndroidJavaObject("com.webengage.sdk.android.WebEngageConfig$Builder");
            configBuilder = configBuilder.Call<AndroidJavaObject>("setWebEngageKey", licenseCode);
            configBuilder = configBuilder.Call<AndroidJavaObject>("setDebugMode", isDebug);
            AndroidJavaObject config = configBuilder.Call<AndroidJavaObject>("build");

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

            GetWebEngageClass().CallStatic("engage", context, config);
#endif
        }

        public static void Start()
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            GetAnalytics().Call("start", activity);
#endif
        }

        public static void Stop()
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            GetAnalytics().Call("stop", activity);
#endif
        }

        // Tracking events
        public static void TrackEvent(string eventName)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetAnalytics().Call("track", eventName);
#endif
        }

#if (UNITY_ANDROID && !UNITY_EDITOR)
        private static AndroidJavaObject GetJavaObject(object val)
        {
            if (val is string)
            {
                AndroidJavaClass strClass = new AndroidJavaClass("java.lang.String");
                AndroidJavaObject strObj = strClass.CallStatic<AndroidJavaObject>("valueOf", val);
                return strObj;
            }
            else if (val is bool)
            {
                var booleanVal = new AndroidJavaObject("java.lang.Boolean", (bool) val);
                return booleanVal;
            }
            else if (val is int)
            {
                var integerVal = new AndroidJavaObject("java.lang.Integer", (int) val);
                return integerVal;
            }
            else if (val is long)
            {
                var longVal = new AndroidJavaObject("java.lang.Long", (long) val);
                return longVal;
            }
            else if (val is double)
            {
                var doubleVal = new AndroidJavaObject("java.lang.Double", (double) val);
                return doubleVal;
            }
            else if (val is float)
            {
                var floatVal = new AndroidJavaObject("java.lang.Float", (float) val);
                return floatVal;
            }
            else if (val is System.DateTime)
            {
                string strDate = ((System.DateTime) val).ToString("yyyy-MM-dd HH:mm:ss.fff");
                try
                {
                    var format = new AndroidJavaObject("java.text.SimpleDateFormat", "yyyy-MM-dd HH:mm:ss.SSS");
                    var date = format.Call<AndroidJavaObject>("parse", strDate);
                    return date;
                }
                catch (System.Exception e)
                {
                    Debug.LogError("WebEngageBridge: Exception while parsing date object: " + strDate + ", " + e);
                }
                return GetJavaObject(strDate);
            }
            else if (val is Dictionary<string, string>)
            {
                return GetHashMapString((Dictionary<string, string>) val);
            }
            else if (val is Dictionary<string, object>)
            {
                return GetHashMap((Dictionary<string, object>) val);
            }
            else if (val is List<string>)
            {
                return GetArrayListString((List<string>) val);
            }
            else if (val is List<object>)
            {
                return GetArrayList((List<object>) val);
            }
            else
            {
                if (val != null)
                {
                    string str = val.ToString();
                    AndroidJavaClass strClass = new AndroidJavaClass("java.lang.String");
                    AndroidJavaObject strObj = strClass.CallStatic<AndroidJavaObject>("valueOf", str);
                    return strObj;
                }
                else
                {
                    return null;
                }
            }
        }

        private static AndroidJavaObject GetHashMap(Dictionary<string, object> dict)
        {
            AndroidJavaObject hashMap = new AndroidJavaObject("java.util.HashMap");
            var put = AndroidJNIHelper.GetMethodID(hashMap.GetRawClass(), "put", "(Ljava/lang/String;Ljava/lang/Object;)Ljava/lang/Object;");

            foreach (KeyValuePair<string, object> entry in dict)
            {
                AndroidJavaObject javaObject = GetJavaObject(entry.Value);
                AndroidJNI.CallObjectMethod(hashMap.GetRawObject(), put, AndroidJNIHelper.CreateJNIArgArray(new object[] { entry.Key, javaObject }));
            }
            return hashMap;
        }

        private static AndroidJavaObject GetHashMapString(Dictionary<string, string> dict)
        {
            AndroidJavaObject hashMap = new AndroidJavaObject("java.util.HashMap");
            var put = AndroidJNIHelper.GetMethodID(hashMap.GetRawClass(), "put", "(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;");

            foreach (KeyValuePair<string, string> entry in dict)
            {
                AndroidJNI.CallObjectMethod(hashMap.GetRawObject(), put, AndroidJNIHelper.CreateJNIArgArray(new object[] { entry.Key, entry.Value }));
            }
            return hashMap;
        }

        private static AndroidJavaObject GetArrayList(List<object> list)
        {
            AndroidJavaObject arrayList = new AndroidJavaObject("java.util.ArrayList");
            var add = AndroidJNIHelper.GetMethodID(arrayList.GetRawClass(), "add", "(Ljava/lang/Object;)Z;", false);

            foreach (object val in list)
            {
                AndroidJavaObject javaObject = GetJavaObject(val);
                AndroidJNI.CallBooleanMethod(arrayList.GetRawObject(), add, AndroidJNIHelper.CreateJNIArgArray(new object[] { javaObject }));
            }
            return arrayList;
        }

        private static AndroidJavaObject GetArrayListString(List<string> list)
        {
            AndroidJavaObject arrayList = new AndroidJavaObject("java.util.ArrayList");
            var add = AndroidJNIHelper.GetMethodID(arrayList.GetRawClass(), "add", "(Ljava/lang/String;)Z;", false);
            foreach (string val in list)
            {
                AndroidJNI.CallBooleanMethod(arrayList.GetRawObject(), add, AndroidJNIHelper.CreateJNIArgArray(new object[] { val }));
            }
            return arrayList;
        }
#endif

        public static void TrackEvent(string eventName, Dictionary<string, object> attributes)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            var attr = GetHashMap(attributes);
            GetAnalytics().Call("track", eventName, attr);
#endif
        }

        // Configurations
#if (UNITY_ANDROID && !UNITY_EDITOR)
        private const string eventReportingStrategyClassPath = "com.webengage.sdk.android.actions.database.ReportingStrategy";
        private const string locationTrackingStrategyClassPath = "com.webengage.sdk.android.LocationTrackingStrategy";
        private const string genderClassPath = "com.webengage.sdk.android.utils.Gender";
        private const string channelClassPath = "com.webengage.sdk.android.Channel";

        private static AndroidJavaObject GetEnum(string classPath, string enumName)
        {
            AndroidJavaClass enumClass = new AndroidJavaClass(classPath);
            return enumClass.GetStatic<AndroidJavaObject>(enumName);
        }
#endif

        public static void SetEventReportingStrategy(string strategy)
        {
            if ("buffer".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetWebEngage().Call("setEventReportingStrategy", GetEnum(eventReportingStrategyClassPath, "BUFFER"));
#endif
            }
            else if ("force_sync".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetWebEngage().Call("setEventReportingStrategy", GetEnum(eventReportingStrategyClassPath, "FORCE_SYNC"));
#endif
            }
            else
            {
                Debug.LogError("WebEngageBridge: Invalid event reporting strategy: " + strategy + ". Must be one of [buffer, force_sync]");
            }
        }

        public static void SetLocationTrackingStrategy(string strategy)
        {
            if ("accuracy_best".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetWebEngage().Call("setLocationTrackingStrategy", GetEnum(locationTrackingStrategyClassPath, "ACCURACY_BEST"));
#endif
            }
            else if ("accuracy_city".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetWebEngage().Call("setLocationTrackingStrategy", GetEnum(locationTrackingStrategyClassPath, "ACCURACY_CITY"));
#endif
            }
            else if ("accuracy_country".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetWebEngage().Call("setLocationTrackingStrategy", GetEnum(locationTrackingStrategyClassPath, "ACCURACY_COUNTRY"));
#endif
            }
            else if ("disabled".Equals(strategy, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetWebEngage().Call("setLocationTrackingStrategy", GetEnum(locationTrackingStrategyClassPath, "DISABLED"));
#endif
            }
            else
            {
                Debug.LogError("WebEngageBridge: Invalid location tracking strategy: " + strategy + ". Must be one of [accuracy_best, accuracy_city, accuracy_country, disabled]");
            }
        }

        // Tracking Users
        public static void Login(string cuid)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("login", cuid);
#endif
        }

        public static void Logout()
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("logout");
#endif
        }

        public static void SetFirstName(string firstName)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setFirstName", firstName);
#endif
        }

        public static void SetLastName(string lastName)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setLastName", lastName);
#endif
        }

        public static void SetEmail(string email)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setEmail", email);
#endif
        }

        public static void SetHashedEmail(string hashedEmail)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setHashedEmail", hashedEmail);
#endif
        }

        public static void SetPhoneNumber(string phone)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setPhoneNumber", phone);
#endif
        }

        public static void SetHashedPhoneNumber(string hashedPhone)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setHashedPhoneNumber", hashedPhone);
#endif
        }

        public static void SetCompany(string company)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setCompany", company);
#endif
        }

        public static void SetBirthDate(string birthDate)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setBirthDate", birthDate);
#endif
        }

        public static void SetLocation(double latitude, double longitude)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setLocation", latitude, longitude);
#endif
        }

        public static void SetGender(string gender)
        {
            if ("male".Equals(gender, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetUser().Call("setGender", GetEnum(genderClassPath, "MALE"));
#endif
            }
            else if (string.Compare(gender, "female", true) == 0)
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetUser().Call("setGender", GetEnum(genderClassPath, "FEMALE"));
#endif
            }
            else if (string.Compare(gender, "other", true) == 0)
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetUser().Call("setGender", GetEnum(genderClassPath, "OTHER"));
#endif
            }
            else
            {
                Debug.LogError("WebEngageBridge: Invalid gender: " + gender + ". Must be one of [male, female, other]");
            }
        }

        public static void SetOptIn(string channel, bool optIn)
        {
            if ("push".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetUser().Call("setOptIn", GetEnum(channelClassPath, "PUSH"), optIn);
#endif
            }
            else if ("in_app".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetUser().Call("setOptIn", GetEnum(channelClassPath, "IN_APP"), optIn);
#endif
            }
            else if ("email".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetUser().Call("setOptIn", GetEnum(channelClassPath, "EMAIL"), optIn);
#endif
            }
            else if ("sms".Equals(channel, System.StringComparison.OrdinalIgnoreCase))
            {
#if (UNITY_ANDROID && !UNITY_EDITOR)
                GetUser().Call("setOptIn", GetEnum(channelClassPath, "SMS"), optIn);
#endif
            }
            else
            {
                Debug.LogError("WebEngageBridge: Invalid channel name: " + channel + ". Must be one of [push, in_app, email, sms]");
            }
        }

        public static void SetUserAttribute(string key, object val)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaObject javaObject = GetJavaObject(val);
            GetUser().Call("setAttribute", key, javaObject);
#endif
        }

        public static void SetUserAttributes(Dictionary<string, object> map)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaObject hashMap = GetHashMap(map);
            GetUser().Call("setAttributes", hashMap);
#endif
        }

     public static void SetDevicePushOptIn(bool val)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("setDevicePushOptIn", val);
#endif
        }

        public static void DeleteUserAttribute(string key)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetUser().Call("deleteAttribute", key);
#endif
        }

        public static void DeleteUserAttributes(List<string> keys)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaObject arrayList = GetArrayListString(keys);
            GetUser().Call("deleteAttributes", arrayList);
#endif
        }

        // Tracking Screens
        public static void ScreenNavigated(string screen)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetAnalytics().Call("screenNavigated", screen);
#endif
        }

        public static void ScreenNavigated(string screen, Dictionary<string, object> data)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaObject hashMap = GetHashMap(data);
            GetAnalytics().Call("screenNavigated", screen, hashMap);
#endif
        }

        public static void SetScreenData(Dictionary<string, object> data)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaObject hashMap = GetHashMap(data);
            GetAnalytics().Call("setScreenData", hashMap);
#endif
        }

        // Push Messages
        public static void SetPushToken(string token)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            GetWebEngage().Call("setRegistrationID", token);
#endif
        }

        public static void SendPushData(Dictionary<string, string> data)
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            AndroidJavaObject hashMap = GetHashMapString(data);
            GetWebEngage().Call("receive", hashMap);
#endif
        }

        public static void UpdateFcmToken()
        {
#if (UNITY_ANDROID && !UNITY_EDITOR)
            try
            {
                AndroidJavaClass webEngageFcmClass = new AndroidJavaClass("com.webengage.android.fcm.WebEngageFirebaseMessagingService");
                webEngageFcmClass.CallStatic("updateToken");
            }
            catch (AndroidJavaException aje)
            {
                Debug.LogError("WebEngage Unity Helper library not found: " + aje);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Exception while updating FCM token: " + e);
            }
#endif
        }
    }
}
