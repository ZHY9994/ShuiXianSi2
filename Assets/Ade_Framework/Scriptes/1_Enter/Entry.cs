using Ade_Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entry : MonoBehaviour
{
    private const int MainSceneBuildIndex = 1;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void Init()
    {
        AdeSDK.Instance.Init(ShowScene);
    }

    public void ShowScene() 
    {
#if Ade_TT
        // 当前模板只有一个主场景，推荐流分支先共用同一入口。
        AdeSDK.Instance.CheckFeedLaunchScene(LoadFeedDirectPlayScene, LoadFeedAcquisitionScene, LoadDefaultScene);
#else
        LoadDefaultScene();
#endif
    }

    private void LoadDefaultScene()
    {
        SceneManager.LoadScene(MainSceneBuildIndex);
    }

#if Ade_TT
    private void LoadFeedDirectPlayScene()
    {
        switch (AdeSDK.Instance.CurrentFeedRepeatSceneType)
        {
            case FeedRepeatSceneType.OfflineReward:
                LoadFeedOfflineRewardScene();
                return;
            case FeedRepeatSceneType.EnergyRecovery:
                LoadFeedEnergyRecoveryScene();
                return;
            default:
                LoadFeedImportantEventScene();
                return;
        }
    }

    /// <summary>
    /// 离线收益复访推荐流预留入口。
    /// 在这里补充离线收益场景加载、数据初始化和界面准备逻辑。
    /// </summary>
    private void LoadFeedOfflineRewardScene()
    {
        LoadConfiguredFeedScene(true);
    }

    /// <summary>
    /// 体力恢复复访推荐流预留入口。
    /// 在这里补充体力恢复场景加载、数据初始化和界面准备逻辑。
    /// </summary>
    private void LoadFeedEnergyRecoveryScene()
    {
        LoadConfiguredFeedScene(true);
    }

    /// <summary>
    /// 重要事件提醒复访推荐流预留入口。
    /// 在这里补充重要事件场景加载、数据初始化和界面准备逻辑。
    /// </summary>
    private void LoadFeedImportantEventScene()
    {
        LoadConfiguredFeedScene(true);
    }


    /// <summary>
    /// 获客推荐流预留入口。
    /// 在这里补充获客场景加载、数据初始化和界面准备逻辑。
    /// </summary>
    private void LoadFeedAcquisitionScene()
    {
        LoadConfiguredFeedScene(false);
    }


    private void LoadConfiguredFeedScene(bool startFeedGameplay)
    {
        // The platform only accepts the ready signal after the game is interactive.
        AsyncOperation load = SceneManager.LoadSceneAsync(MainSceneBuildIndex);
        if (load == null)
        {
            Debug.LogError("[推荐流] 无法加载主场景，未上报就绪");
            return;
        }

        load.completed += _ =>
        {
            if (startFeedGameplay)
            {
                StartCoroutine(StartFeedGameplayAfterMainSceneReady());
                return;
            }

            AdeSDK.Instance.ReportFeedSceneReady();
        };
    }

    private IEnumerator StartFeedGameplayAfterMainSceneReady()
    {
        // SceneManager's completed callback can run before all Main scene
        // singletons have finished their first frame initialization.
        const int maxWaitFrames = 300;
        int waitedFrames = 0;
        yield return null;
        while ((KobGamesSDKSlim.StorageManager.Instance == null
                || KobGamesSDKSlim.GameManager.Instance == null
                || LevelManager.Instance == null
                || LevelManager.Instance.CurrentLevel == null
                || KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance == null)
            && waitedFrames++ < maxWaitFrames)
        {
            yield return null;
        }

        KobGamesSDKSlim.StorageManager storage = KobGamesSDKSlim.StorageManager.Instance;
        KobGamesSDKSlim.GameManager gameManager = KobGamesSDKSlim.GameManager.Instance;
        KobGamesSDKSlim.MenuManagerV1.MenuManager menuManager = KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance;
        if (storage == null || gameManager == null || LevelManager.Instance == null || menuManager == null)
        {
            Debug.LogError("[推荐流-复访] 主场景初始化超时，无法直接进入关卡");
            yield break;
        }

        // HighScoreLevel is the game's persisted level progress. A new player
        // has no save key and must start from level 1.
        int startLevel = PlayerPrefs.HasKey("HighScoreLevel")
            ? storage.HighScoreLevel
            : storage.DefaultStartlevel;
        storage.CurrentLevel = Mathf.Max(1, startLevel);
        Debug.Log($"[推荐流-复访] 直接进入第 {storage.CurrentLevel} 关");

        // Match the normal start event sequence while skipping the welcome page
        // and transition UI. The level has already been prepared by GameManager.
        gameManager.StartGame();
        Screen_Transition.OnStartGame?.Invoke();
        menuManager.OpenMenuScreen("Screen_GamePlay", false);
        AdeSDK.Instance.ReportFeedSceneReady();
    }
#endif
}
