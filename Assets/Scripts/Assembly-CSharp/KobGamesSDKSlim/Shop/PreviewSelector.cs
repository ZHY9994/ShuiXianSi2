using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	public class PreviewSelector : SkinSelector
	{
		public bool IsRotate;

		[SerializeField]
		private Rotator m_Rotator;

		[SerializeField]
		private bool IsDanceOnSelect;

		[SerializeField]
		private Animator m_Animator;

		[SerializeField]
		private List<int> m_DanceClips;

		protected override void OnEnable()
		{
			base.OnEnable();
			DisableAllItems();
			if (m_Rotator != null) m_Rotator.enabled = IsRotate;
			if (IsDanceOnSelect) dance();
		}

		protected override void onSelectItem(TItemData i_ItemData)
		{
			base.onSelectItem(i_ItemData);
			if (IsDanceOnSelect) dance();
		}

		private void dance()
		{
			m_Animator.SetTrigger("Dance");
			m_Animator.SetInteger("DanceType", m_DanceClips.GetRandomItem());
		}

		private void OnValidate()
		{
			SetRefs();
		}

		public override void SetRefs()
		{
			base.SetRefs();
			m_Rotator = GetComponentInChildren<Rotator>(true);
		}
	}
}
