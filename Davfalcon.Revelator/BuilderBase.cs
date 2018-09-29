namespace Davfalcon.Revelator
{
	public abstract class BuilderBase<T, TOut> where T : TOut
	{
		protected T build;

		public virtual TOut Build()
			=> build;
	}

	public abstract class BuilderBase<T> : BuilderBase<T, T>
	{

	}
}
