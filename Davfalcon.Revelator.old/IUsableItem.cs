
namespace Davfalcon.Revelator
{
	public interface IUsableItem : IItem, IEffectSource
	{
		bool IsConsumable { get; }
		int Remaining { get; set; }
		UsableDuringState UsableDuring { get; }
		void Use();
	}
}
