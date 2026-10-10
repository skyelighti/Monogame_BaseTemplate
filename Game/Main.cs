using BaseTemplate.Game.BaseClasses;
using BaseTemplate.Game.BaseClasses.Assets;
using BaseTemplate.Game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
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
        private SceneManager sceneManager;
        public Main(ContentManager content)
        {
            contentManager = content;
            var sprites = new SpriteLibrary(content);
            var gameInfo = new GameInfoManager(content);
            var inputManager = new InputManager();
            //UIManager = new UIManager(content);
            Services.Initalize(inputManager, sprites, gameInfo);
            //simplify sprite loading? 
            gameInfo.InitItemList();

            sceneManager = new SceneManager(content);
        }
        //load textures and atlas here for right now? maybe a dedicated asset loader?
        //maybe implement a statemaachine for game state? :P extra work to do if i hate reading!!
        public void Update(GameTime gameTime, Rectangle screenBounds)
        {
            // update everything else here as well?
            ScreenBounds = screenBounds;
            sceneManager.Update(gameTime);
            CollisionManager.Instance.Update(gameTime);
            //UIManager.Update(gameTime);
            Services.Input.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sceneManager.Draw(spriteBatch);
            //UIManager.Draw(spriteBatch);
        }
    }
}
