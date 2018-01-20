using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace EntProto.Examples.Sources.Systems
{
	public class HelloSystem : ReactiveSystem<GameEntity>
	{
		public				HelloSystem				( Contexts contexts ) : base( contexts.game )
		{
		}

		protected override 	ICollector<GameEntity> 	GetTrigger				( IContext<GameEntity> context )
		{
			return context.CreateCollector( GameMatcher.HelloComp );
		}
		protected override 	Boolean 				Filter					( GameEntity entity )
		{
			return entity.hasHelloComp;
		}
		protected override 	void					Execute					( List<GameEntity> entities )
		{
			for ( var i = 0; i < entities.Count; i++ )
			{
				var ent = entities[i];
				Debug.Log( ent.helloComp.Value );
			}
		}
	}
}