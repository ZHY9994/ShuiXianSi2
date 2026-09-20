using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class NestedScrollRect : ScrollRect
{
	private bool routeToParent;

	private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
	{
		Transform parent = transform.parent;
		while (parent != null)
		{
			Component[] components = parent.GetComponents<Component>();
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i] is T handler)
				{
					action(handler);
				}
			}
			parent = parent.parent;
		}
	}

	public override void OnInitializePotentialDrag(PointerEventData eventData)
	{
		DoForParents<IInitializePotentialDragHandler>(handler => handler.OnInitializePotentialDrag(eventData));
		base.OnInitializePotentialDrag(eventData);
	}

	public override void OnDrag(PointerEventData eventData)
	{
		if (routeToParent)
		{
			DoForParents<IDragHandler>(handler => handler.OnDrag(eventData));
		}
		else
		{
			base.OnDrag(eventData);
		}
	}

	public override void OnBeginDrag(PointerEventData eventData)
	{
		if (!horizontal && Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
		{
			routeToParent = true;
			DoForParents<IBeginDragHandler>(handler => handler.OnBeginDrag(eventData));
		}
		else if (!vertical && Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y))
		{
			routeToParent = true;
			DoForParents<IBeginDragHandler>(handler => handler.OnBeginDrag(eventData));
		}
		else
		{
			routeToParent = false;
			base.OnBeginDrag(eventData);
		}
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		if (routeToParent)
		{
			DoForParents<IEndDragHandler>(handler => handler.OnEndDrag(eventData));
		}
		else
		{
			base.OnEndDrag(eventData);
		}
		routeToParent = false;
	}
}
