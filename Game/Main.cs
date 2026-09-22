using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Graphics;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;
namespace BaseTemplate.Game
{
    public class Main
    {
        public static Rectangle ScreenBounds;
        public static ContentManager contentManager { get; private set; }
        private CollisionManager collisionManager;
        //private SceneManager sceneManager;
        //private UIManager UIManager;
        public static TextureAtlas atlas { get; private set; }

        public Main(ContentManager content)
        {
            contentManager = content;
            collisionManager = new CollisionManager(content);
            //sceneManager = new SceneManager(content);
            //UIManager = new UIManager(content);
        }
        //load textures and atlas here for right now? maybe a dedicated asset loader?
        //maybe implement a statemaachine for game state? :P extra work to do if i hate reading!!
        public void Update(GameTime gameTime, Rectangle screenBounds)
        {
            // update everything else here as well?
            ScreenBounds = screenBounds;
            //sceneManager.Update(gameTime);
            collisionManager.Update(gameTime);
            //UIManager.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //sceneManager.Draw(spriteBatch);
            //UIManager.Draw(spriteBatch);
        }
    }
}
