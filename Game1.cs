using BaseTemplate.Game;
using Gum;
using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using BlendState = Microsoft.Xna.Framework.Graphics.BlendState;

namespace Homework2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        GumService GumUI => GumService.Default;
        private SpriteBatch _spriteBatch;
        private Main main;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GumUI.Initialize(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Main.ScreenBounds = GraphicsDevice.Viewport.Bounds;
            main = new Main(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            GumUI.Update(gameTime);
            // TODO: Add your update logic here
            main.Update(gameTime, GraphicsDevice.Viewport.Bounds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
            // TODO: Add your drawing code here
            main.Draw(_spriteBatch);
            _spriteBatch.End();
            //GumUI.Draw();
            base.Draw(gameTime);
        }
    }
}
