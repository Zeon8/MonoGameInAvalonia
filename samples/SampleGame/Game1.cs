using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameInAvalonia;

namespace SampleGame
{
    public class Game1 : EmbeddedGame
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _texture;
        private readonly Point _size = new(64, 64);

        private Vector2 _position = new(100, 100);
        private Vector2 _velocity = new(100, 100);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.HardwareModeSwitch = false;
            _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("Icon");
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position += _velocity * deltaTime;

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            if (_position.X <= 0 || _position.X + _size.X >= screenWidth)
                _velocity.X *= -1;

            if (_position.Y <= 0 || _position.Y +  _size.Y >= screenHeight)
                _velocity.Y *= -1;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred);
            var position = new Point((int)_position.X, (int)_position.Y);
            _spriteBatch.Draw(_texture, new Rectangle(position, _size), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
