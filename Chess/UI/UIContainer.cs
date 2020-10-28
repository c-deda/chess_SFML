using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;

namespace Chess.UI
{
    class UIContainer : UIElement
    {
        public Vector2f Position { get; private set; }
        public RectangleShape Shape {get; private set; }
        public List<UIElement> Elements { get; private set; }
        
        public UIContainer()
        {
            this.Elements = new List<UIElement>();
            Shape = new RectangleShape();
        }
        public UIContainer(Vector2f position)
        {
            this.Elements = new List<UIElement>();
            this.Position = position;
            Shape = new RectangleShape();
        }
        public UIContainer(Vector2f position, Vector2f size, Color shapeColor)
        {
            this.Elements = new List<UIElement>();
            this.Position = position;
            
            this.Shape = new RectangleShape(size);
            this.Shape.Position = position;
            this.Shape.FillColor = shapeColor;
        }
        public void SetBorder(uint outlineThickness, Color outlineColor)
        {
            Shape.Size = new Vector2f(Shape.Size.X - (outlineThickness * 2), Shape.Size.Y - (outlineThickness * 2));
            Shape.Position = new Vector2f(Shape.Position.X + outlineThickness, Shape.Position.Y + outlineThickness);
            Shape.OutlineThickness = outlineThickness;
            Shape.OutlineColor = outlineColor;
        }
        public void AddElement(UIElement newElement)
        {
            this.Elements.Add(newElement);
        }
        public void RemoveElement(UIElement elementToRemove)
        {
            this.Elements.Remove(elementToRemove);
        }
        public override void OnHover(float x, float y)
        {
            foreach (UIElement element in Elements)
            {
                if (element.Contains(x, y))
                {
                    element.OnHover(x, y);
                }
                else
                {
                    element.OnIdle(x, y);
                }
            }
        }
        public override void OnIdle(float x, float y)
        {
            foreach (UIElement element in Elements)
            {
                if (!element.Contains(x, y))
                {
                    element.OnIdle(x, y);
                }
            }
        }
        public override void OnClick(float x, float y)
        {
            foreach (UIElement element in Elements)
            {
                if (element.Contains(x, y))
                {
                    element.OnClick(x, y);
                }
            }
        }
        public override bool Contains(float x, float y)
        {
            foreach (UIElement element in Elements)
            {
                if (element.Contains(x, y))
                {
                    return true;
                }
            }

            return false;
        }
        public override void Draw()
        {
            Application.Instance().MainWindow.Draw(Shape);

            foreach (UIElement element in Elements)
            {
                element.Draw();
            }
        }
    }
}