using System.Collections;
using Entitas;
using EntProto.Examples.Sources.Systems;
using UnityEngine;

namespace EntProto.Examples
{
	public class GameController : MonoBehaviour
	{
		private 			Systems					_systems;
		private 			Contexts 				_contexts;
		private				void					Start					(  )
		{
			Contexts.sharedInstance = new Contexts(  );
			_contexts = Contexts.sharedInstance;

			_systems = new Systems(  );
			_systems.Add( 
					new HelloSystem( _contexts )
				);

			_systems.Initialize(  );
			StartCoroutine( ClonePrototypes(  ) );
		}
		private 			void 					Update 					(  )
		{
			_systems.Execute(  );
			_systems.Cleanup(  );
		}
		private 			void 					OnDestroy				(  )
		{
			_systems.TearDown(  );
		}
		private 			IEnumerator 			ClonePrototypes			(  )
		{
			yield return new WaitForSeconds( 1f );
			GameProtoHolder.Instance.Clone( "Hello1", _contexts.game );
			
			yield return new WaitForSeconds( 1f );
			GameProtoHolder.Instance.Clone( "SharedHello1", _contexts.game );
			
			yield return new WaitForSeconds( 1f );
			GameProtoHolder.Instance.Clone( "SharedHello2", _contexts.game );
			
			yield return new WaitForSeconds( 1f );
			GameProtoHolder.Instance.Clone( "SharedHello3", _contexts.game );
		}
	}
}