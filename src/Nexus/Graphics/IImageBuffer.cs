using Nexus.Graphics.Colors;

namespace Nexus.Graphics
{
	public interface IImageBuffer
	{
		ColorF this[int x, int y]
		{
			get;
			set;
		}
	}
}