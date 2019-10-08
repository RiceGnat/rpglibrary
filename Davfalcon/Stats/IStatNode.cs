using Davfalcon.Nodes;

namespace Davfalcon.Stats
{
    public interface IStatNode : INode<int>
    {
        string Name { get; }
        int Base { get; }
        INode<int> Additions { get; }
        INode<int> Multipliers { get; }
    }
}
