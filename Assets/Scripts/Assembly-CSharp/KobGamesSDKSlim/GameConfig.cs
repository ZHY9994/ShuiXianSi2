using KobGamesSDKSlim.Shop;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[CreateAssetMenu(fileName = "GameConfig")]
	public class GameConfig : GameConfigBase
	{
		public DebugVariablesEditor Debug;

		public InputVariablesEditor Input;

		public GamePlayVariablesEditor GamePlay;

		public LevelsVariablesEditor Levels;

		public HUDVariablesEditor HUD;

		public MenuVariablesEditor Menus;

		public PlayerVariablesEditor Player;

		public CameraVariablesEditor Camera;

		public HapticsVariablesEditor Haptics;

		public AddressablesEditor Addressables;

		public ShopParametersEditor Shop;

		public GameConfig()
			: base()
		{
			Debug = new DebugVariablesEditor();
			Input = new InputVariablesEditor();
			GamePlay = new GamePlayVariablesEditor();
			Levels = new LevelsVariablesEditor();
			HUD = new HUDVariablesEditor();
			Menus = new MenuVariablesEditor();
			Player = new PlayerVariablesEditor();
			Camera = new CameraVariablesEditor();
			Haptics = new HapticsVariablesEditor();
			Addressables = new AddressablesEditor();
		}
	}
}
