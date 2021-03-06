﻿using System.Drawing;

namespace UNO_Client.Decorator
{
	public class BackgroundDecorator : RectDecorator
	{
		public BackgroundDecorator(Rect rectangleToBeDecorated) : base(rectangleToBeDecorated)
		{
			this.rectangle = rectangleToBeDecorated.rectangle;
			this.graphics = rectangleToBeDecorated.graphics;
		}

		public override void Draw()
		{
			base.Draw();
			SolidBrush WhiteBrush = new SolidBrush(Color.White);
			rectangleToBeDecorated.graphics.FillRectangle(WhiteBrush, rectangleToBeDecorated.rectangle);
		}
	}
}
