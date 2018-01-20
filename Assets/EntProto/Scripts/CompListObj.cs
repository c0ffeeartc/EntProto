//-------------------------------------------------
// Copyright (C) 0000-2017, Yegor c0ffee
// Email: c0ffeeartc@gmail.com
//-------------------------------------------------

using System.Collections.Generic;
using Entitas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EntProto
{
	[CreateAssetMenu( menuName = "Custom/Comp List", fileName = "CompList" )]
	public class CompListObj : SerializedScriptableObject
	{
		[SerializeField]
		public              List<IComponent>        Components;
	}
}
