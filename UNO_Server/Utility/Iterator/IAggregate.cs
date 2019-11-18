namespace UNO_Server.Utility.Iterator
{
	public interface IAggregate<T>
	{
        IIterator<T> GetIterator();
        T this[int itemIndex] { set; get; }
        int Count { get; }
    }
}
