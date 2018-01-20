//-------------------------------------------------
// Copyright (C) 0000-2017, Yegor c0ffee
// Email: c0ffeeartc@gmail.com
//-------------------------------------------------

using Entitas;
using DesperateDevs.Utils;

namespace Custom.Scripts
{
	public static class ContextProtoExtensions
	{
        public static TEntity CloneEntityExt<TEntity>(this IContext<TEntity> context, IEntity entity, bool replaceExisting = false, params int[] indices) where TEntity : class, IEntity
        {
			TEntity entity1 = context.CreateEntity();
			entity.CopyToExt((IEntity) entity1, replaceExisting, indices);
			return entity1;
		}
		public static void CopyToExt(this IEntity entity, IEntity target, bool replaceExisting = false, params int[] indices)
		{
			foreach (int index in indices.Length == 0 ? entity.GetComponentIndices() : indices)
			{
                IComponent component1 = entity.GetComponent(index);
                IComponent component2 = target.CreateComponent(index, component1.GetType());
				component1.CopyPublicMemberValues((object) component2);
				(component2 as IAfterCopy)?.AfterCopy(  );
				if (replaceExisting)
			  		target.ReplaceComponent(index, component2);
				else
			  		target.AddComponent(index, component2);
		  	}
        }
	}
}