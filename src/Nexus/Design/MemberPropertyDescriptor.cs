using System;
using System.ComponentModel;
using System.Reflection;

namespace Nexus.Design
{
	/// <summary>
	/// Borrowed from XNA
	/// </summary>
	internal abstract class MemberPropertyDescriptor : PropertyDescriptor
	{
		// Fields
		private MemberInfo _member;

		// Methods
		public MemberPropertyDescriptor(MemberInfo member)
			: base(member.Name, (Attribute[]) member.GetCustomAttributes(typeof(Attribute), true))
		{
			this._member = member;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			MemberPropertyDescriptor descriptor = obj as MemberPropertyDescriptor;
			return ((descriptor != null) && descriptor._member.Equals(this._member));
		}

		public override int GetHashCode()
		{
			return this._member.GetHashCode();
		}

		public override void ResetValue(object component)
		{
		}

		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}

		// Properties
		public override Type ComponentType
		{
			get
			{
				return this._member.DeclaringType;
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}
	}
}