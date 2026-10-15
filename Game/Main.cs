using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameBase.Input;
using GameBase.Assets;
using GameBase.Dialogue;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;

namespace GameBase;

public class Main
{
    public static Rectangle ScreenBounds;
    public static ContentManager contentManager { get; private set; }
    private SceneManager sceneManager;
    public static MonoGame.Extended.OrthographicCamera camera;

    public Main(ContentManager content)
    {
        contentManager = content;
        var sprites = new SpriteLibrary(content);
        var gameInfo = new GameInfoManager(content);
        var inputManager = new InputManager();
        //UIManager = new UIManager(content);
        Services.Initalize(inputManager, sprites, gameInfo, new DialogueManager(inputManager));
        //simplify sprite loading? 
        gameInfo.InitItemList(content);
        gameInfo.InitDialogueLines(content);
        gameInfo.InitNPCDefinitions(content);
        gameInfo.playerInventory.OnEnable();

        sceneManager = new SceneManager(content);
    }
    //load textures and atlas here for right now? maybe a dedicated asset loader?
    //maybe implement a statemaachine for game state? :P extra work to do if i hate reading!!
    public void Update(GameTime gameTime, Rectangle screenBounds)
    {
        // update everything else here as well?
        ScreenBounds = screenBounds;
        sceneManager.Update(gameTime);
        Services.Input.Update(gameTime);
        Services.Dialogue.Update(gameTime);
        CollisionManager.Instance.Update(gameTime);
        //UIManager.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        sceneManager.Draw(spriteBatch);
        //UIManager.Draw(spriteBatch);
    }
}
