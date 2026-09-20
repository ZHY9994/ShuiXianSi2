# GameManagerV1/GameManagerBase 实时还原记录

## 2026-08-09

- IDA `0x0223A908`: `RaiseOnLevelContinue` 恢复继续事件派发。
- IDA `0x0223A974`: `OnAwakeEvent` 恢复基础唤醒、分辨率/FPS/状态重置、Tween 清理和时间缩放。
- IDA `0x0223AB60`: `OnEnable` 恢复远程配置订阅及快捷键回调注册。
- IDA `0x0223AE3C`: `Start` 恢复关闭关卡数据恢复、尝试次数递增和关卡加载。
- IDA `0x0223B014`: `Update` 恢复分辨率变化检测及 `OnScreenChanged` 派发。
- IDA `0x0223B138`、`0x0223B1D0`、`0x0223AB38`：恢复游戏/关卡重置逻辑。
- IDA `0x0223B268`、`0x0223B2F4`、`0x0223B380`、`0x0223AF94`：恢复 Tween 清理、暂停/恢复及关卡加载逻辑。
- IDA `0x0223B40C`、`0x0223B508`、`0x0223B518`、`0x0223B624`、`0x0223B77C`、`0x0223B88C`、`0x0223B994`：恢复关卡生命周期状态和强类型事件派发。
- 未改动任何字段声明。
- Unity 脚本验证：0 个错误；仅有 `Update` 可能产生临时分配的分析器警告。
