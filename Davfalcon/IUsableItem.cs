using RPGLibrary;

namespace Davfalcon
{
	public interface IUsableItem : IItem, IEffectSource, IAutoCatalogable
	{
		bool IsConsumable { get; }
		int Remaining { get; set; }
		UsableDuringState UsableDuring { get; }
		void Use();
	}
}
