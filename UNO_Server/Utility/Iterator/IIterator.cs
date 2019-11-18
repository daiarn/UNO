namespace UNO_Server.Utility.Iterator
{
	public interface IIterator<T>
    {
        T First();
        T Next();
        T Current();
        bool HasNext();
    }
}
