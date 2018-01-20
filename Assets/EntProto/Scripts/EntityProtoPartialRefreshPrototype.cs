//-------------------------------------------------
// Copyright (C) 0000-2017, Yegor c0ffee
// Email: c0ffeeartc@gmail.com
//-------------------------------------------------

#if UNITY_EDITOR
using System;
using Entitas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Custom.Scripts
{
	public abstract partial class EntityProto<TEntity, TContext>
		where TEntity:Entity
		where TContext:Context<TEntity>, new(  )
	{
		[HideIf( "IsInEditMode" )]
		private				void					RefreshPrototype		(  )
		{
			_proto = CreateEntityProto(  );
		}
		private				Boolean					IsInEditMode			(  )
		{
			return !Application.isPlaying;
		}
	}
}
#endif