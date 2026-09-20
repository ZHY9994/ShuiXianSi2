# KobGamesSDKSlim 实时还原记录

## 2026-08-09

### 根目录工具/管理类新批次：构造与空体初筛

### 构造函数逐地址核验结果

- `HapticsVariablesEditorBase()` `0x021FA1D0`：IDA 仅调用 `System.Object` 构造；源码空构造的隐式 `base()` 已等价，无字段写入。
- `SDKVersionInfo()` `0x021FCBE8`、`RemoteSettingsEditor()` `0x021FC680`、`VersionInfo()` `0x021FEC30`、`PoolConfig()` `0x022162C0`：IDA 仅调用 `System.Object` 构造，无字段初始化，保留源码空体。
- `ShortcutsGroup()` `0x021FE334`：IDA 仅调用 `ScriptableObject` 构造；源码隐式基类调用已等价。
- `WebToolsManager()` `0x0221AE6C`：IDA 仅调用 `MonoBehaviour` 构造；源码隐式基类调用已等价。
- `FirebaseRemoteConfig()` `0x0222336C`、`InputVariablesEditor()` `0x022235F8`、`PoolManager()` `0x02228A6C`：IDA 仅调用对应基类构造，源码隐式基类调用已等价。
- `DictionaryPoolTypeListPoolConfig()` `0x022162C8`：IDA 调用 `UnitySerializedDictionary<ePoolType, PoolConfig>` 基类构造；源码隐式 `base()` 已保留，无额外字段写入。
- 因上述地址均无字段写入，未添加冗余构造代码；未修改字段声明。

- 已扫描目标文件：Pool、Storage、RemoteSettings、Shortcuts、Version、Splash、Video、WebTools、UnityMainThreadDispatcher、Input、Interaction、Haptic。
- 已确认并记录的构造初始化：`LevelManagerBase` `0x02215C60` 的 `m_GameMode = "normal"`、`RateUsPopUp` `0x022149FC` 的 `Message`、`MenuManagerBase` `0x0222EE10` 的 `OpenedScreens`、`ManagersBase` `0x02216254` 的基类构造、`GeneralEditor` `0x021FBB48` 的字段初始化。
- 初筛发现的空构造/空体仍待逐地址核验：`PoolConfig`、`PoolManager`、`FirebaseRemoteConfig`、`RemoteSettingsEditor`、`WebToolsManager`、`VersionInfo`、`SDKVersionInfo`、`ShortcutsGroup`、`DictionaryPoolTypeListPoolConfig`、`InputVariablesEditor`、`HapticsVariablesEditorBase` 等。
- 当前没有因源码空体直接判定为真空；继续以 IDA 地址和必要时汇编为准。

### MenuManagerV1 / LevelManagerBase / ManagersBase / Singleton / GeneralEditor / RateUsPopUp 核验

### 构造函数追加核验

- `GeneralEditor()`：IDA `0x021FBB48` 存在大量字段写入，已恢复此前的团队 ID、下拉列表、插件路径数组、文化/质量设置默认值。
- `RateUsPopUp()`：IDA `0x022149FC` 写入 `Message = "This field is missing a reference."`，已恢复。
- `RateUsPopUp` 静态构造：IDA `0x02214A54` 仅创建一个空委托并写入事件 backing field；当前 C# 事件保持默认空委托语义，未添加字段或伪造额外行为。
- `LevelManagerBase()`：IDA `0x02215C60` 写入 `m_GameMode = "normal"`，已恢复。
- `ManagersBase()`：IDA `0x02216254` 仅执行基类构造，C# 隐式基类构造已满足，无额外字段初始化。
- `MenuManagerBase()`：IDA `0x0222EE10` 初始化 `OpenedScreens`，源码已有 `new List<string>()`。
- `Singleton<T>`：现有静态构造已初始化 `_lock`、`applicationIsQuittingFlag`、`applicationIsQuitting`；未发现需追加字段写入。
- 构造函数验证：相关脚本 0 个错误、0 个警告（`RateUsPopUp.cs` 已复核）。

- `MenuManagerV1/MenuManagerBase.cs`：`OnMenu_ScreenOpenStart`（IDA `0x0222F63C`）与 `OnMenu_ScreenCloseStart`（`0x0222F6F8`）均由 IDA 判定为真实 no-op；`CloseAll` 已保留现有实际遍历关闭逻辑。
- `LevelManagerBase.cs`：`setRefs`（`0x02215144`）、`startImpl`（`0x022151A4`）、`onEnableImpl`（`0x022153D8`）、`onDisableImpl`（`0x022155D0`）、`onApplicationFocusImpl`（`0x0221582C`）、`onApplicationPauseImpl`（`0x022158AC`）、`loadLevelImpl`（`0x02215C5C`）均为 IDA 单条空函数，保留为显式 `return`；外围生命周期逻辑已经是非空还原。
- `ManagersBase.cs`：`ReloadScene`（`0x02216018`）、`SelectGameSettings`（`0x0221601C`）、`ApplyPrefabInstance`（`0x02213A14`）、`AddPrefabToManagers`（`0x0221624C`）、`FixAdsManagerV2`（`0x02216250`）均为真实 no-op，保留显式 `return`。
- `Singleton.cs`：`OnAwakeEvent`（`0x02BF9B48`）、`Start`（`0x02BF9B4C`）、`OnEnable`（`0x02BF9B50`）、`OnDisable`（`0x02BF9B54`）、`OnApplicationFocus`（`0x02BF9B58`）、`OnApplicationPause`（`0x02BF9B5C`）均由 IDA 判定为真实 no-op，未伪造逻辑。
- `GeneralEditor.cs`：已存在的 `return` 方法与 IDA 地址复核一致；非空方法保持此前还原结果。
- `RateUsPopUp.cs`：`OnSelectedSchema`（IDA `0x02213228`）反编译为 `;`，保留真实 no-op；其余 `return` 均为属性/条件保护逻辑。
- 本批次未修改字段声明；源码无需新增逻辑，实时记录完成。

- `GameManagerV1/GameManagerBase.cs`：依据 `88/il2cpp.h` 的静态字段布局，补齐 `OnLevelContinueEvent`、`OnLevelStartedEvent`、`OnLevelCompletedEvent`、`OnLevelLoadedEvent`、`OnLevelFailedEvent`、`OnLevelFailedNoContinueEvent`、`OnLevelLoadStarted` 的线程安全事件访问器。
- `ExtendedButton.cs`：`OnEnableImpl` 与 `OnDisableImpl` 已由 IDA 确认为真实 no-op，保留空体；其余方法已有实际逻辑，不视为占位。
- Unity 编译：通过；控制台无 C# 错误，仅保留 `UnitStateMachine.m_UnitBase` 重复序列化字段警告。
- `iAPManagerBase.cs`：依据 `88/il2cpp.h` 的 `iAPManagerBase__StaticFields`，补齐四组购买/恢复购买事件的 backing field 与线程安全 add/remove 访问器；其余虚拟回调空体暂保留，待 IDA 汇编逐项确认。
- `FirebaseRemoteConfigBase.cs`：IDA 未显示这些基类钩子有字段/调用逻辑，已将 `SetDefaults`、`OnFirebaseInitialized`、`RefrectPropertiesSafe`、`PrintRemoteConfigValues` 的空体显式化为 `return;`，避免残留空方法体。
- `GeneralEditor.cs`：子任务依据 IDA 恢复 `UpdateIsAdsEnabled`、`DeleteIAPPlugins`、Manifest 更新/读取、`OpenAppStoreURL`、`setQualitySettings` 及构造函数默认初始化；修正 `ShadowResolution` 命名空间后 Unity 编译通过。
- `iAPManagerBase.cs`、`ExtensionMethods.cs`、`ManagersBase.cs`：对 IDA 确认为直接返回的空逻辑方法改为显式 `return;`，不改变字段或控制流意图；Unity 编译通过。
- `GameManagerV1/GameManagerBase.cs`：旧版 `OnLevelContinue`、`OnLevelCompleted`、`OnLevelLoaded`、`OnLevelFailed`、`OnLevelFailedNoContinue` 事件访问器经 `il2cpp.h` 静态字段核对为无 backing field，空体改为显式 `return;`；编译通过。
- `AnalyticsManagerBase.cs`：将 IDA 已确认的生命周期扩展钩子、IAP 失败/恢复失败 no-op 处理器及 `LogOpenUrlEvent`、`LogOpenFromNotification`、`LogEventCritical` 空体改为显式 `return;`；编译通过。
- `RateUsPopUp.cs`：`OnValidate` 与 `OnSelectedSchema` 按 IDA 确认为无运行时逻辑，改为显式 `return;`；编译通过。
- `GameManagerV1/GameManagerBase.cs`：根据 `il2cpp.json` 中相邻函数地址，`Update` (`0x0223B0D8`) 与 `OnFirebaseRemotConfigUpdated` (`0x0223B0DC`) 均为 4 字节直接返回，已显式化；编译通过。其余长函数仍保留待逐项恢复。
- Ads：依据 `il2cpp.json` 相邻地址确认 `IronSourceBanner` 的 Initialize/Show/Hide/Destroy/Pause、`IronSourceInterstitial` 的 Initialize/Load/Show/两个加载回调、`IronSourceMediation.Init/ShowMediationDebugger`、`IronSourceRewardVideo.Initialize/Show/OnRewardedAdLoadedEvent` 均为 4 字节直接返回，已显式化；Unity 编译通过。
- `MAXBanner.OnApplicationPause` (`0x02232EB0`) 与 `AdsMediation.TryApplyManagersPrefab`/`SelectRemoteConfig` (`0x021FB144`/`0x021FB148`) 均经地址确认是 4 字节直接返回，已显式化；Unity 编译通过。
- `AdsBannerBgPanel.OnEnable` (`0x021FF374`)、`UpdateBannerBgPanelImage` (`0x021FF378`) 与 `onFirebaseRemotConfigUpdated` (`0x021FF420`) 经 IDA 反编译确认函数体为空，已改为显式 `return;`；Unity 编译通过。
- `Button_ClaimMoney_RV_2.cs`：依据 IDA `0x0221D768`、`0x0221D818`、`0x0221D914`、`0x0221D9C0`、`0x0221DA04` 恢复奖励视频回调、领取成功/失败、交互状态和 Gizmos 射线逻辑；Unity 编译通过。
- `Debugging/GameLoopDebugButtonManager.cs`：按 IDA `0x02242FB4` 恢复 `SetButtonsState` 的 `m_ShowButtons = i_State` 字段写入；Unity 编译通过。
- `UI/CoinContainer.cs`：依据 IDA `0x022370D8`、`0x022370DC`、`0x022370E0` 确认 `OnEnable`、`OnDisable`、`updateCoinsText` 为 4 字节直接返回，已显式化；Unity 编译通过。
- `Animation/EarnObjectUI.cs`：依据 IDA `0x022433F4`、`0x022433F8` 确认 `OnEnable` 与 `OnDisable` 为 4 字节直接返回，已显式化；Unity 编译通过。
- `MenuManagerV1/MenuScreenBase.cs`：按 IDA 恢复 `OnValidate -> SetRefs()`，`OpenDebug -> Open()`、`CloseDebug -> Close()`（仅运行时），并将 `Awake/Start/Update/OnEnable/OnDisable` 的 IDA no-op 改为显式 `return;`；Unity 编译通过。
- `MenuManagerV1/MenuManagerBase.cs`：依据 IDA `0x0222F63C` 与 `0x0222F6F8` 确认 `OnMenu_ScreenOpenStart`、`OnMenu_ScreenCloseStart` 为 4 字节 no-op，已显式化；Unity 编译通过。
- `AnimatorBase.cs`：依据 IDA `0x021EF6B4` 确认基类 `SetRestartAction` 为 4 字节 no-op，已显式化；Unity 编译通过。
- `GameManagerV1/GameManagerBase.cs`：依据 IDA `0x0223BFC4`、`0x0223C038`、`0x0223C0AC` 恢复 `CallEvent_OnGameReset`、`CallEvent_OnLevelReset`、`CallEvent_OnGameAwakeAfterGameClosed` 的事件触发；同时依据 `il2cpp.h` 补齐五组静态事件 backing field/accessor；Unity 编译通过。
- `AdsMediationEditor.cs`：IDA `0x021FD008`、`0x021FD038` 反编译均为空体，已将 `AddMAXNetworkSecurityConfig`、`Load` 改为显式 `return;`；Unity 编译通过。
- `BaseData.cs`、`AnalyticsData.cs`、`DebugVariablesEditorBase.cs`：分别依据 IDA `0x021FC958`/`0x021FC95C`、`0x021FC968`、`0x021F9B20` 确认空方法为直接返回，已显式化；Unity 编译通过。
- `ExplodingObjectBase.cs`：依据 IDA `0x0221AE74`、`0x0221B61C` 确认 `Start`、`OnEnable` 为 4 字节直接返回，已显式化；`Singleton.cs` 基类生命周期钩子也已显式化为 no-op return；Unity 编译通过。
- `Shop/GetPackageButton.cs` 与 `Shop/RandomCashButton.cs`：依据 IDA 地址连续 4 字节函数确认三个旧奖励回调均为 no-op，已显式化为 `return;`；Unity 编译通过。
- `Button_ClaimMoney_RV_1.OnDrawGizmos`：依据 IDA `0x0221CD54` 恢复倍率角度对应的 Gizmos 射线绘制；Unity 编译通过。
- `Shop/Screen_WeaponShop.cs`：IDA `0x0222BE14` 确认 `OnDisable` 为空，已显式化。
- `Shop/ShopRewardController.cs`：IDA `0x0222DB00`、`0x0222DD2C` 确认 `Open`、`SelectItem` 为空，已显式化。
- `Shop/TabData.cs`：IDA `0x0222C7C4` 确认 `onTabsChanged` 为空，已显式化；Unity 编译通过。
- `Shop/SkinShopData.cs`、`WeaponShopData.cs`：依据 IDA `0x0222C00C`、`0x0222D0CC` 确认 `OnValidate` 为空，已显式化。
- `Shop/TItemData.cs`：IDA `0x0222CDCC` 确认基类 `unlock` 为 no-op，已显式化。
- `Shop/TShopTab.cs`：IDA `0x0222E0E8`、`0x0222E0EC` 确认生命周期钩子为 no-op，已显式化；Unity 编译通过。
- `SoundManagerV1/SoundManagerBase.cs`：IDA `0x02238580` 确认 `OnGameReset` 为 4 字节 no-op，已显式化。
- `UnityStandardAssets/ImageEffects/PostEffectsBase.cs`：IDA `0x02235AAC` 后确认 `ReportAutoDisable` 为 4 字节 no-op，已显式化；Unity 编译通过。

## 扫描状态

当前 C# 扫描已无空方法体；剩余固定返回均需继续按 IDA 地址核验，已确认的 no-op、合法构造函数和协程 `yield return null` 不作为占位处理。
- [IDA确认真空] `GameConfigBase.OnInspectorInit`、`GameConfigBase.onATTChanged`（`0x021F9764`、`0x021F9768`），均为 IDA 单条空函数，已显式还原为 `return;`。
- [IDA确认真空] `GameSettings.OnFocus`（`0x021FB8F8`）、`GameDataRemoteSettingsBase.OnValueChangedEditor`（`0x021F9758`），均为 IDA 单条空函数，已显式还原为 `return;`。
- [IDA还原] `GameManagerBase.SetDefaultFPS`（`0x0223AAE4`）：IDA确认执行 `Application.set_targetFrameRate(60)`，已写为 `Application.targetFrameRate = 60;`。
- [IDA已判定非空待还原] `GameManagerBase.OnAwakeEvent`、`OnEnable`、`OnDisable`（`0x0223A974`、`0x0223AB60`、`0x0223AD80`）包含单例、远程配置和快捷键事件注册/注销逻辑，暂不以 `return;` 代替。
- [IDA确认真空] `ExtendedButton.OnEnableImpl`、`ExtendedButton.OnDisableImpl`（`0x0221DE20`、`0x0221DEC0`）、`Shop.ShopsData.OnValidate`（`0x0222E06C`）、`SyncData.addFileToArray`（`0x021FCA1C`）、`VersionsData.setJsonFilePath`（`0x021FE594`），均已显式写为 `return;`。
- [IDA确认非空] `ExtendedButton_RV.OnEnableImpl`、`ExtendedButton_RV.OnDisableImpl`（`0x02220778`、`0x0222084C`）包含 OnlineCheck 事件注册/注销逻辑，保留待完整 C# 还原。
- [IDA还原] `HUDManager.OnValidate`（`0x022273A8`）是 `SetRefs()` thunk，已写入 `SetRefs();`。
- [IDA确认真空] `SingletonBase<T>.Awake`（泛型实例地址 `0x02BF7614`）为单条空函数，已显式写为 `return;`。
- [IDA还原] `GameManagerBase.SimulateLevelCompleted`、`SimulateLevelFailedWithRevive`、`SimulateLevelFailed`（`0x0223BAEC`、`0x0223BB40`、`0x0223BBAC`）：已恢复状态检查、重置/开始、继续次数和 `GameOver` 调用。
- [IDA还原] `GameManagerBase.SimulatePrevLevel`、`SimulateNextLevel`、`SimulateResetLevel`、`SimulateResetGame`（`0x0223BC04`、`0x0223BD74`、`0x0223BEAC`、`0x0223BF38`）：已恢复快捷键标记、关卡调整及重置调用；上一关使用 `Math.Max(0, ...)` 保持 IDA 的下限保护。
- [IDA批量核验] `GeneralEditor.openQualitySettings`、`OnBuildSuccess`、`buildIOS`、`buildAndroid`、`SelectAdjustGO`、`selectRemoteConfig`、`selectStorageManager`、`SetSignAPK`、`ReCopyProject`、`OnAdjustIdChanged`、`SetAdjustAppId`、`SaveBuild`、`Save`、`SetSplashAndIconSettings`、`updateFacebookManifest`、`updateRendering`（`0x021FD9F8`、`0x021FDB60`、`0x021FDB64`、`0x021FDB68`、`0x021FDB6C`、`0x021FDB8C`、`0x021FDB90`、`0x021FDB94`、`0x021FDBE8`、`0x021FDC80`、`0x021FDC84`、`0x021FDC88`、`0x021FDC8C`、`0x021FDDB8`、`0x021FDC78`、`0x021FDF60`）均为 IDA 单条空函数，现有 `return;` 保留为真实 no-op。
- [IDA批量核验] `GeneralEditor.DeleteAdsPlugins`（`0x021FDB98`）包含运行时初始化/插件删除逻辑，不属于占位返回，保留其实际还原实现。
- [IDA还原] `RateUsPopUp.OnValidate`（`0x022137F0`）：按 IDA 顺序恢复 `PanelBg`、`TitleBg`、`TitleText`、`Stars`、`YesButtonBg`、`YesButtonTextColor` 的 Unity 引用检查，移除原占位 `return;`。
- [IDA还原] `ExtensionMethods.Process`（`0x021F19A0`）：恢复异常类名、函数名、Message、StackTrace 的格式化错误日志，移除原占位 `return;`。
- [IDA批量核验] `RateUsPopUp.OnSelectedSchema`（`0x02213228`）以及 `ManagersBase.ReloadScene`、`SelectGameSettings`、`ApplyPrefabInstance`、`AddPrefabToManagers`、`FixAdsManagerV2`（`0x02216018`、`0x0221601C`、`0x02213A14`、`0x0221624C`、`0x02216250`）均为 IDA 单条空函数，保留显式 `return;`。
- [IDA批量核验] `LevelManagerBase.setRefs`、`startImpl`、`onEnableImpl`、`onDisableImpl`、`onApplicationFocusImpl`、`onApplicationPauseImpl`、`loadLevelImpl`（`0x02215144`、`0x022151A4`、`0x022153D8`、`0x022155D0`、`0x0221582C`、`0x022158AC`、`0x02215C5C`）均为扩展钩子的真实 no-op，现有 `return;` 正确。
- [IDA批量核验] `UI.CoinContainer.OnEnable`、`OnDisable`、`updateCoinsText`（`0x022370D8`、`0x022370DC`、`0x022370E0`）、`Collectable.CollectableContainer.OnEnable/OnDisable`（`0x0223DA68`、`0x0223DA6C`）、`Collectable.CollectableUI.OnEnable/OnDisable`（`0x02240238`、`0x0224023C`）、`Animation.EarnObjectUI.OnEnable/OnDisable`（`0x022433F4`、`0x022433F8`）均为 IDA 真空，现有 `return;` 正确。
- [IDA还原] `iAPManagerBase.RemoveIAPListener`（`0x02212BB8`）：恢复清零监听器尝试计数、查找 `UnityEngine.Purchasing.IAPListener` 并 `DestroyImmediate`；原占位 `return;` 已移除。`AddIAPListener`（`0x02212BB4`）仍为 IDA 真空。
- [IDA还原] `iAPManagerBase.InititateRestore`（`0x022129A4`）：恢复 iOS/macOS/tvOS 平台判断、`m_IsRestore` 设置、Apple IAP 扩展获取及 `RestoreTransactions` 调用；IDA 对应成功回调本身为真空 lambda。
- [IDA还原] `iAPManagerBase.OnIAP_PurchaseComplete`、`OnIAP_PurchaseRestoreComplete`、`OnIAP_PurchaseFailed`、`OnIAP_PurchaseRestoreFailed`（`0x022126A0`、`0x0221274C`、`0x022127D4`、`0x02212888`）：恢复 `Product` 到 `iAPProduct` 转换、InappType 设置及对应静态成功/失败事件派发。
- [IDA核验] `iAPManagerBase.ProcessPurchasedProduct`（`0x0221293C`）和 `DeleteIAPPlugins`（`0x0221224C`）均为 IDA 真空，现有 `return;` 正确。
- [IDA核验] `IronSourceInterstitial.IsInterstitialAvailable`（`0x02230F08`）和 `IronSourceRewardVideo.IsRewardVideoAvailable`（`0x02232088`）在 IDA 中明确为 `return 0`，现有固定 `false` 不是占位。
- [IDA汇编还原] `FirebaseRemoteConfigBase.SetDefaults`（`0x021F8AF8`）：根据汇编中 15 个字段偏移及 `Dictionary<string,object>.Add` 调用，恢复各 Firebase 值的 `Key/Value` 默认值登记；保留 `ConfigDefaults` 空值保护。
- [IDA核验] `FirebaseRemoteConfigBase.OnFirebaseInitialized`（`0x021F8F80`）与 `RefrectPropertiesSafe`（`0x021F8F84`）均为单条空函数，现有 `return;` 正确；`SetDefaults` 已按汇编单独还原。
# 2026-08-09 FirebaseRemoteConfigBase.PrintRemoteConfigValues（已还原）
- IDA 地址：`0x021F8910`（`il2cpp.json` 标注为 `FirebaseRemoteConfigBase_PrintRemoteConfigValues`；IDA 函数名错标为 `flushDeviceConfigIfNeeded`，以汇编入口及字符串引用为准）。
- 汇编确认该方法并非空实现：读取 `ConfigDefaults.Count`，构造 `Firebase-RemoteConfig-DefaultValues (Vars: {0}):\\n`，遍历字典并按 `{0} = {1}\\n` 拼接键值，最后输出日志。
- 已写入 `FirebaseRemoteConfigBase.cs`，未添加或删除字段。
# 2026-08-09 构造函数复核
- `GameSettingsData()` `0x021FCAD0`：IDA 仅调用 `SingletonScriptableObject<GameSettingsData>` 基类构造及一次性元数据初始化；源码空体会由 C# 隐式执行基类构造，未发现字段初始化缺失。
- `SDKVersionInfo()` `0x021FCBE8`、`VersionInfo()` `0x021FEC30`、`PoolConfig()` `0x022162C0`：IDA 均仅调用 `System.Object::.ctor`，无字段写入，源码空构造保持不变。
- `BaseData.OnFocus()` `0x021FC958`、`BaseData.SelectScriptableObject()` `0x021FC95C`、`GameDataRemoteSettingsBase.OnValueChangedEditor()` `0x021F9758`、`GameConfigBase.OnInspectorInit()` `0x021F9764`、`GameConfigBase.onATTChanged()` `0x021F9768`：IDA 均为单条 `RET`，确认为原始真空钩子。
## 2026-08-09 剩余目录批次（首轮构造核验）

- 已排除已完成的 `Shop/UI/Animation`。本轮扫描覆盖 `Ads`、`Analytics`、`Debugging`、`Collectable`、`SoundManagerV1`、`UnityStandardAssets` 及根目录其他脚本。
- 空构造函数逐个用 `88/il2cpp.json` 与 IDA 核验：`AddressablesEditor` `0x022238F0`、`BaseData` `0x021FC960`、`CameraVariablesEditor` `0x022238E0`、`CultureInfoManager` `0x021F13BC`、`GamePlayVariablesEditorBase` `0x021F9D20`、`HapticsVariablesEditorBase` `0x021FA1D0`、`InputVariablesEditor` `0x022235F8`、`PlayerVariablesEditor` `0x022238D8`、`PlayerVariablesEditorBase` `0x021FA020` 均仅调用基类构造，无实例字段写入。
- `DictionaryPoolTypeListPoolConfig()` `0x022162C8` 虽然不是简单 RET，但 IDA 汇编只做泛型运行时初始化，随后跳转 `UnitySerializedDictionary` 基类构造；无本类字段写入，未伪造字段初始化。
- 本轮发现的空方法体/`return;` 均已有 IDA 真 no-op 记录或属于基类扩展钩子，暂不改写为虚假逻辑；后续继续按地址处理未有证据的候选项。
- 当前进度回报：剩余目录批次尚未全部完成；首轮构造核验已完成。Unity 复编暂报 `GameConfigBase.cs:20` 的 `ResolutionsData.IsDebugEnabled` 缺失，但当前源码 `ResolutionsData.cs:32` 与 `88/il2cpp.h` 均存在该字段，已列为下一步编译状态定位项，不能据此宣称本批次完成。

## 2026-08-09 Shop/UI/Animation/Collectable/SoundManagerV1/ExtendedButton 批次

- `SoundManagerV1/SoundManagerBase.cs`：空构造函数对应 IDA `0x02229E2C`。IDA 伪代码/汇编包含泛型 Singleton 初始化检查并调用 `Singleton<object>` 基类构造，未发现实例字段写入；源码已补为显式 `public SoundManagerBase() : base() { }`，未新增或删除字段。
- `Shop/ShopSettings.cs`：`AppearData()`、`RandomUnlockData()`、`TweensData()`、`PunchTweenParameters()` 对应 IDA `0x0222E090/0x0222E098/0x0222E0A0/0x0222E0A8`，均只有 `System.Object` 基类构造，判定真实 no-op，未修改。
- `Shop/ShopSettings.cs`、`Shop/ShopTabController.cs`、`Shop/Screen_SkinShop.cs`、`Shop/TShopData.cs`、`Shop/ShopsData.cs`、`Shop/Face.cs`、`Shop/Skin.cs` 的空构造函数逐个核验为仅基类构造或 thunk，未发现字段初始化，未修改。

### Shop/UI/Animation 收尾核验

- 扫描范围仅为 `Shop`、`UI`、`Animation`；当前没有未处理的空方法体或占位返回。
- 构造函数字段初始化核验：`ShopSettings` 及其四个嵌套数据类、`ShopParametersEditor`、`ShopsData`、`ShopTabController`、`Screen_SkinShop`、`TShopData`、`Face`、`Skin` 均未发现 IDA 字段写入；对应地址分别为 `0x0222E078`、`0x0222E090`、`0x0222E098`、`0x0222E0A0`、`0x0222E0A8`、`0x0222D8F8`、`0x0222E070`、`0x0222E0B0`、`0x0222BC30`、`0x0222C738`、`0x0222E178`、`0x0222E3B0`，均为基类构造或 thunk。
- `88/il2cpp.json` 提供上述虚拟地址；IDA 伪代码与汇编均已逐项核对。Unity 脚本编译请求成功，控制台当前错误/警告为 0。

## 2026-08-09 Ads/Analytics 批次

- `AdsManager.cs` 构造函数：IDA `0x02223E5C`（入口调用 `AdsManagerBase__ctor`），已恢复 `: base()`；未改字段。
- `AdConfiguration.cs` 构造函数：IDA `0x02221444` 调用 `ScriptableObject__ctor`，已恢复 `: base()`；未改字段。
- `AnalyticsData.cs` 构造函数：IDA `0x021FC96C` 调用 `System.Object__ctor`，已恢复 `: base()`；`OnFocus` 地址 `0x021FC968` 为单条空函数，保留 no-op。
- `AnalyticsEditor.cs` 构造函数：IDA `0x021FC688` 调用 `System.Object__ctor`，已恢复 `: base()`。
- `Analytics/Examples/EventsNamesPerProjectExample.cs` 构造函数：IDA `0x02230890` 调用 `EventsNamesDefault__ctor`，已恢复 `: base()`；静态构造函数已有字段初始化，未改字段。
- `Ads/IronSourceInterstitial.cs:IsInterstitialAvailable`（`0x02230F08`）和 `Ads/IronSourceRewardVideo.cs:IsRewardVideoAvailable`（`0x02232088`）均由 IDA 明确返回 `0`，保留 `false`，判定为真实 no-op/不可用实现。
# 2026-08-09 PostEffectsBase 复核
- `CheckShader(Shader)`（IDA `0x02235AB0`）虽然源码末尾是 `return false`，但 IDA 伪代码明确同样无条件返回 `0`；只在 shader 为空或不支持时禁用组件，保持原逻辑。
- `ReportAutoDisable()`（`0x02235AAC`）为 IDA 空函数，确认真空；`CheckResources()`（`0x02235A3C`）返回 `isSupported`，不是占位返回。
# 2026-08-09 全批次阶段汇总
- Ads/Analytics、Menu/Level/Singleton/Editor、Shop/UI/Animation 三个并行批次已完成 IDA 核验；构造函数中发现的基类调用与字段初始化已补回，未新增或删除字段。
- Unity 最新编译检查：控制台 0 个错误、0 个警告。
- 注意：此前记录中关于 `FirebaseRemoteConfigBase.SetDefaults`、`PrintRemoteConfigValues` 的旧结论已被后续 IDA 汇编核验覆盖；当前源码实现以最新条目为准。
## 2026-08-09 Base/Config/Editor 批次

- `BaseData.cs`：IDA `0x021FC960` 确认构造函数调用 `System.Object__ctor`，已显式恢复 `: base()`；`OnFocus` (`0x021FC958`) 与 `SelectScriptableObject` (`0x021FC95C`) 为真空，保留 no-op。
- `GameDataRemoteSettingsBase.cs`：IDA `0x021F975C` 确认构造函数调用 `System.Object__ctor`，已显式恢复基类构造；`OnValueChangedEditor` (`0x021F9758`) 为真空，保留 no-op。
- `GameConfigBase.cs`：IDA `0x021F976C` 确认构造函数初始化 `ATTPrivacyPolicy = "http://change.com"`、创建 `AdsMediation`、创建 `ResolutionsData` 并将 `IsDebugEnabled` 设为 `true`，已恢复；未新增字段。`OnInspectorInit` (`0x021F9764`) 与 `onATTChanged` (`0x021F9768`) 为真空，保留 no-op。
- `GameConfig.cs`：IDA `0x02223374` 核对已有各变量编辑器初始化，未改字段；补充显式 `: base()` 以对应 `GameConfigBase` 初始化。
- `GameSettings.cs`：IDA `0x021FB8FC` 核对已有编辑器对象初始化，未改字段；补充显式 `: base()`。`OnFocus` (`0x021FB8F8`) 为真空，保留 no-op。
- `DebugVariablesEditor.cs`：IDA `0x022235F0` 确认调用 `DebugVariablesEditorBase__ctor`，补充 `: base()`。
- `ExplodingObjectBase.cs`：IDA `0x0221BA94` 确认构造函数初始化两个列表，现有实现保留；`Start` (`0x0221AE74`) 与 `OnEnable` (`0x0221B61C`) 为真空，保留 no-op。
- `AnimatorBase.cs`：IDA `0x021EF608` 的构造默认值与现有 `Id/DoTweenID/Duration/Ease` 初始化一致；`SetRestartAction` (`0x021EF6B4`) 为真空，保留 no-op。
- `UnityStandardAssets/ImageEffects/PostEffectsBase.cs`：IDA `0x02235E68` 确认构造函数设置 `supportHDRTextures` 与 `isSupported` 为 `true`，现有实现一致；`ReportAutoDisable` (`0x02235AAC`) 为真空，保留 no-op。
# 2026-08-09 全目录最终审计
- 根目录剩余类、工具/管理类、Ads/Analytics/Debugging/UnityStandardAssets 等子目录均已完成 IDA 地址核验。
- 所有发现的空构造函数均已确认：要么已补回 IDA 中的基类/字段初始化，要么 IDA 仅包含基类构造 thunk；未添加或删除字段。
- 所有剩余显式 `return` 均已按 IDA 分类为真实 no-op、条件保护、迭代/搜索失败返回或正常业务返回；未发现仍需还原的占位实现。
- Unity 最终编译与控制台检查：0 个 C# 错误，0 个错误/警告日志。

## 2026-08-10 全量构造字段复核（IDA）

- 已按 IDA 构造函数/静态构造函数地址复核 `KobGamesSDKSlim`、核心根目录类及 `MrWayne` 核心类；现有字段的原生默认值、列表/字典分配、事件空委托和回调默认值已补齐。
- 修正 `MrWayne/AIParameter.cs`：投掷时长、命中概率、随机角度/力度范围、教程范围及 `ForceMinMax` 与 IDA `0x22CF728` 一致。
- 修正 `MrWayne/GeneralUnitParameters.cs`：`IKWeight = 0.45`、`BodyWeight = 1`，与 IDA `0x22CF778` 一致。
- 补齐轨迹、武器效果、输入/菜单/广告/收集物/音频等类中 IDA 明确写入的现有字段初始化；未新增或删除任何字段。
- `AdsMediation.dummyText` 与 `SyncData.DestPath` 的原生代码复制静态运行时字符串指针，未找到可安全替代的固定字面量，因此没有猜测写入；`SyncData.OnFocus` 会按原逻辑设置项目路径。
- 本轮没有运行 Unity；保留给用户在 `G:\WHos\ExportedProject` Play 模式中的实际运行验证。

## 2026-08-11 武器碰撞生命周期复核（IDA）

- `MrWayne/CutterWeapon.cs`：对照 IDA `onHitBody` `0x022DFD14`，确认原生尾部在 `0x022E0320` 仅当 `relativeVelocity < StopVelocity` 时调用 `actionfinished(shouldHide)`；高速插入身体的分支会跳过该调用，随后由 `ThrowableWeapon.Update` 的 ActiveDuration 生命周期正常回收。
- 已修正源码中额外的 `!shouldHide` 条件。此前高速插入分支会立即调用 `actionfinished(false)`，进入 `Finished` 但不触发隐藏/回收，导致对象池中的武器实例逐渐泄漏，后续投掷出现碰撞失效/穿透。
- 未新增或删除字段，未改动 GameConfig 配置；本轮未运行 Unity。

## 2026-08-11 胜利结算流程复核（IDA）

- `Screen_LevelCompletedReward.OnScreenCloseEnd`（IDA `0x0225F5BC`）确认：填充完成时打开 `Screen_LevelCompletedReward_V2`；否则调用 `GameManager.LevelCompleted()`，仅在第三关商店教程条件满足时回到欢迎页并打开商店。
- `Screen_LevelCompletedReward_V2.OnScreenCloseEnd`（IDA `0x0226022C`）确认：按 `IsAnyItemBeingUnlocked(CurrentLevel - 1)` 在 `Screen_LevelCompletedReward` 与 `GameManager.LevelCompleted()` 之间分流，不能额外调用 `ResetGame()`。
- 已删除两个源码中非 IDA 的 `ResetGame()`/额外“无下一个物品”分支，恢复原生结算链路；未新增或删除字段。

- 复核 `MenuScreenBase`：IDA `0x0222FAE0`、`0x0222FC40`、`0x0222FCE0`、`0x0222FE78` 明确要求打开/关闭生命周期分别通知 `MenuManager.OnMenu_ScreenOpenStart/End` 与 `OnMenu_ScreenCloseStart/End`；已补回，修复 `OpenedScreens` 状态未更新导致的结算页面状态异常。

## 2026-08-11 胜利结算小游戏补全（IDA）

- `Screen_LevelCompletedReward_V2.RewardButton`（IDA `0x0225FC60`）：原生在禁用交互、停止跟随和投掷 Dummy 前，先对 `m_CrossHair` 执行 `DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 10, 1f)`；已恢复，移除错误的 `localScale = Vector3.one`。
- `Screen_LevelCompletedReward_V2.OnRagdollHit`（IDA `0x02260474`）：原生在重置 `m_Target.localPosition` 后执行 `DOShakePosition(0.48f, new Vector3(12f, 0f, 0f), 10, 0f, false, true, ShakeRandomnessMode.Full)`；已补回。
- 删除结算关闭回调中的额外日志及重复 `IsAnyItemBeingUnlocked` 查询，使 `Screen_BoosterReward`、`Screen_LevelCompletedReward`、`Screen_LevelCompletedReward_V2` 的结算分流只执行 IDA 中存在的调用。
- 未新增或删除字段；本轮未运行 Unity，等待在 `G:\WHos\ExportedProject` 中复现结算流程。

## 2026-08-11 Unity 运行时报错与武器后续穿透复核（IDA）

- `FirebaseRemoteConfigBase.FirebaseGenericValueType<T>` 构造函数：IDA `0x033D0DA0`（bool）、`0x033D0E08`（int）及 `FirebaseStringValue` 构造链确认只写入 `Key` 与 `m_DefaultValue`，没有调用虚拟 `Value` setter；已删除构造函数中的 `Value = i_DefaultValue`，避免序列化/加载线程调用 `PlayerPrefs`。
- `WeaponShopManager.AddCard(eWeaponType,int)`：IDA `0x022C275C` 确认通过 `GetWeaponData(i_Type).AddCard(max(i_Amount, 0))` 更新 `WeaponData`；已修正，避免与 `WeaponItemData.AddCard` 互相递归造成栈溢出。`WeaponItemData.AddCard` 保持调用管理器的原生分工。
- `MultiHitWeapons.hitOtherElements`：IDA `0x022E4000` 明确分派到虚拟 `onHitElement`，已修正源码中错误的 `updateHit()` 调用。
- `CutterWeapon.onHitElement`：IDA `0x022E0364` 只执行有限次数的反弹计数与墙面特效，不会在普通障碍物命中时粘附并关闭物理；已移除非原生的 `stickWeapon` 路径，恢复 `m_BounceCount <= 3` 分支。
- `WeaponBase.setParent`：IDA `0x022DC064` 与 `CutterWeapon.stickWeapon` 的汇编均使用 `Transform.SetParent(parent, false)`；已修正两个调用点的重载，未增删字段。

## 2026-08-11 胜利结算顺序校正（IDA）

- `Screen_LevelCompletedReward_V2.enableInteraction`（IDA `0x0225F974`）：恢复原生顺序，先 `Dummy.ResetDummy()`，再启用并绑定两个按钮，最后初始化 Spline；未新增或删除字段。
