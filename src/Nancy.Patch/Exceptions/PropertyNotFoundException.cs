using System;

namespace Nancy.Patch.Exceptions
{
	internal class PropertyNotFoundException : Exception
	{
		public string PropertyName { get; private set; }

		public PropertyNotFoundException(string propertyName)
			: base("Could not find writable property: " + propertyName)
		{
			PropertyName = propertyName;
		}
	}
}
