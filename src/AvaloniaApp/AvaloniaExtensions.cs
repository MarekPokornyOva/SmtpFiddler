#region using
using Avalonia.Controls;
using Avalonia.VisualTree;
using System;
using System.Reflection;
#endregion using

namespace AvaloniaApp
{
	public static class AvaloniaExtensions
	{
		static Type _componentType = typeof(IVisual);
		public static void InitControlFields(this Window container)
		{
			INameScope ns = container.FindNameScope();
			if (ns == null)
				return;
			foreach (FieldInfo fi in container.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
				if ((_componentType.IsAssignableFrom(fi.FieldType) && (fi.GetValue(container) == null)))
				{
					object o = ns.Find(fi.Name);
					if (o != null)
						fi.SetValue(container, o);
				}
		}
	}
}
