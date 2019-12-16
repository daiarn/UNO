namespace UNO_Client.Interpreter
{
	public abstract class Expression
	{
        public string Output;
		public abstract void Interpret(Context context);
	}
}
