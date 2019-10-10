namespace Davfalcon.Revelator
{
    public sealed class Unit : UnitTemplate<IUnit>, IUnit
    {
        protected override IUnit Self => this;
    }
}
