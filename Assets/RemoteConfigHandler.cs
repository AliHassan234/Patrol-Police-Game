//using Firebase.Analytics;
//using Firebase.Extensions;
//using System;
//using System.Threading.Tasks;
//using UnityEngine;

//public class RemoteConfigHandler : SingletonDoNotDestroy<RemoteConfigHandler>
//{
//    public RemoteConfigs remoteConfigs;

//    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
//    public  static bool isFirebaseInitialized, remoteReady, remoteNotReady;

//    // When the app starts, check to make sure that we have
//    // the required dependencies to use Firebase, and if not,
//    // add them if possible.
//    private void Start()
//    {
//        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
//        {
//            dependencyStatus = task.Result;
//            if (dependencyStatus == Firebase.DependencyStatus.Available)
//            {
//                InitializeFirebase();
//            }
//            else
//            {
//                Debug.LogError(
//                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
//            }
//        });
//    }


//    // Initialize remote config, and set the default values.
//    void InitializeFirebase()
//    {
//        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
//        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
//        // [START set_defaults]
//        System.Collections.Generic.Dictionary<string, object> defaults =
//          new System.Collections.Generic.Dictionary<string, object>();

//        // These are the values that are used if we haven't fetched data from the
//        // server
//        // yet, or if we ask for values that the server doesn't have:
//        for (int i = 0; i < remoteConfigs.configs.Length; i++)
//        {
//            defaults.Add(remoteConfigs.configs[i].key, remoteConfigs.configs[i].value);
//        }

//        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
//          .ContinueWithOnMainThread(task =>
//          {
//              // [END set_defaults]
//              Debug.Log("RemoteConfig configured and ready!");
//              isFirebaseInitialized = true;

//              Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener
//                  += ConfigUpdateListenerEventHandler;
//              FetchDataAsync();
//          });
//    }


//    public void PlayBtn()
//    {
//        // Log an event with a float.
//        FirebaseAnalytics.LogEvent("PlayBtnClicked");
//    }

//    public void LevelStart(int num)
//    {
//        // Log an event with a float.
//        FirebaseAnalytics.LogEvent("LevelProgress", "Started", num);
//    }

//    public void LevelComplete(int num)
//    {
//        // Log an event with a float.
//        FirebaseAnalytics.LogEvent("LevelProgress", "Completed", num);
//    }

//    public void NextClicked(int num)
//    {
//        // Log an event with a float.
//        FirebaseAnalytics.LogEvent("LevelProgress", "NextClicked", num);
//    }



//    // Display the currently loaded data.  If fetch has been called, this will be
//    // the data fetched from the server.  Otherwise, it will be the defaults.
//    // Note:  Firebase will cache this between sessions, so even if you haven't
//    // called fetch yet, if it was called on a previous run of the program, you
//    //  will still have data from the last time it was run.
//    private void DD()
//    {
//        for (int i = 0; i < remoteConfigs.configs.Length; i++)
//        {
//            Debug.Log(remoteConfigs.configs[i].key + ": " + Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(remoteConfigs.configs[i].key).BooleanValue);

//            remoteConfigs.configs[i].value = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
//                     .GetValue(remoteConfigs.configs[i].key).BooleanValue;
//        }
//        remoteReady = true;
//    }


//    private void ConfigUpdateListenerEventHandler(
//        object sender, Firebase.RemoteConfig.ConfigUpdateEventArgs args)
//    {
//        if (args.Error != Firebase.RemoteConfig.RemoteConfigError.None)
//        {
//            Debug.Log(String.Format("Error occurred while listening: {0}", args.Error));
//            return;
//        }
//        Debug.Log(String.Format("Auto-fetch has received a new config. Updated keys: {0}",
//            string.Join(", ", args.UpdatedKeys)));
//        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
//        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
//          .ContinueWithOnMainThread(task =>
//          {
//              Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
//                              info.FetchTime));
//          });
//    }


//    // [START fetch_async]
//    // Start a fetch request.
//    // FetchAsync only fetches new data if the current data is older than the provided
//    // timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
//    // By default the timespan is 12 hours, and for production apps, this is a good
//    // number. For this example though, it's set to a timespan of zero, so that
//    // changes in the console will always show up immediately.
//    public Task FetchDataAsync()
//    {
//        Debug.Log("Fetching data...");
//        System.Threading.Tasks.Task fetchTask =
//        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
//            TimeSpan.Zero);
//        return fetchTask.ContinueWithOnMainThread(FetchComplete);
//    }
//    //[END fetch_async]

//    void FetchComplete(Task fetchTask)
//    {
//        if (fetchTask.IsCanceled)
//        {
//            Debug.Log("Fetch canceled.");
//        }
//        else if (fetchTask.IsFaulted)
//        {
//            Debug.Log("Fetch encountered an error.");
//        }
//        else if (fetchTask.IsCompleted)
//        {
//            Debug.Log("Fetch completed successfully!");
//        }

//        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
//        switch (info.LastFetchStatus)
//        {
//            case Firebase.RemoteConfig.LastFetchStatus.Success:
//                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
//                .ContinueWithOnMainThread(task =>
//                {
//                    Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
//                               info.FetchTime));
//                    DD();
//                });

//                break;
//            case Firebase.RemoteConfig.LastFetchStatus.Failure:
//                switch (info.LastFetchFailureReason)
//                {
//                    case Firebase.RemoteConfig.FetchFailureReason.Error:
//                        Debug.Log("Fetch failed for unknown reason");
//                        break;
//                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
//                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
//                        break;
//                }

//                remoteNotReady = true;
//                break;
//            case Firebase.RemoteConfig.LastFetchStatus.Pending:
//                Debug.Log("Latest Fetch call still pending.");
//                break;
//        }
//    }
//}
//[Serializable]
//public class RemoteConfigs
//{
//    public Config[] configs;
//}
//[Serializable]
//public class Config
//{
//    public string key;
//    public bool value;
//}