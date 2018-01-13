using RPGLibrary;

namespace Davfalcon
{
	public interface IUsableItem : IItem, IEffectSource
	{
		bool IsConsumable { get; }
		int Remaining { get; set; }
		UsableDuringState UsableDuring { get; }
		void Use();
	}
}
