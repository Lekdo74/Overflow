using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Overflow.src
{
    public class Button
    {
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        private Vector2 mousePosition;

        private Texture2D _texture;
        private SpriteFont _font;
        private bool _isHovering;

        public event EventHandler Click;

        private Color _fontColor;
        private Vector2 _position;
        private string _text;


        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            FontColor = Color.Black;
        }

        public Button(Texture2D texture)
        {
            _texture = texture;
        }

        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }

        public Color FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                _fontColor = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            Color color = Color.White;

            if (_isHovering)
                color = Color.Gray;

            spritebatch.Draw(_texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                float x = Rectangle.X + Rectangle.Width / 2 - _font.MeasureString(Text).X / 2;
                float y = Rectangle.Y + Rectangle.Height / 2 - _font.MeasureString(Text).Y / 2;

                spritebatch.DrawString(_font, Text, new Vector2(x, y), FontColor);
            }
        }

        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            mousePosition = PlayerInputs.MousePosition(out _currentMouse);

            Rectangle mouseRectangle = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

            if (mouseRectangle.Intersects(Rectangle))
                _isHovering = true;
            else
                _isHovering = false;

            if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed && Click != null && _isHovering)
                Click(this, new EventArgs());
        }
    }
}
