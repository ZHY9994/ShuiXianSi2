using KobGamesSDKSlim.MenuManagerV1;
using System.Linq;
using UnityEngine;
using Wayne.Game.Tutorials;

public class Screen_ShopTutorial : MenuScreenBase
{
	[SerializeField]
	private TTutorialStep[] m_Steps;

	protected override void OnEnable()
	{
		base.OnEnable();
		select(eStep.Step_1);
		Wayne.Game.Tutorials.ShopTut.OnStepChanged += onStepChanged;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Wayne.Game.Tutorials.ShopTut.OnStepChanged -= onStepChanged;
	}

	private void onStepChanged(eStep i_Step)
	{
		select(i_Step);
	}

	private void hideAll()
	{
		foreach (TTutorialStep step in m_Steps)
			step.Hide();
	}

	private void select(eStep i_Step)
	{
		hideAll();
		TTutorialStep step = m_Steps.FirstOrDefault(x => x.Step == i_Step);
		if (step != null)
			step.Show();
	}
}
