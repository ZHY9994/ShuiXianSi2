# KobGamesSDKSlim 还原状态

- [已处理] `AnalyticsManagerBase.LogEvent`：依据 IDA `0x02211C58` 还原 Firebase `kv[]` 转详情列表、构造 `GADataContainer` 并发送事件。
- [验证] Unity 脚本编译通过；当前仅剩既有序列化字段重复警告 `m_UnitBase`，无 C# 编译错误。
- [已处理] `Shop/Extensions.Shuffle<T>`：依据 IDA `0x026525C4` 还原 `System.Random` Fisher-Yates 原地洗牌。
- [已处理] `AdsBannerBgPanel.OnValidate`：依据 IDA `0x021FF1C8` 自动补齐子级 `Image`、其 `RectTransform` 及父级 `Canvas` 引用。
- [已处理] `AdsBannerBgPanel.OnDestroy`：依据 IDA `0x021FF37C` 取消 `RemoteSettingsManager.OnFirebaseRemotConfigUpdated` 订阅。
- [IDA确认真空] `AdsBannerBgPanel.OnEnable`、`onFirebaseRemotConfigUpdated`、`UpdateBannerBgPanelImage`：对应 IDA `0x021FF374`、`0x021FF420`、`0x021FF378` 无有效逻辑，保留空体。
- [已处理] `Ads/MAXMediation.OnValidate/Init/onSdkInitializedEvent/Delayed*Init/ShowMediationDebugger`：依据 IDA `0x02233BA8`、`0x02233D20`、`0x02233E7C`、`0x02233F7C`、`0x0223401C`、`0x022340BC`、`0x0223415C` 恢复 MAX 初始化、延迟初始化及调试器调用。
- [IDA确认真空] `AdsMediation.TryApplyManagersPrefab`、`AdsMediation.SelectRemoteConfig`（`0x021FB144`、`0x021FB148`）。
- [IDA确认真空] `AnimatorBase.SetRestartAction`（`0x021EF6B4`）、`SyncData.addFileToArray`（`0x021FCA1C`）。
- [IDA确认真空] `UI/CoinContainer.OnEnable`、`OnDisable`、`updateCoinsText`（`0x022370D8`、`0x022370DC`、`0x022370E0`）。
- [IDA确认真空] `Shop/TItemData.unlock`、`Shop/ShopRewardController.Open`、`SelectItem`（`0x0222CDCC`、`0x0222DB00`、`0x0222DD2C`）。
- [检查] `Ads/IronSourceInterstitial` 与 `IronSourceRewardVideo` 的 `Is*Available => false` 是当前构建中 IronSource 后端的实际 IDA 结果，不作为未还原占位返回；其初始化/展示入口也已由 IDA 核验为真空实现。
- [已处理] `SkinSelector.OnEnable/OnDisable/OnValidate`：依据 IDA `0x0222ABC4`、`0x0222ACC8`、`0x0222B3B8` 恢复皮肤商店选择事件订阅、当前选中项初始化及 `SetRefs()`。
- [IDA确认真空] `Shop/Screen_WeaponShop.OnDisable`、`Shop/TShopTab.OnEnable/OnDisable`、`Shop/TabData.onTabsChanged`、`Shop/WeaponShopData.OnValidate`、`Shop/GetPackageButton.onRVButtonCallback/onRVClaimFailed/onRewardClick`：对应 IDA `0x0222BE14`、`0x0222E0E8`、`0x0222E0EC`、`0x0222C7C4`、`0x0222D0CC`、`0x0222E7AC`、`0x0222E7B0`、`0x0222E7B4` 均无有效指令逻辑。
- [IDA确认真值返回] `Ads/IronSourceInterstitial.IsInterstitialAvailable`（`0x02230F08`）与 `Ads/IronSourceRewardVideo.IsRewardVideoAvailable`（`0x02232088`）均由原生代码直接返回 `false`，不是反编译占位。
- [IDA确认真空] `SoundManagerV1.SoundManagerBase.OnGameReset`（`0x02238580`）。
- [已处理] `RateUsPopUp.OnInspectorGUI`：依据 IDA `0x02213BC8` 恢复 CanvasRoot 刷新逻辑；`OnSelectedSchema`（`0x02213228`）确认真空。
- [核验] `RateUsPopUp.OnValidate`（`0x022137F0`）仅执行 Unity 对象空值比较，无字段写入，保留原空体等价实现。
- [IDA确认真空] `BaseData.OnFocus`、`BaseData.SelectScriptableObject`、`AnalyticsData.OnFocus`（`0x021FC958`、`0x021FC95C`、`0x021FC968`）。
- [IDA确认真空] `AdsMediationEditor.AddMAXNetworkSecurityConfig`、`Load`（`0x021FD008`、`0x021FD038`）。
- [IDA确认真空] `ManagersBase.ReloadScene`、`SelectGameSettings`、`ApplyPrefabInstance`、`AddPrefabToManagers`、`FixAdsManagerV2`（`0x02216018`、`0x0221601C`、`0x02213A14`、`0x0221624C`、`0x02216250`）。
- [IDA确认真空] `Shop/SkinShopData.OnValidate`、`Shop/ShopsData.OnValidate`（`0x0222C00C`、`0x0222E06C`）。
- [已处理] `VersionsData.UpdateVersions`：依据 IDA `0x021FE718` 的逐项名称匹配与版本字段写回流程，恢复已导出版本清单的名称到版本映射；保留空项和未知项不变。
- [IDA确认真空] `Shop/RandomCashButton.onRVButtonCallback/onRVClaimFailed/onRewardClick`（`0x0222EB34`、`0x0222EB38`、`0x0222EB3C`）。
- [IDA确认真空] `Ads/MAXBanner.OnApplicationPause`（`0x02232EB0`）。
- [已处理] `Ads/IronSourceBanner.BannerAdLoadFailedEvent/BannerAdScreenDismissedEvent/BannerAdDestroyedEvent/OnBannerAdScreenPresentedEvent`：依据 IDA `0x02230AFC`、`0x02230C7C`、`0x02230C90`、`0x02230CC4` 恢复 Banner 回调转发；`OnApplicationPause`（`0x02230E6C`）确认真空。
- [IDA确认真空] `IronSourceInterstitial.OnInterstitialLoadedEvent/OnInterstitialLoadFailedEvent`（`0x02230F14`、`0x02230F18`）、`IronSourceRewardVideo.OnRewardedAdLoadedEvent`（`0x02232094`）。
- [已处理] `MAXInterstitial.OnInterstitialLoadedEvent`：依据 IDA `0x022333E8` 恢复 `m_RetryAttempt = 0`。
- [已处理] `Ads/IronSourceMediation.OnValidate`：依据 IDA `0x022316E4` 恢复 `SetRefs()`。
- [IDA确认真空] `Ads/IronSourceMediation.Init`、`ShowMediationDebugger`（`0x0223185C`、`0x02231860`）。
- [IDA确认真空] `IronSourceBanner.Initialize/ShowBanner(BannerEvents)/ShowBanner/HideBanner/DestroyBanner/OnApplicationPause`（`0x02230AE8`–`0x02230AF8`、`0x02230E6C`）。
- [已处理] `AnalyticsManagerBase.onLogServiceStartEvent()`：依据 IDA `0x022069FC` 恢复向 `onLogServiceStartEvent(false)` 重载转发。
- [IDA确认真空] `AnalyticsManagerBase.LogOpenUrlEvent`、`LogOpenFromNotification`、`LogEventCritical`（`0x02211FDC`、`0x022120A8`、`0x022120AC`）。
- [IDA确认真空] `SingletonBase<T>.Awake`、`Singleton<T>.OnAwakeEvent/Start/OnEnable/OnDisable/OnApplicationFocus/OnApplicationPause`（`0x02BF7614`、`0x02BF9B48`–`0x02BF9B5C`）；这些是供派生类覆写的基类钩子。
- [构造函数核验] `DebugVariablesEditor`、`AdConfiguration`、`ShopSettings` 及其数据子类构造函数均仅调用基类构造，无遗漏字段初始化；其中 `GeneralSettings` 的默认字段初始化已存在并与 IDA 一致。
- [IDA确认真空] `VersionsData.setJsonFilePath`（`0x021FE594`）。
- [已处理] `AdsBannerBgPanel.StateUpdatedListeners.add/remove`：依据 `88\\il2cpp.h` 的 `AdsBannerBgPanel__StaticFields.StateUpdatedListeners` 及 IDA `0x021FED1C/0x021FEDF4`，恢复静态委托存储和线程安全合并/移除逻辑。
- [已处理] `MAXMediation`、`IronSourceMediation` 两组事件访问器：依据 `88\\il2cpp.h` 中的实例字段 `OnInterstitialAdReadyEvent`、`OnInitializedCallback` 及 IDA 委托访问器逻辑，恢复线程安全 add/remove。
- [已处理] `AdsMediation.OnUseDummyAdsOnDeviceFromTestSuiteChangeEvent`：依据 `88\\il2cpp.h` 静态字段及 IDA 事件访问器模式恢复线程安全 add/remove。
扫描根目录：`G:\\WHos\\ExportedProject\\Assets\\Scripts\\Assembly-CSharp\\KobGamesSDKSlim`

扫描时间：2026-08-09；最近更新：2026-08-09

## 规则

- IDA 是唯一真相源；地址来自 `G:\\WHos\\88\\il2cpp.json`。
- 先处理空方法体、占位返回和构造/静态构造中的字段初始化。
- 字段结构不增不删；只写入 IDA 能确认的字段初始化和方法逻辑。
- IDA 确认为真实 no-op 的方法保留 no-op，并在本表标记为“IDA确认无逻辑”。
- 每完成一个方法，立即更新本表的状态、地址和备注。

## 初始扫描结果

正则扫描发现大量 `{ }`，其中包含合法 no-op、编辑器占位类、平台适配空实现和真实反编译丢失的方法；因此不能按文本匹配直接批量改写。当前明确出现占位返回的重点文件：

| 文件 | 重点 | 状态 |
|---|---|---|
| `AnalyticsManagerBase.cs` | 多个 `return null`、`default(GameLoopEventsData)`，以及事件日志空方法 | 待按 IDA 逐方法还原 |
| `UnityStandardAssets/ImageEffects/PostEffectsBase.cs` | 多个返回值需区分基类合法默认值与丢失逻辑 | 待按 IDA 确认 |
| `Utils.cs` | 反射/资源查找中的 null 是边界返回，需按地址核验 | 待核验 |
| `Ads/IronSourceInterstitial.cs` | 平台适配方法与 `return false` | 待按 IDA 确认 |
| `Ads/IronSourceRewardVideo.cs` | 平台适配方法与 `return false` | 待按 IDA 确认 |

明确发现空构造方法（需要核对 IDA 是否有字段初始化）：

| 文件 | 构造方法 | 状态 |
|---|---|---|
| `AnalyticsData.cs` | `AnalyticsData()` | 待核对 |
| `AddressablesEditor.cs` | `AddressablesEditor()` | 待核对 |
| `AdsManager.cs` | `AdsManager()` | 待核对 |
| `BaseData.cs` | `BaseData()` | 待核对 |
| `CameraVariablesEditor.cs` | `CameraVariablesEditor()` | 待核对 |
| `CultureInfoManager.cs` | `CultureInfoManager()` | 待核对 |
| `DictionaryPoolTypeListPoolConfig.cs` | `DictionaryPoolTypeListPoolConfig()` | 待核对 |
| `GamePlayVariablesEditorBase.cs` | `GamePlayVariablesEditorBase()` | 待核对 |
| `HapticsVariablesEditorBase.cs` | `HapticsVariablesEditorBase()` | 待核对 |
| `InputVariablesEditor.cs` | `InputVariablesEditor()` | 待核对 |
| `PlayerVariablesEditor.cs` | `PlayerVariablesEditor()` | 待核对 |
| `PlayerVariablesEditorBase.cs` | `PlayerVariablesEditorBase()` | 待核对 |
| `Shop/ShopSettings.cs` | `AppearData()`、`RandomUnlockData()`、`TweensData()`、`PunchTweenParameters()` | 待核对 |

## 并行处理批次

当前环境未提供可调用的子智能体调度接口，因此按最多三个并行批次建立任务边界；每批完成后更新本文件：

### 批次 A：核心逻辑与菜单

范围：`AnalyticsManagerBase.cs`、`MenuManagerV1/`、`ExtendedButton*.cs`、`AnimatorBase.cs`、`Singleton*.cs`、`Utils.cs`。

状态：进行中。已完成 `ExtendedButton.cs`、`MenuScreenBase.cs` 的首轮 IDA 还原；`AnalyticsManagerBase.cs` 尚未完成。

### 批次 B：广告/分析适配

范围：`Ads/`、`AdsMediation*.cs`、`AdsManager*.cs`、`InterstitialHandler.cs`、`FirebaseRemoteConfig*.cs`。

状态：部分完成。已按 IDA 处理若干 MAX/IronSource 基础逻辑；剩余空适配方法需逐项判定是否为真实 no-op。

### 批次 C：数据、商店、收集物与 UI

范围：`Shop/`、`Collectable/`、`UI/`、`Animation/`、`RateUsPopUp.cs`、`SkinSelector.cs`、`VideoAdPlayer.cs`。

状态：部分完成。已完成多项收集物、商店按钮、动画和 `VideoAdPlayer.cs`；`Shop/` 与 `UI/` 仍需继续扫描。

## 已完成记录

| 文件 | 方法/范围 | IDA/验证 | 状态 |
|---|---|---|---|
| `ExtendedButton.cs` | 生命周期、指针/拖拽、Setup、事件、交互状态、图形状态 | `0x0221DBA4` 至 `0x0221EBB0`；Unity 编译通过 | 已处理 |
| `MenuManagerV1/MenuScreenBase.cs` | 引用收集、打开/关闭、淡入淡出、按钮交互与重置 | `0x0222F7B8`、`0x0222FA48`、`0x0222FA5C`、`0x0222FB1C`、`0x0222FCD8`、`0x0222FD8C`、`0x0222FF20`、`0x02230054`、`0x02230104` | 已处理，待运行时回归 |
| `VideoAdPlayer.cs` | `Start`、`Update`、`Reset` | `0x02221C24`、`0x02221CD8`、`0x02221DC4`；Unity 编译通过 | 已处理 |

## 构造方法扫描处理记录（第一批）

| 地址 | 方法 | IDA 结论 | 源码处理 |
|---|---|---|---|
| `0x021F13BC` | `CultureInfoManager()` | 仅调用 `MonoBehaviour` 基类构造 | 原空构造可保留，已核实 |
| `0x021F9D20` | `GamePlayVariablesEditorBase()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x021FA020` | `PlayerVariablesEditorBase()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x021FA1D0` | `HapticsVariablesEditorBase()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x021FC960` | `BaseData()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x021FC96C` | `AnalyticsData()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x022162C8` | `DictionaryPoolTypeListPoolConfig()` | 转发到 `UnitySerializedDictionary<ePoolType,PoolConfig>` 基类构造 | 原空构造可保留，已核实 |
| `0x022235F8` | `InputVariablesEditor()` | 转发到 `InputVariablesEditorBase` 构造 | 原空构造可保留，已核实 |
| `0x022238D8` | `PlayerVariablesEditor()` | 转发到 `PlayerVariablesEditorBase` 构造 | 原空构造可保留，已核实 |
| `0x022238E0` | `CameraVariablesEditor()` | 转发到 `CameraVariablesEditorBase` 构造 | 原空构造可保留，已核实 |
| `0x022238F0` | `AddressablesEditor()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x02223E5C` | `AdsManager()` | 初始化类型后调用 `AdsManagerBase` 构造 | 原空构造可保留，已核实 |
| `0x0222E078` | `ShopSettings()` | 仅调用 `ScriptableObject` 构造 | 原空构造可保留，已核实 |
| `0x0222E080` | `ShopSettings.GeneralSettings()` | 写入两个 bool 字段为 `true` | 源码已写入，已核实 |
| `0x0222E090` | `AppearData()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x0222E098` | `RandomUnlockData()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x0222E0A0` | `TweensData()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |
| `0x0222E0A8` | `PunchTweenParameters()` | 仅调用 `System.Object` 构造 | 原空构造可保留，已核实 |

## 当前 Unity 状态

- 最近脚本编译无 C# 错误。
- 仍有原项目序列化警告：`Base(UnitStateMachine) m_UnitBase` 字段重复；本任务不改字段结构。

## 最近更新

- 已完成三批构造方法的 IDA 核对，共 18 个构造入口。
- 其中只有 `ShopSettings.GeneralSettings()` 在 IDA 中写入字段；源码已确认写入 `IsShowLockedItemSilhouettes = true` 和 `IsCloseShopOnClickingSelectedItem = true`。
- 其余构造入口只调用基类构造，没有额外字段初始化；未人为增加字段或伪造初始化。
- `DictionaryPoolTypeListPoolConfig()` 的 IDA 结果为跳转到泛型基类构造，源码保持空构造以保留 C# 基类构造链。
- `AnalyticsManagerBase` 的 `getDetailsOnLevelLoaded`（`0x02207908`）、`convertAdsAvailabilityResult`（`0x0220B408`）和 `LogEvent`（`0x02211C58`）均被 IDA 判定为真实逻辑，不能保留当前占位实现；已加入批次 A 待还原队列。
- `AnalyticsManagerBase.LogOpenUrlEvent`（`0x02211FDC`）被 IDA 确认为真实 no-op，源码空体保留并标记为合法 no-op。
- 由于当前环境没有子智能体调度接口，三批任务由主代理按 A/B/C 边界顺序执行；每次 IDA 核对后立即写入本表。
- 静态构造第一批已核对：`Constants` `0x021F0A20` 的路径字段初始化已存在且与 IDA 一致；`RemoteSettingsManager` `0x022184D8`、`StorageManagerBase` `0x0221AA28`、`StorageManager` `0x0222A960` 的静态事件在当前 C# 中已用现有事件声明承载，未新增字段；其 IDA 委托初始化属于编译器生成事件存根，继续保持事件语义并列入运行时回归验证。
- `AnalyticsManagerBase` 的地址映射出现函数名、反编译标题与汇编边界不一致：`0x0220B408` 的汇编首条注释为 `convertAdsAvailabilityResult`，但函数元数据却显示 `logInterstitialAvailability`，并且 `0x0220B500` 已进入 `getDetailsOnLogInterstitialAvailability`。该项已标记为“IDA 元数据冲突”，暂不写入，避免把一个方法的伪代码误写入另一个方法。
- `UnityStandardAssets/ImageEffects/PostEffectsBase.cs` 已核对：`CheckResources`、`CheckSupport`、`Dx11Support`、`CheckShader` 的返回逻辑与 IDA 一致；`ReportAutoDisable`（`0x02235AAC`）在 IDA 中确认为真实 no-op，未改写。
- `Ads/IronSourceInterstitial.cs` 的 `Initialize`、`LoadInterstitial`、`ShowInterstitial`（`0x02230F00`–`0x02230F10`）以及 `Ads/IronSourceRewardVideo.cs` 的 `Initialize`、`ShowRewardVideo`（`0x02232084`、`0x02232090`）均经 IDA 确认为真实 no-op；`return false` 的可用性实现也符合当前无 IronSource 运行时接入状态，未伪造广告调用。

## 静态构造扫描

`il2cpp.json` 中确认存在静态构造入口的主要类型已纳入扫描队列。编译器生成的 `<>c` 和 `<.cctor>b__*` 回调不作为字段初始化入口单独改写，必须随所属静态构造一起核对。

| 批次 | 类型/入口地址 | 状态 |
|---|---|---|
| A | `Constants` `0x021F0A20`；`ExtensionMethods` `0x021F37B0`；`Utils/<>c` `0x021F81DC`；`RemoteSettingsManager` `0x022184D8`；`StorageManagerBase` `0x0221AA28`；`StorageManager` `0x0222A960` | 待逐项读取 IDA |
| B | `AdsMediation` `0x021FB23C`；`AdsBannerBgPanel` `0x021FF8E8`；`AdsManagerBase` `0x02203ADC`；`iAPManagerBase` `0x02212D34`；`RateUsPopUp` `0x02214A54`；`IronSourceMediation/<>c` `0x02231FCC`；`MAXMediation/<>c` `0x022348BC` | 待逐项读取 IDA |
| C | `GameManagerBase` `0x0223C194`；`Shop.Extensions` `0x0222D87C`；`SkinShopData/<>c` `0x0222C740`；`WeaponShopData/<>c` `0x0222D7F8`；Analytics pooled 类型 `0x03864E38`–`0x03867C78` | 待逐项读取 IDA |

本次构造扫描的第一批已完成；静态构造扫描已登记但尚未宣称完成，避免把静态字段初始化遗漏在“空方法体”扫描之外。

## 下一步

1. 读取 `AnalyticsManagerBase.cs` 所有占位返回/空方法对应的 IDA 地址。
2. 先还原构造/静态构造中的字段初始化，并即时更新“已完成记录”。
3. 再按 A/B/C 批次继续处理真实逻辑空方法；IDA 确认 no-op 的条目标记为“IDA确认无逻辑”。
## 本轮新增记录
- `RateUsPopUp.cs` 已按 IDA 还原：`OnRateUsEvent` 事件存储、星星默认状态刷新、`OnEnable`/`OnDisable` 按钮与关卡事件注册/移除、关卡加载触发、Later/No/Yes 按钮逻辑、黑名单追加。对应地址：`0x02206634`、`0x02213234`、`0x02213C28`、`0x02213FDC`、`0x022142E0`、`0x022146AC`、`0x02214740`、`0x022147DC`、`0x02214968`。
- `OnSelectedSchema` 已由 IDA 确认为运行时 no-op；`OnInspectorGUI` 属编辑器路径，未伪造运行时逻辑。
- 2026-08-09: Re-scanned all C# files for empty bodies and placeholder returns. Constructor-only empty bodies were cross-checked against `il2cpp.json`; confirmed constructor chains with no field writes are retained as valid C# base-constructor bodies. `AnalyticsManagerBase.cs` remains the main unresolved placeholder cluster.
- 2026-08-09: IDA assembly confirmed `convertAdsAvailabilityResult` occupies `0x0220B408-0x0220B4A0` despite the decompiler function-boundary label collision. Assembly logic: `Success=0`, `Not_Available=1`, `Waited=2`, with the returned strings selected from static `EventsSO`/analytics constants; source was not guessed because those static string identities are not exposed in the reconstructed C# fields.
- 2026-08-09: IDA decompile confirmed `AnalyticsManagerBase.onLogServiceStartEvent(bool)` at `0x02206A04` is real logic (disabled-event guard, game-launch count, pooled GA data, details dispatch, `sendEvent`); it is queued for exact restoration after resolving the analytics constants mapping. The one-argument `0x022069FC` is only a thunk into it.
- 2026-08-09: `AnalyticsManagerBase.convertAdsAvailabilityResult` 已按 IDA 汇编 `0x0220B408-0x0220B4A0` 还原。枚举值来自 `eAdsAvailabilityResult.cs`，返回 `EventsNamesDefault` 中对应 `Success` / `NotAvailable` / `Waited` 的 `.Name`；未新增字段。
- 2026-08-09: 已还原 `AnalyticsManagerBase.onLogServiceStartEvent(bool)`、`onLevelLoaded`、`onLevelStarted`、`onLevelCompleted` 的入口逻辑：IDA 对应事件名、禁用事件检查、等级/启动次数 GA 数据、详情分派和 `sendEvent` 调用均已写入；未新增字段。
- 2026-08-09: 已按 IDA 还原 `onLevelFailedNoContinue` 与 `onLevelContinue`：事件禁用检查、关卡号 GA 数据、详情回调和 `sendEvent` 均已写入。
- 2026-08-09: IDA 确认 `onIAP_PurchaseFailed` (`0x022106F8`)、`onIAP_PurchaseRestoreComplete` (`0x02210A30`)、`onIAP_PurchaseRestoreFailed` (`0x02210D68`) 为真实 no-op，保持空体，不伪造 IAP 逻辑。
- 2026-08-09: 已按 IDA `0x022118E0` 还原 `onRateUs`：RateUs 事件禁用检查、结果/星数 GA 数据、详情回调和 `sendEvent`。
- 2026-08-09: 已还原 `logInterstitialOpen` 与 `logRewardVideoOpen` 的 IDA 事件入口：广告事件名、禁用检查、广告开始值、placement、详情回调与 `sendEvent`。
- 2026-08-09: 已按 IDA 汇编 `0x02207848-0x02207900` 还原 `convertLevelDataToGameLoopEventsData`：逐字段映射 `LevelNumber/Name/Count/Difficulty/Loop/Random/Type/GameMode`，并保留 IDA 的 `LevelLoop <= 1 ? 1 : LevelLoop` 规则。
- 2026-08-09: IDA 确认 `LogOpenUrlEvent` (`0x02211FDC`) 与 `LogEventCritical` (`0x022120AC`) 为真实 no-op；已还原 `LogAppMetricaEvent` (`0x02211F60`) 的 Tuple 转换/发送，以及 `LogLevelProgress` (`0x02211FE0`) 对 `LogEvent` 的转发。
- 2026-08-09: 已还原 `getDetailsOnLogServiceStartEvent`：按 IDA 读取的 `GameLaunchCount`、`HighScoreLevel`、`TotalAttempts`、`TotalVideosWatched` 构造 `EventNameObjectPair` 列表。
- 2026-08-09: 已还原 `getDetailsOnApplicationPause`：按 IDA 的暂停状态分支，写入 `k_Type_App_PauseStatus` 与 `k_Value_General_True/False` 键值对。
- 2026-08-09: 已按 IDA `0x02211A24` 还原 `getDetailsOnRateUs`（ShowReason、Yes 时星数，否则 0）；并按 IDA `0x02207908` 的字段顺序还原 `getDetailsOnLevelLoaded` 的 8 个关卡详情键值对。
- 2026-08-09: 已还原四组关卡详情方法：`getDetailsOnLevelStarted`、`getDetailsOnLevelCompleted`、`getDetailsOnLevelFailedNoContinue`、`getDetailsOnLevelContinue`。按 IDA 栈布局区分基础字段与完成/失败的时间、进度、继续次数、Win/Lose 结果字段。
- 2026-08-09: 已按 IDA `0x0220A20C` 还原 `logBannerShownSuccess`：Banner 事件、禁用检查、placement、成功值、详情回调和 `sendEvent`。
- 2026-08-09: 已按 IDA `0x022110A0` 还原 `onGameAwakeAfterGameClosed`：游戏关闭恢复事件、关卡号、`Game_Closed` 值、禁用检查、详情回调和 `sendEvent`。
- 2026-08-09: 已按 IDA `0x022100CC` 还原 `onIAP_PurchaseComplete` 的主入口：产品/定义检查、成功事件、storeSpecificId、禁用检查、详情回调和 `sendEvent`。
- 2026-08-09: 已还原 4 个 IAP 详情占位返回：`getDetailsOnIAP_PurchaseComplete`、`getDetailsOnIAP_PurchaseFailed`、`getDetailsOnIAP_PurchaseRestoreComplete`、`getDetailsOnIAP_PurchaseRestoreFailed`。依据 IDA 的商品元数据访问，恢复价格、标题、货币、商品 ID、receipt、transaction ID、是否有 receipt 等字段。
- 2026-08-09: 已清除 11 个广告详情 `return null` 占位：Banner、Interstitial、RewardVideo 的成功/失败/可用性/打开/关闭详情，统一按 IDA 使用的 placement、result、error code/description 键构造列表。
- 2026-08-09: 已补齐广告入口空体：Banner 失败、Interstitial 可用性/成功/失败、RewardVideo 可用性/成功/关闭/失败，均写入 IDA 对应事件、GA 数据、禁用检查、详情回调和 `sendEvent`。
- 2026-08-09: 全目录复扫后，`AnalyticsManagerBase` 已无 `return null` 占位；剩余扫描命中均为 IDA 已确认 no-op 的 SDK 返回值、合法空构造函数、协程 `yield return null`、空值保护分支或正常查找失败返回，不作为伪占位处理。
- 2026-08-09: `getDetailsOnGameAwakeAfterLostFocus` 已按 IDA 真实字段用途恢复为关卡详情加 `Game_Closed` 结果。
