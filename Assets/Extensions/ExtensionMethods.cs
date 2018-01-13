using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ExtensionMethods {

	public static Transform SearchForChild(this Transform target, string name)
	{
		if (target.name == name) return target;

		for (int i = 0; i < target.childCount; ++i)
		{
			var result = SearchForChild(target.GetChild(i), name);

			if (result != null) return result;
		}

		return null;
	}

	public static T GetComponentInAncestry<T>(this Transform transform)
	{
		var grandParent = transform.parent.parent;
		var c = transform.GetComponentInParent<T>();

		if (c != null)
			return c;

		if (c == null & grandParent != null)
			return transform.GetComponentInAncestry<T>();

		return c;
	}
}

class WaitWhile : CustomYieldInstruction
{
	private Func<bool> predicate;

	public override bool keepWaiting
	{
		get
		{
			return predicate();
		}
	}

	public WaitWhile(Func<bool> predicate) { this.predicate = predicate; }
}
