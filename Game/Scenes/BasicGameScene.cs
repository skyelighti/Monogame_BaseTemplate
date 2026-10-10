using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameBase;

public class BasicGameScene : Scene
{
    //private readonly TiledMap ground;
    //List<ICollidable> tileColliders = new();
    //public IReadOnlyList<SpawnInfo> Spawns { get; }
    //

    //private readonly Player player;
    //public IReadOnlyList<SpawnInfo> Spawns { get; }
    // references to a tilemap & player if we using either
    public BasicGameScene(ContentManager content) : base(content)
    {
        //load level here. 
        //LevelData level = Tilemap.LoadLevel(content, "Level1TileMap.xml");
        //ground = level.Map;
        //Spawns = level.spawns;
        //ground.Scale = new Vector2(3, 3);

        //foreach spawn in spawns: get location, and spawn type here
        //resettiilecolliders
    }
    public override void EnterScene()
    {
        base.EnterScene();
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public override void ExitScene()
    {
        base.ExitScene();
    }

    public void ResetLevel() { 
    //if level requires a hard reset
    }
}
