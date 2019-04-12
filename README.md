# WebEngage Unity Android

WebEngage Unity Android plugin for Unity Android apps. This unitypackage is only for Android and would not work on any other platform.


## Installation

 1. Download the WebEngageUnityAndroid.unitypackage from root of this repository.

 2. Import the downloaded unitypackage into your Unity project through `Assets` > `Import Package` > `Custom Package...`.


## Initialization

1. Add the following meta-data tags in `Assets/Plugins/Android/AndroidManifest.xml` file of your Unity project.

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    ...>

    <application
    	...>

	    <meta-data android:name="com.webengage.sdk.android.key" android:value="YOUR-WEBENGAGE-LICENSE-CODE" />

	    <!-- true if development build else false -->
	    <meta-data android:name="com.webengage.sdk.android.debug" android:value="true" />

	    ...
	</application>
</manifest>
```

If Add `AndroidManifest.xml` file does not exist in `Assets/Plugins/Android/` directory of your Unity project, then you can create a new `AndroidManifest.xml` file and copy the below content in it.

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:tools="http://schemas.android.com/tools">

    <application
        android:label="@string/app_name"
        android:icon="@drawable/app_icon">

        <meta-data android:name="com.webengage.sdk.android.key" android:value="YOUR-WEBENGAGE-LICENSE-CODE" />

        <meta-data android:name="com.webengage.sdk.android.debug" android:value="true" />

        <activity
            android:name="com.unity3d.player.UnityPlayerActivity"
            android:configChanges="fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen">

            <intent-filter>

                <action
                    android:name="android.intent.action.MAIN" />

                <category
                    android:name="android.intent.category.LAUNCHER" />

                <category
                    android:name="android.intent.category.LEANBACK_LAUNCHER" />
            </intent-filter>

            <meta-data
                android:name="unityplayer.UnityActivity"
                android:value="true" />
        </activity>
    </application>
</manifest>
```

**Note:** Replace YOUR-WEBENGAGE-LICENSE-CODE with your own WebEngage license code.


2. Initialize the WebEngage SDK at start of your application.

```csharp
using WebEngage;
...

public class YourScript : MonoBehaviour
{
    private void Awake()
    {
        WebEngageBridge.Engage();
        ...
    }
    ...
}
```


## Attribution Tracking

Add the following receiver tag in the `Assets/Plugins/Android/AndroidManifest.xml` file.

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    ...>

    <application
        ...>

        ...

        <receiver
          android:name="com.webengage.sdk.android.InstallTracker"
          android:exported="true">
          <intent-filter>
             <action android:name="com.android.vending.INSTALL_REFERRER" />
          </intent-filter>
        </receiver>

        ...
    </application>
</manifest>
```


## Tracking Users

1. Login and Logout

```csharp
using WebEngageBridge;
...

public class YourScript : MonoBehaviour
{
    ...

    // User login
    WebEngage.Login("userId");

    // User logout
    WebEngage.Logout();
}
```

2. Set system user attributes as shown below.

```csharp
using WebEngageBridge;
...

public class YourScript : MonoBehaviour
{
    // Set user first name
    WebEngage.SetFirstName("John");

    // Set user last name
    WebEngage.SetLastName("Doe");  

    // Set user email
    WebEngage.SetEmail("john.doe@email.com");

    // Set user gender
    WebEngage.SetGender(Gender.Male);

    // Set user birth-date
    WebEngage.SetBirthDate("1994-04-29");

    // Set user company
    WebEngage.SetCompany("Google");
}
```

3. Set custom user attributes as shown below.

```csharp
using WebEngageBridge;
    ...

    WebEngage.SetUserAttribute("age", 25);
    WebEngage.SetUserAttribute("premium", true);

    Dictionary<string, object> customAttributes = new Dictionary<string, object>();
    customAttributes.Add("Twitter Email", "john.twitter@mail.com");
    customAttributes.Add("Subscribed", true);
    WebEngage.SetUserAttributes(customAttributes);
```

4. Delete custom user attributes as shown below.

```csharp
using WebEngageBridge;
    ...
    
    WebEngage.DeleteUserAttribute("age");
```


## Tracking Events

Track custom events as shown below.

```csharp
using WebEngageBridge;
    ...

    // Track simple event
    WebEngage.TrackEvent("Product - Page Viewed");

    // Track event with attributes
    Dictionary<string, object> orderPlacedAttributes = new Dictionary<string, object>();
    orderPlacedAttributes.Add("Amount", 808.48);
    orderPlacedAttributes.Add("Product 1 SKU Code", "UHUH799");
    orderPlacedAttributes.Add("Product 1 Name", "Armani Jeans");
    orderPlacedAttributes.Add("Product 1 Price", 300.49);
    orderPlacedAttributes.Add("Product 1 Size", "L");
    orderPlacedAttributes.Add("Product 2 SKU Code", "FBHG746");
    orderPlacedAttributes.Add("Product 2 Name", "Hugo Boss Jacket");
    orderPlacedAttributes.Add("Product 2 Price", 507.99);
    orderPlacedAttributes.Add("Product 2 Size", "L");
    orderPlacedAttributes.Add("Delivery Date", System.DateTime.ParseExact("2017-10-21 09:27:37.100", "yyyy-MM-dd hh:mm:ss.fff", null));
    orderPlacedAttributes.Add("Delivery City", "San Francisco");
    orderPlacedAttributes.Add("Delivery ZIP", "94121");
    orderPlacedAttributes.Add("Coupon Applied", "BOGO17");
    WebEngage.TrackEvent("Order Placed", orderPlacedAttributes);

    // Track complex event
    Dictionary<string, object> product1 = new Dictionary<string, object>();
    product1.Add("SKU Code", "UHUH799");
    product1.Add("Product Name", "Armani Jeans");
    product1.Add("Price", 300.49);

    Dictionary<string, object> detailsProduct1 = new Dictionary<string, object>();
    detailsProduct1.Add("Size", "L");
    product1.Add("Details", detailsProduct1);

    Dictionary<string, object> product2 = new Dictionary<string, object>();
    product2.Add("SKU Code", "FBHG746");
    product2.Add("Product Name", "Hugo Boss Jacket");
    product2.Add("Price", 507.99);

    Dictionary<string, object> detailsProduct2 = new Dictionary<string, object>();
    detailsProduct2.Add("Size", "L");
    product2.Add("Details", detailsProduct2);

    Dictionary<string, object> deliveryAddress = new Dictionary<string, object>();
    deliveryAddress.Add("City", "San Francisco");
    deliveryAddress.Add("ZIP", "94121");

    Dictionary<string, object> orderPlacedAttributes = new Dictionary<string, object>();
    List<object> products = new List<object>();
    products.Add(product1);
    products.Add(product2);

    List<string> coupons = new List<string>();
    coupons.Add("BOGO17");

    orderPlacedAttributes.Add("Products", products);
    orderPlacedAttributes.Add("Delivery Address", deliveryAddress);
    orderPlacedAttributes.Add("Coupons Applied", coupons);

    WebEngage.TrackEvent("Order Placed", orderPlacedAttributes);
```


## Push Notifications

#### 1. Using FCM Unity Plugin

1. Import FCM Unity plugin as instructed [here](https://firebase.google.com/docs/cloud-messaging/unity/client) into your Unity project.

2. If you have replaced the `Assets/Plugins/Android/AndroidManifest.xml` then make sure to add back your WebEngage license-code and debug-mode in the `AndroidManifest.xml` file.

3. In your script where you have registered callbacks for `OnTokenReceived` and `OnMessageReceived`, add the following code snippets.

```csharp
public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
{
    ...
    WebEngageBridge.SetPushToken(token.Token);
}

public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
{
    Dictionary<string, string> data = new Dictionary<string, string>(e.Message.Data);
    if (data.ContainsKey("source") && "webengage".Equals(data["source"]))
    {
        WebEngageBridge.SendPushData(data);
    }
    ...
}
```

Push notifications will work as expected when app is in foreground.

**Note**: Drawback of this approach is that push notifications will not be shown when app is in background. However those push notifications are cached and will be shown on next app launch. If you wish to prevent this drawback, then follow the Overriding FCM Unity Plugin approach given below.

#### 2. Overriding FCM Unity Plugin

1. Import FCM Unity plugin as instructed [here](https://firebase.google.com/docs/cloud-messaging/unity/client) into your Unity project.

2. If you have replaced the `Assets/Plugins/Android/AndroidManifest.xml` then make sure to add back your WebEngage license-code and debug-mode in the `AndroidManifest.xml` file.

3. Download and add the `webengage-android-fcm.aar` file from root of this repository in `Assets/Plugins/Android/` directory of your Unity project.

4. Add the following service tag in your `Assets/Plugins/Android/AndroidManifest.xml` file as shown below.

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    ...>

    <application
        ...>

        <service android:name="com.webengage.android.fcm.WebEngageFirebaseMessagingService">
            <intent-filter>
                <action android:name="com.google.firebase.MESSAGING_EVENT" />
            </intent-filter>
        </service>

        ...
    </application>
</manifest>
```

5. Update the FCM registration token on app start as shown below.

```csharp
using WebEngageBridge;
    ...

    WebEngage.UpdateFcmToken();
```


## In-app Notifications

No additional steps are required for in-app notifications.
