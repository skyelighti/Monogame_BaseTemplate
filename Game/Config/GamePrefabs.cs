using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public static partial class Prefabs
{
    //factory create objs? 
    public static GameObject CreateBullet(Scene owner, Sprite sprite, Vector2 position) { 
        GameObject bullet = CreateSolid(owner, sprite, position);
        bullet.UpdateLayer(Layer.EnemyAtk);
        //to be updated to use a bullet component instead of a hitbo
        bullet.AddComponent(new Bullet(bullet));

        return bullet;  
    }
}
