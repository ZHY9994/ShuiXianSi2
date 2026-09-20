using System;
using System.Collections.Generic;

namespace MrWayne
{
	[Serializable]
	public class BeatingWeaponEditor : WeaponCategoryEditor
	{
		public MobilePhoneData MobilePhone;

		public RockData Rock;

		public BowlingBallData BowlingBall;

		public BallWithSpikesData BallWithSpikes;

		public HammerData Hammer;

		public MaseData Mase;

		public FootBallData FootBall;

		public PipeData Pipe;

		public BottleData Bottle;

		public override List<WeaponData> All => new List<WeaponData> { MobilePhone, Rock, BowlingBall, BallWithSpikes, Hammer, Mase, FootBall, Pipe, Bottle };
	}
}
