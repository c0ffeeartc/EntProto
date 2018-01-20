//-------------------------------------------------
// Copyright (C) 0000-2017, Yegor c0ffee
// Email: c0ffeeartc@gmail.com
//-------------------------------------------------

using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Custom.Scripts
{
	public abstract partial class EntityProto<TEntity, TContext>
		where TEntity:Entity
		where TContext:Context<TEntity>, new(  )
	{
		[SerializeField]
		private				List<String>			_removeCompNames		= new List<String>(  );
		[SerializeField]
		private				List<IComponent>		_selfComps;
		[SerializeField]
		private				List<CompListObj>		_sharedCompObjs;
		private				TEntity					_proto;
		private				List<Int32>				_removeCompIndexes;
		private static		TContext				_protoContext			= new TContext(  );
		public				TEntity					Proto
		{
			get
			{
				return _proto ?? ( _proto = CreateEntityProto(  ) );
			}
			set
			{
				_proto = value;
			}
		}
		private				List<Int32>				RemoveCompIndexes
		{
			get
			{
				if ( _removeCompIndexes != null )
				{
					return _removeCompIndexes;
				}

				_removeCompIndexes = new List<Int32>(  );
				for ( var i = 0; i < _removeCompNames.Count; i++ )
				{
					_removeCompIndexes.Add( Array.FindIndex( _protoContext.contextInfo.componentNames, v => v == _removeCompNames[i] ) );
				}
				return _removeCompIndexes;
			}
		}

		public				TEntity					Clone					( TContext context, params int[] indices )
		{
			return context.CloneEntityExt( Proto, false, indices );
		}
		public				void					ApplyTo					( TEntity entity, Boolean replaceExisting = true, params int[] indices )
		{
			var count = RemoveCompIndexes.Count;
			for ( var i = 0; i < count; i++ )
			{
				var index = RemoveCompIndexes[i];
				if ( entity.HasComponent( index ) )
				{
					entity.RemoveComponent( index );
				}
			}
			Proto.CopyToExt( entity, replaceExisting, indices );
		}
		private				List<IComponent>		ExpandComps				(  )
		{
			var comps = new List<IComponent>(  );
			for ( var i = 0; i < _selfComps.Count; i++ )
			{
				var comp		= _selfComps[i];

				var compIndex	= Array.FindIndex( _protoContext.contextInfo.componentTypes, v => v == comp.GetType(  ) );
				if ( RemoveCompIndexes.Contains( compIndex ) )
				{
					continue;
				}

				comps.Add( comp );
			}

			for ( var j = 0; j < _sharedCompObjs.Count; j++ )
			{
				var sharedCompObj = _sharedCompObjs[j];
				for ( var i = 0; i < sharedCompObj.Components.Count; i++ )
				{
					var comp		= sharedCompObj.Components[i];

					var index		= comps.FindIndex( v=> v.GetType(  ) == comp.GetType(  ) );
					if ( index >= 0 )
					{
						continue;
					}

					var compIndex	= Array.FindIndex( _protoContext.contextInfo.componentTypes, v => v == comp.GetType(  ) );
					if ( RemoveCompIndexes.Contains( compIndex ) )
					{
						continue;
					}

					comps.Add( comp );
				}
			}

			return comps;
		}
		private				TEntity					CreateEntityProto		(  )
		{
			var entity			= _protoContext.CreateEntity(  );
			_removeCompIndexes	= null;
			var components		= ExpandComps(  );
			for ( var i = 0; i < components.Count; i++ )
			{
				var comp	= components[i];
				// TODO: cache or use static dictionary
				var compI	= Array.FindIndex( _protoContext.contextInfo.componentTypes, t => t == comp.GetType(  ) );
				entity.AddComponent( compI, comp );
			}
			return entity;
		}
	}
}
