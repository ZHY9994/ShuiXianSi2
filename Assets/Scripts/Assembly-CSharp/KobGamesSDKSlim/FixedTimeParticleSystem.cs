using UnityEngine;

namespace KobGamesSDKSlim
{
	public class FixedTimeParticleSystem : MonoBehaviour
	{
		public ParticleSystem ps;

		private bool playing;

		public void Play()
		{
			ps.Simulate(0f, true, true);
			playing = true;
		}

		public void Stop()
		{
			playing = false;
		}

		public void FixedUpdate()
		{
			if (playing)
				ps.Simulate(Time.fixedDeltaTime, true, false);
		}
	}
}
