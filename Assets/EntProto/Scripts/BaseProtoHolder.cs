//-------------------------------------------------
// Copyright (C) 0000-2017, Yegor c0ffee
// Email: c0ffeeartc@gmail.com
//-------------------------------------------------

using System;
using System.Collections.Generic;
using Entitas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Custom.Scripts
{
	public abstract class BaseProtoHolder <TProto, TEntity, TContext>
		: SerializedBehaviour
			where TProto : EntityProto<TEntity, TContext>
			where TEntity : Entity
			where TContext : Context<TEntity>, new (  )
	{
		protected  			Dictionary<String, TProto>						_runtimeProtos		= new Dictionary<String, TProto>(  );
		private static		BaseProtoHolder <TProto, TEntity, TContext>		_instance;
		public static		BaseProtoHolder <TProto, TEntity, TContext>		Instance
		{
			get
			{
				return _instance;
			}
			set
			{
				_instance = value;
			}
		}
		public				Dictionary<String, TProto>						Prototypes
		{
			get
			{
				return _runtimeProtos;
			}
		}

		public 				TEntity					Clone					( String protoName, TContext context, params int[] indices )
		{
			TProto entityProto;
			if ( !_runtimeProtos.TryGetValue( protoName, out entityProto ) )
			{
				Debug.LogError( "No such prototype " + protoName );
				return null;
			}

			return entityProto.Clone( context, indices );
		}
		public				void					ApplyTo					( String protoName, TEntity entity, Boolean replaceExisting = true, params int[] indices )
		{
			TProto entityProto;
			if ( !_runtimeProtos.TryGetValue( protoName, out entityProto ) )
			{
				Debug.LogError( "No such prototype " + protoName );
				return;
			}

			entityProto.ApplyTo( entity, replaceExisting, indices );
		}
		protected virtual	void					Awake					(  )
		{
			_instance = this;
		}
	}
}
