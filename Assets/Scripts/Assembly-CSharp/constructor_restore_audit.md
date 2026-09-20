# 构造函数/静态构造函数还原清单

扫描范围：`Assets/Scripts/Assembly-CSharp/**/*.cs`

扫描结果：

- 检出空实例构造函数或仅有 `base()` 的构造函数：约 100 个（包含数据类、编辑器类、UI 组件和管理器）。
- 检出显式静态构造函数：22 个。
- 本清单只记录候选项；是否需要写入字段必须逐项以 IDA 构造函数地址确认。

## 已确认并已修复

| 类 | IDA 地址 | 结果 |
|---|---:|---|
| `KobGamesSDKSlim.StorageManagerBase` | `0x0221A9B0` | 写入 `DefaultStartlevel = 1`、`m_IsFirstAttempt = true` |
| `Wayne.Game.Shops.WeaponShopManager` 静态构造函数 | `0x022C2A28` | 初始化 9 个静态事件委托，已写回 |
| `Screen_SpecialOffer` 静态构造函数 | `0x02246E90` | 初始化 `OnSpecialOfferOpen`、`OnSpecialOfferClose`，已写回 |
| `Wayne.Game.Shops.SkinShopTabController` 静态构造函数 | `0x022C3A2C` | 初始化 `OnTabInitialized`，当前 C# 已还原 |
| `Wayne.Game.Shops.WeaponShopTabController` 静态构造函数 | `0x022C4EB0` | 初始化 `OnTabInitialized`，当前 C# 已还原 |

## 已确认无需字段写入

| 类 | IDA 地址 | 结论 |
|---|---:|---|
| `Wayne.Game.Shops.SkinShopManager` 实例构造函数 | `0x022C0490` | 仅调用 `Singleton` 基类构造函数 |
| `Wayne.Game.Shops.SkinShopManager` 静态构造函数 | `0x022C04FC` | 初始化 7 个静态事件；当前 C# 已有对应委托初始化 |
| `Wayne.Game.Shops.WeaponItemData` | `0x022C5668` | 仅调用 `TItemData` 基类构造函数 |
| `KobGamesSDKSlim.PoolManager` | `0x02228A6C` | 仅调用 `PoolManagerBase` 基类构造函数 |
| `KobGamesSDKSlim.Shop.TShopData` | `0x0222C738` | 仅调用 `System.Object` 构造函数 |
| `KobGamesSDKSlim.Shop.ShopsData` | `0x0222E070` | 仅调用 `ScriptableObject` 构造函数 |
| `KobGamesSDKSlim.Collectable.CollectableManager` | `0x0223D460` | 仅调用 `Singleton` 基类构造函数 |
| `BackgroundManager` | `0x02191BDC` | 仅调用 `Singleton` 基类构造函数 |
| `BackGroundData` | `0x02191CB0` | 仅调用 `System.Object` 构造函数 |
| `BackgroundPart` | `0x02191E5C` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `HealthController` | `0x02194774` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `DeckItem` | `0x02195478` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `Screen_Booster` | `0x02251428` | 仅调用 `MenuScreenBase` 基类构造函数 |
| `SkinShopTabController` 实例构造函数 | `0x022C3A24` | 仅调用 `TShopTabController` 基类构造函数 |
| `WeaponShopTabController` 实例构造函数 | `0x022C4EAC` | 仅调用 `TShopTabController` 基类构造函数 |
| `ShopSettings` | `0x0222E078` | 仅调用 `ScriptableObject` 基类构造函数 |
| `Face` | `0x0222E178` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `Skin` | `0x0222E3B0` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `WeaponRarityItem` | `0x022C5DB0` | 仅调用 `TShopRarityItem` 基类构造函数 |
| `WeaponRarityItemHandler` | `0x022C5E8C` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `SkinRarityItem` | `0x022C9478` | 仅调用 `TShopRarityItem` 基类构造函数 |
| `SkinRarityItemHandler` | `0x022C971C` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `GameConstants` | `0x02223354` | 仅调用 `System.Object` 构造函数 |
| `SDKVersionInfo` | `0x021FCBE8` | 仅调用 `System.Object` 构造函数 |
| `VersionInfo` | `0x021FEC30` | 仅调用 `System.Object` 构造函数 |
| `SelectDeckItem` | `0x022BD45C` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `UnitManager` | `0x021A36BC` | 初始化 `m_ActiveUnits`、`PlayerUnits`、`EnemyUnits` 三个列表；当前 C# 已还原 |
| `WeaponSelectionManager` | `0x0224A078` | 初始化 `m_ActiveItems` 列表；当前 C# 已还原 |
| `Screen_LevelCompletedReward` | `0x0225F7CC` | 写入 `m_MoneyToReceive = 180`；当前 C# 已还原 |
| `Screen_LevelCompletedReward_V2` | `0x02260554` | 写入 `m_BaseMiniGameReward = 50`；当前 C# 已还原 |
| `GameplayController` | `0x0219A45C` | 仅调用 `Singleton` 基类构造函数 |
| `Screen_BoosterReward` | `0x02251F30` | 仅调用 `MenuScreenBase` 基类构造函数 |
| `Screen_Transition` | `0x02256078` | 仅调用 `MenuScreenBase` 基类构造函数 |
| `SkinItemData` | `0x022C346C` | 仅调用 `TItemData` 基类构造函数 |
| `TRarityItem` | `0x022C3240` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `TShopRarityItem` | `0x022C3344` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `GameplayController` 静态构造函数 | `0x0219A4C8` | 初始化 `OnStateChange` 委托，已写回 |
| `WeaponSelectionManager` 静态构造函数 | `0x0224A100` | 初始化 `OnSetWeapon`、`PlayerSetWeapon`、`OnWeaponDeckOpened`，已写回 |
| `GameManagerBase` 静态构造函数 | `0x0223C194` | 初始化游戏流程事件、关卡事件和屏幕事件委托，已写回 |
| `DeckManager` 实例构造函数 | `0x021964E0` | 初始化 `Items` 列表；C# 属性初始化已等价还原 |
| `DeckManager` 静态构造函数 | `0x021965A0` | 初始化 `OnDeckOpen`、`OnDeckClose`、`OnDeckUpdate`，已写回 |
| `LevelManager` | `0x021992B4` | 写入 `m_FirstLaunch = true`，已写回 |
| `TrajectoryManager` | `0x021A0F8C` | 初始化 `m_PreviewList`、`m_CorrectDotList`；当前 C# 已还原 |
| `Screen_LevelCompleted` | `0x022531B4` | 写入 `m_MoneyToReceive = 90`，已写回 |

## 本批次新增核对（Assembly-CSharp 核心逻辑）

| 类 | IDA 地址 | 结论 |
|---|---:|---|
| `BGPartData` | `0x02191CB8` | 写入 `SpeedCoefficient = 0.1`，已改为字段初始化器 |
| `AISkinData` | `0x021A24B0` | 写入 `Color = (1,0,0,1)`，已改为字段初始化器 |
| `AudioClipData` | `0x02190948` | 写入 `Volume = 1`，已改为字段初始化器 |
| `BoosterRevealAnimData` | `0x02192EA0` | 写入 `BoosterRewardCloseDelay = 0.2`、`ScreenFadeDuration = 0.5`、`ScreenFadeEase = 3`，已由字段初始化器保留 |
| `DamagePopUpDatas` | `0x0219477C` | 写入 `SpawnOffset = 0.65`，已改为字段初始化器 |
| `HapticData` | `0x02198558` | 写入 `IsWork = true`、`HapticType = 4`，已改为字段初始化器 |
| `AchievedLevelEventData` | `0x0219D530` | 初始化 `m_DataList` 和 16 项 `m_EventLevels`，已改为字段初始化器 |
| `MrWayne.AIParameter` | `0x022CF728` | 写入 5 个参数字段，已改为字段初始化器 |
| `MrWayne.GeneralUnitParameters` | `0x022CF778` | 写入 9 个参数字段及 `DefaultHitEvent`，已改为字段初始化器 |
| `MrWayne.PlayerParameter` | `0x022CF8CC` | 写入 3 个参数字段，已改为字段初始化器 |
| `MrWayne.LauncherWeapons` | `0x022E5B68` | 初始化 `m_ActiveFragments`，已改为字段初始化器 |
| `MrWayne.UnitPhysics` | `0x022D6CD4` | 初始化 `m_BodyData`，已改为字段初始化器 |
| `MrWayne.UnitState` | `0x022D0AC4` | 初始化 `m_Hits = new RaycastHit[3]`，已改为字段初始化器 |
| `MrWayne.WeaponBase` | `0x022DCC24` | 初始化 `m_DamageProfile`、`m_EffectControllers`，已改为字段初始化器 |
| `MatchMaking` 静态构造函数 | `0x021AA974` | 三个事件当前已是字段委托初始化器，无需重复写入 |
| `iAPManager` 静态构造函数 | `0x0224F424` | 三个购买事件当前已是字段委托初始化器，无需重复写入 |
| `Screen_BoosterReward` 静态构造函数 | `0x02251F38` | `OnBoosterOpen` 当前已是字段委托初始化器，无需重复写入 |
| `Screen_Transition` 静态构造函数 | `0x02256080` | `TransitionMode = 2` 当前已是字段初始化器 `HideAndShow` |
| `WeaponRaritySelector` | `0x02244B68` | 仅调用 `MonoBehaviour` 基类构造函数，无字段写入 |
| `Wayne.Game.Shops.TShopTabController` | `0x022C3D7C` | 初始化 `m_Tabs` 字典，已改为字段初始化器 |
| `Wayne.Game.Shops.TTabButton` | `0x022C4710` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `Wayne.Game.Shops.TTabButtonController` | `0x022C48E4` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `Wayne.Game.Shops.WeaponItemData` | `0x022C5668` | 仅调用 `TItemData` 基类构造函数 |
| `Wayne.Game.Shops.Weapons.WeaponShopTab` | `0x022C8194` | 仅调用 `TShopTab` 基类构造函数 |
| `Wayne.Game.Shops.Weapons.WeaponMenuButton` | `0x022BB390` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `Wayne.Game.Shops.Weapons.WeaponRarityItem` | `0x022C5DB0` | 仅调用 `TShopRarityItem` 基类构造函数 |
| `Wayne.Game.Shops.Weapons.WeaponRarityItemHandler` | `0x022C5E8C` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `Wayne.Game.Shops.Skins.SkinRarityItem` | `0x022C9478` | 仅调用 `TShopRarityItem` 基类构造函数 |
| `Wayne.Game.Shops.Skins.SkinRarityItemHandler` | `0x022C971C` | 仅调用 `MonoBehaviour` 基类构造函数 |
| `Spawner` 静态构造函数 | `0x021871DC` | 初始化 `PoolList`，已改为静态字段初始化器 |
| `SpawnerBreeder` 静态构造函数 | `0x02187518` | 初始化 `SpawnPoolDict`，已改为静态字段初始化器 |
| `WayneStaticFunctions` 静态构造函数 | `0x02258E70` | 初始化 `m_ComponentCache`，已改为静态字段初始化器 |
| `Wayne.Game.Utils.ProjectUtils` 静态构造函数 | `il2cpp.json` 对应静态初始化 | 初始化两个颜色字典，已改为静态字段初始化器 |
| `Wayne.Game.Tutorials.TutorialUtils` 静态构造函数 | `il2cpp.json` 对应静态初始化 | 初始化 9 个教程字符串，已改为静态字段初始化器 |
| `Wayne.Game.Shops.Weapons.WeaponTabButton` 静态构造函数 | `il2cpp.json` 对应静态初始化 | `OnTabSelected` 委托已改为事件字段初始化器 |
| `Wayne.Game.Shops.Weapons.WeaponShopTabButtonController` 静态构造函数 | `il2cpp.json` 对应静态初始化 | `OnTabUpdate` 委托已改为事件字段初始化器 |
| `Wayne.Game.Shops.SkinShopManager` 静态构造函数 | `0x022C04FC` | 7 个商店事件已改为字段委托初始化器 |
| `Wayne.Game.Shops.SkinShopTabController` 静态构造函数 | `0x022C3A2C` | `OnTabInitialized` 已改为字段委托初始化器 |
| `Wayne.Game.Shops.WeaponShopTabController` 静态构造函数 | `0x022C4EB0` | `OnTabInitialized` 已改为字段委托初始化器 |
| `Wayne.Game.Shops.Skins.SkinShopItem` 静态构造函数 | `il2cpp.json` 对应静态初始化 | `OnUnlock` 已改为字段委托初始化器 |
| `Wayne.Game.Shops.Skins.SkinShopTabButtonController` 静态构造函数 | `0x022CC0FC` | `OnTabUpdate` 已改为字段委托初始化器 |
| `Wayne.Game.Shops.Skins.SkinTabButton` 静态构造函数 | `0x022CC620` | `OnTabSelected` 已改为字段委托初始化器 |
| `Wayne.Game.Shops.Weapons.WeaponShopItem` 静态构造函数 | `0x022C7658` | `OnUnlock` 已改为字段委托初始化器 |
| `CryptoExtensions` 静态构造函数 | `0x0217E4A0` | IDA 确认写入 `RandomGenerator`、1024 字节 `DefaultPublicKey`（`RuntimeHelpers.InitializeArray`）和 `m_PublicKey2Str`；C# 已保留等价的 1024 字节数组初始化，原始 metadata blob 未凭空伪造 |

## 优先核查候选

这些类会影响运行时逻辑，优先于纯数据/编辑器类：

- `Wayne.Game.Shops.WeaponShopManager`
- `Wayne.Game.Shops.SkinShopManager`
- `Wayne.Game.Shops.WeaponItemData`
- `KobGamesSDKSlim.Shop.ShopsData`
- `KobGamesSDKSlim.Shop.TShopData`
- `KobGamesSDKSlim.GameManager`
- `KobGamesSDKSlim.GameManagerV1.GameManagerBase`
- `KobGamesSDKSlim.MenuManagerV1.MenuManager`
- `KobGamesSDKSlim.PoolManager`
- `KobGamesSDKSlim.Collectable.CollectableManager`
- `Wayne.Game.Shops.Weapons.WeaponRarityItem`
- `Wayne.Game.Shops.Skins.SkinRarityItem`

## 处理规则

1. 先从 `il2cpp.json` 获取构造函数/静态构造函数地址。
2. IDA 反编译无法还原时读取对应汇编。
3. 只有确认存在字段写入、委托初始化或集合初始化时才修改 C#。
4. 空构造函数若 IDA 也为空，保留空体，不为消除空体而添加无依据代码。
5. 不新增、不删除字段。

## 本批次（UI/数据/菜单/第三方插件，2026-08-10）

| 类 | IDA 地址 | 结果 |
|---|---:|---|
| `AudioClipData` | `0x02190948` | `Volume = 1f`，已改为字段初始化器 |
| `BGPartData` | `0x02191CB8` | `SpeedCoefficient = 0.1f`，已改为字段初始化器 |
| `BoosterData.SlotData` | `0x021922E0` | `Random = true`、`AddCount = 3`，已改为字段初始化器 |
| `BoosterData.SlotDatas` | `0x021922F8` | `Slots = new SlotData[5]`，已改为字段初始化器 |
| `BoosterRevealAnimData` | `0x02192EA0` | `BoosterRewardCloseDelay = 0.2f`、`ScreenFadeDuration = 0.5f`、`ScreenFadeEase = (Ease)3`，已改为字段初始化器 |
| `DifficultyConfig` | `0x02193314` | `PowerConversion = 0.3f`、`WinCoef = 0.5f`、`LoseCoef = 0.1f`、`DifficultyIncreaseOnWin = 1.05f`、`DifficultyIncreaseOnWinNew = 5f`，已改为字段初始化器 |
| `TimeManagerData` | `0x02194968` | `UseSlowMo = true`、`TimeScaleLerpSpeed = 1f`、`TargetScale = 0.1f`、`TimeScaleDuration = 0.4f`，已改为字段初始化器 |
| `AISkinData` | `0x021A24B0` | `Color = new Color(1f, 0f, 0f, 1f)`，已改为字段初始化器 |
| `CardInfoManager` 静态构造 | `0x021A79B4` | `OnCardInfoOpen` 委托初始化，已改为字段初始化器 |
| `LocalizationEventCaller` 静态构造 | `0x0219F700` | `OnLocalChange` 委托初始化，已改为字段初始化器 |
| `TimeScaleEvent` 静态构造 | `0x02257474` | `OnEvent` 委托初始化，已改为字段初始化器 |
| `FreezeFrameEvent` 静态构造 | `0x02257840` | `OnEvent` 委托初始化，已改为字段初始化器 |
| `Spawner` 静态构造 | `0x021871DC` | `PoolList = new Dictionary<string, SpawnerPool>()`，当前源码已有等价字段初始化 |
| `SpawnerBreeder` 静态构造 | `0x02187518` | `SpawnPoolDict = new Dictionary<string, SpawnerPool>()`，当前源码已有等价字段初始化 |
| `ShopDeck` 静态构造 | `0x02197F04` | `OnShopDeckPreClose` 委托初始化，当前源码已有等价字段初始化 |
| `MatchMaking` 静态构造 | `0x021AA974` | 三个匹配事件委托初始化，当前源码已有等价字段初始化 |
| `OpenOfferScreenButton` 静态构造 | `0x022460EC` | 点击事件委托初始化，当前源码已有等价字段初始化 |
| `iAPManager` 静态构造 | `0x0224F424` | 三个购买事件委托初始化，当前源码已有等价字段初始化 |
| `Screen_BoosterReward` 静态构造 | `0x02251F38` | `OnBoosterOpen` 委托初始化，当前源码已有等价字段初始化 |
| `Screen_Transition` 静态构造 | `0x02256080` | `TransitionMode = HideAndShow`，当前源码已有等价字段初始化 |

本批次 IDA 均确认过；纯基类调用的构造函数继续只记录，不添加无依据代码。

## 本批次追加（2026-08-10）

| 类 | IDA 地址 | 结果 |
|---|---:|---|
| `AchievedLevelEventData` | `0x0219D530` | `m_DataList`、固定事件等级列表改为字段初始化器 |
| `DamagePopUpDatas` | `0x0219477C` | `SpawnOffset = 0.65f` 改为字段初始化器 |
| `EffectsDataEditor.HeadShotParameters` | `0x0219DC84` | `SpawnOffset = 1.2f`、`RotationMinMax = (5f,15f)` 改为字段初始化器 |
| `CFX_SpawnSystem` | `0x0225D360` | 两个空数组、`spawnAsChildren`、两个字典改为字段初始化器 |
| `CFX_Demo_Translate` | `0x0225BE24` | `speed = 30f`、`rotation/axis = Vector3.forward` 改为字段初始化器 |
| `CFX2_Demo` | `0x0225A6CC` | `orderedSpawns`、`step`、`range`、`order`、`randomSpawnsDelay` 改为字段初始化器 |
| `CFX_Demo_New` | `0x0225B7D0` | `onScreenParticles` 列表改为字段初始化器 |
| `CFX_Demo_RotateCamera` | `0x0225BCB4` | `speed = 30f` 改为字段初始化器 |
| `SimpleMeshCombine` | `0x0218E700` | `meshName`、`lightmapScale`、`keepStructure`、`setStatic` 改为字段初始化器 |
| `MediationTester` | `0x02177E88` | `MRecAdUnitId` 改为字段初始化器 |
| `GameObjectHashSet` | `0x0217A81C` | `HashSet` 改为字段初始化器 |

以上修改均未增加或删除字段；构造函数中的写入仅改变为同一字段的 C# 初始化器。

## 本批次追加 2（2026-08-10）

| 类 | IDA 地址 | 结果 |
|---|---:|---|
| `GameObjectList` | `0x0217A764` | `List = new List<GameObject>()` 改为字段初始化器 |
| `SyncFilePaths` | `0x02178E1C` | `Paths = new List<FilePath>()` 改为字段初始化器 |
| `Test2` | `0x0224E1AC` | `Data = new List<SkinMoveData>()` 改为字段初始化器 |
| `TrajectoryDataEditor.TutorialData` | `0x0219FD80` | `CorrectThreshold = 0.8f` 改为字段初始化器 |
| `TrajectoryDataEditor.TutorialData.Data` | `0x0219FD94` | `Name = "Data"` 改为字段初始化器 |
| `TrajectoryDataEditor.TrajectoryParameter` | `0x0219FD3C` | IDA 仅调用 `System.Object`，无字段写入 |

## 本批次追加 3（2026-08-10）

| 类 | IDA 地址 | 结果 |
|---|---:|---|
| `FilePath` 默认构造 | `0x02178EE8` | `IsEnabled = true` 改为字段初始化器；带参构造保留参数写入 |
| `SpawnerPool` | `0x02187274` | `PoolName = string.Empty`、`AvailableObjects`、`SpawnedObjects` 改为字段初始化器；参数相关写入保留 |
| `ExecutionOrderAttribute` | `0x02178B20` | 仅参数字段写入 `ExecutionOrder`，不适合字段初始化器，保留 |
| `Percentage(int/Decimal)` | `0x0217E660`、`0x0217E6D0` | 仅构造参数/值类型 backing field 写入，无可独立字段初始化，保留 |
| `ObjectDumper(int)` | `0x02184168` | `sb`、`writer`、`depth` 均依赖构造参数/运行时对象，保留 |
| `ObjectDumper(int,bool)` | `0x02185094` | IDA 地址对应 `WriteObject`，未据此修改字段 |

## 本批次（字段初始化器转换）

| 文件 | IDA 地址 | 处理 |
|---|---:|---|
| `KobGamesSDKSlim/Constants.cs` | `0x021F0A20` | 将 9 个静态字段写入从静态构造函数移为字段初始化器；值与 IDA（`Application.dataPath` 拼接及两个常量字符串）一致 |
| `KobGamesSDKSlim/DontDestroyOnLoad.cs` | `0x021F180C` | 将 `PersistentGameObjects = new HashSet<string>()` 从静态构造函数移为静态字段初始化器 |

## 本批次已核对并转换（IDA 已确认字段写入）

| 文件 | IDA 地址 | 字段写入 |
|---|---:|---|
| `KobGamesSDKSlim/AdsManagerBase.cs` | `0x02203ADC` | 已转换：3 个事件对象、3 个 `Action`、1 个 `Action<bool>` 和 `m_ForceExitPlayMode` |
| `KobGamesSDKSlim/iAPManagerBase.cs` | `0x02212D34` | 已转换：4 个 IAP 回调委托和 `s_TryAdddIAPListenerCount` |
| `KobGamesSDKSlim/RateUsPopUp.cs` | `0x02214A54` | 已转换：`OnRateUsEvent` 回调委托 |
| `KobGamesSDKSlim/RemoteSettingsManager.cs` | `0x022184D8` | 已转换：`OnFirebaseRemotConfigUpdated` 回调委托 |
| `KobGamesSDKSlim/StorageManager.cs` | `0x0222A960` | 已转换：`OnCoinsAmountChanged` 回调委托 |
| `KobGamesSDKSlim/AdsMediation.cs` | `0x021FB23C` | 已转换：`m_OnUseDummyAdsOnDeviceFromTestSuiteChange` 回调委托 |

## 本批次已转换为字段初始化器

| 文件 | IDA 地址 | 字段初始化 |
|---|---:|---|
| `KobGamesSDKSlim/Fps.cs` | `0x021F3A44` | `label = string.Empty`、`textColor = Color.white`、`style = new GUIStyle()` |
| `KobGamesSDKSlim/GameEditorConstants.cs` | `0x021FB834` | 三个 Fastlane 字符串常量 |
| `KobGamesSDKSlim/SyncData.cs` | `0x021FCA20` | `SDKPath`、`DestPath` |
| `KobGamesSDKSlim/SDKVersions.cs` | `0x021FCBF0` | `PluginsVersion`、`SDKVersionsInfo = new List<SDKVersionInfo>()` |
| `KobGamesSDKSlim/GameSettings.cs` | `0x021FB8FC` | 9 个编辑器数据对象字段 `new` 初始化 |
| `KobGamesSDKSlim/LevelData.cs` | `0x02214E3C` | `m_LevelName`、`m_LevelDifficulty`、`m_LevelType`、`m_GameMode`、`m_LevelLoop` |
