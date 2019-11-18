namespace UNO_Server.Utility.Iterator
{
	public interface IIterator<T>
    {
        T First { get; }
        T Next { get; }
        T Current { get; }
        bool HasNext { get; }
    }
}
