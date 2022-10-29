namespace Core.Factory
{
	public interface IFactory<in TParam, out TProduct>
	{
		TProduct Create(TParam parameter);
	}

	public interface IFactory<in TParamFirst, in TParamSecond, out TProduct>
	{
		TProduct Create(TParamFirst firstParameter, TParamSecond secondParameter);
	}
}