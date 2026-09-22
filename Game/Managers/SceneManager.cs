using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Game.Managers
{
    public enum Scenes
    {
        StartScene,
        GameScene,
        EndScene
    }
    public class SceneManager
    {
        public static SceneManager Instance { get; private set; }
        public StateMachine SceneM = new StateMachine();
        Dictionary<Scenes, Scene> SceneDict = new Dictionary<Scenes, Scene>();

        //inits scenes, in scene manager and sets the functions for enter update and exiting. 

        public SceneManager(ContentManager content)
        {
            Instance = this;
            SceneM.SetNumOfStates(3);
            //SceneDict.Add(Scenes.StartScene, new StartScene(content));
            //SceneDict.Add(Scenes.GameScene, new GameScene(content));
            //SceneDict.Add(Scenes.EndScene, new EndScene(content));
            //SceneM.SetStateFunctions(0, EnterStart, UpdateStart, ExitStart);
            //SceneM.SetStateFunctions(1, EnterGame, UpdateGame, ExitGame);
            //SceneM.SetStateFunctions(2, EnterEnd, UpdateEnd, ExitEnd);
            //init stateMachine
            SceneM.SetState((int)Scenes.GameScene);
        }
        void EnterStart()
        {
            SceneDict[Scenes.StartScene].EnterScene();
        }
        void UpdateStart(GameTime gameTime)
        {
            SceneDict[Scenes.StartScene].Update(gameTime);
        }
        void ExitStart()
        {
            SceneDict[Scenes.StartScene].ExitScene();
        }
        void EnterGame()
        {
            SceneDict[Scenes.GameScene].EnterScene();
        }
        void UpdateGame(GameTime gameTime)
        {
            SceneDict[Scenes.GameScene].Update(gameTime);
        }
        void ExitGame()
        {
            SceneDict[Scenes.GameScene].ExitScene();
        }
        void EnterEnd()
        {
            SceneDict[Scenes.EndScene].EnterScene();
        }
        void UpdateEnd(GameTime gameTime)
        {
            SceneDict[Scenes.EndScene].Update(gameTime);
        }
        void ExitEnd()
        {
            SceneDict[Scenes.EndScene].ExitScene();
        }

        public void Update(GameTime gameTime)
        {
            SceneM.UpdateState(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (SceneM.CurrentState == -1) { return; }
            SceneDict[(Scenes)SceneM.CurrentState].Draw(spriteBatch);
            //update shouldnt know draw spritebatchinfo
        }
    }
}
