using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntProto.Examples
{
	public class GameEntProto : EntityProto<GameEntity, GameContext>
	{
	}
	public class GameProtoHolder : BaseProtoHolder<GameEntProto, GameEntity, GameContext>
	{
		[SerializeField]
		private 	Dictionary<String, GameEntProto> _prototypes			= new Dictionary<string, GameEntProto>(  );

		protected override 	void 					Awake					(  )
		{
			base.Awake( );
			foreach ( var kv in _prototypes )
			{
				Prototypes.Add( kv.Key, kv.Value );
			}
		}
	}
}