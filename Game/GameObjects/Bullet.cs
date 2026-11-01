using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class Bullet : GameObject
{
    public Vector2 dir;
    public float speed = 50f;
    GameObject shooter;
    public int dmgAmt = 1;
    public float lifetime = 4f;
    float timer = 0f;
    HitboxComponent hitbox;
    Rectangle roomBounds;
    public Bullet(string spriteName, Scene owner, string name = null) : this(Services.Sprites.CreateSprite(spriteName), owner, name)
    {
  
    }

    public Bullet(Sprite sprite, Scene owner, string name = null) : base(sprite, owner, name)
    {
        layer = Layer.EnemyAtk;
        hitbox = new HitboxComponent(this);
        hitbox.CollisionEnter += OnCollisionEnter;
        hitbox.SetTrigger(true);
        AddComponent(hitbox);

    }

    public void Launch(Vector2 direction, GameObject shooter, Rectangle bounds) {
        dir = direction;
        speed = Random.Shared.Next(40, 61);
        this.shooter = shooter;
        roomBounds = bounds;
        transform.position = shooter.transform.position;
        timer = 0f;
        SetActive(true);
    }
    public override void Update(GameTime gameTime)
    {
        if (!activeSelf) { return; }

        CollisionManager.Instance.Move(dir * speed * (float)gameTime.ElapsedGameTime.TotalSeconds, this.GetComponent<ICollidable>());
        if (!roomBounds.Contains(hitbox.BoxCollider)) {
            SetActive(false);
            return;
        }
        if (timer <= lifetime) {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if(timer >= lifetime)
        {
            SetActive(false);
        }
        base.Update(gameTime);
        
        
    }
    void OnCollisionEnter(ICollidable col) {

        if (!activeSelf || col.owner == shooter) { return; }
        if (col.layer == Layer.Player)
        {
            col.owner.GetComponent<IDamagable>().takeDamage(dmgAmt);
        }
        SetActive(false);
    }
    public override void OnDestroy()
    {
        hitbox.CollisionEnter -= OnCollisionEnter;
        base.OnDestroy();
    }
}
