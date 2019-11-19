namespace UNO_Client.Decorator
{
	public abstract class RectDecorator : Rect
	{
		protected readonly Rect rectangleToBeDecorated;
		public RectDecorator(Rect rectangleToBeDecorated)
		{
			this.rectangleToBeDecorated = rectangleToBeDecorated;
			this.graphics = rectangleToBeDecorated.graphics;
			this.rectangle = rectangleToBeDecorated.rectangle;
		}

		public override void Draw()
		{
			rectangleToBeDecorated.Draw();
		}
	}
}
