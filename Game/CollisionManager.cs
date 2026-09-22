using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;


public class CollisionManager
{
    public static CollisionManager Instance { get; private set; }
    public List<ICollidable> CollidableObjects { get; private set; }
    //just have others add on init?
    float ascaleX;
    float ascaleY;
    float bscaleY;
    float bscaleX;
    public CollisionManager(ContentManager content)
    {
        Instance = this;
        CollidableObjects = new List<ICollidable>();
        //needs a list of everything with Icolliable & list needs to update on spawn of new objects? 
    }
    public void Update(GameTime gameTime)
    {
        //preform basic collision check, the pixel perfect if needed I think
        //i hate this i think i overcomplicated this hella
        //i miss unity :'(
        for (int i = 0; i < CollidableObjects.Count; i++)
        {
            if (!CollidableObjects[i].IsActive) { continue; }
            for (int j = i + 1; j < CollidableObjects.Count; j++)
            {
                if (!CollidableObjects[j].IsActive) { continue; }
                if (CollidableObjects[i].BoxCollider.Intersects(CollidableObjects[j].BoxCollider))
                {
                    if (PixelPerfectCollision(CollidableObjects[i], CollidableObjects[j]))
                    {
                        CollidableObjects[i].OnCollision(CollidableObjects[j]);
                        CollidableObjects[j].OnCollision(CollidableObjects[i]);
                    }
                    //replace w piixel perfect if there time

                    if (!CollidableObjects[i].IsActive) { break; }
                    //prevent bullet taking out more than 1
                }
            }
        }
    }
    public void AddCollidable(ICollidable col)
    {
        CollidableObjects.Add(col);
    }
    public void RemoveCollider(ICollidable col)
    {
        CollidableObjects.Remove(col);
    }
    //i think i could simplify by just pruning everything each call, but im worried about needlessly iterating? 
    //maybe implement a stack? specifically for bullets? idk TT
    public List<ICollidable> CircleCast(ICollidable gameobject, int radius)
    {
        List<ICollidable> overlap = new List<ICollidable>();
        Rectangle bounds = gameobject.BoxCollider;
        Vector2 center = new Vector2(
            bounds.X + bounds.Width / 2f,
            bounds.Y + bounds.Height / 2f
        );
        foreach (ICollidable c in CollidableObjects)
        {
            if (c == gameobject || !c.IsActive) { continue; }

            //gameobject center, or else everything else is at the top le

            int closeX = (int)Math.Clamp(center.X, c.BoxCollider.Left, c.BoxCollider.Right);
            int closeY = (int)Math.Clamp(center.Y, c.BoxCollider.Top, c.BoxCollider.Bottom);
            //uses the corner thats closest to the center
            float dist = (float)Math.Sqrt(Math.Pow(center.X - closeX, 2) + Math.Pow(center.Y - closeY, 2));
            if (dist <= radius)
            {
                overlap.Add(c);
            }
        }
        return overlap;
    }

    //these methodsd are used mainly for scene loading and unloading
    public void AddCollidableList(List<ICollidable> cols)
    {
        foreach (ICollidable c in cols)
        {
            CollidableObjects.Add(c);
        }
    }
    public void RemoveColliderList(List<ICollidable> cols)
    {
        foreach (ICollidable c in cols)
        {
            CollidableObjects.Remove(c);
        }

    }

    public bool PixelPerfectCollision(ICollidable a, ICollidable b)
    {
        // i could also combine w basic check, if in border then also run a pixel perfect check? 
        //should take two gameobject or sprites and check the overlap
        //only take into account the pixels that are overlapping and have a positive alpha value?
        //return true if collision, false if not
        Rectangle rectoverlap = Rectangle.Intersect(a.BoxCollider, b.BoxCollider);
        if (rectoverlap.Width == 0 || rectoverlap.Height == 0)
        {
            return false;
        }
        ascaleX = a.BoxCollider.Width / (float)a.sprite.Region.Width;
        ascaleY = a.BoxCollider.Height / (float)a.sprite.Region.Height;
        bscaleX = b.BoxCollider.Width / (float)b.sprite.Region.Width;
        bscaleY = b.BoxCollider.Height / (float)b.sprite.Region.Height;
        //must use region.width/region.height and not .height or .width becuz region uses unscaled, and .width/height scales up.


        for (int i = rectoverlap.X; i < rectoverlap.Right; i++)
        {
            for (int j = rectoverlap.Y; j < rectoverlap.Bottom; j++)
            {
                int xposA = i - a.BoxCollider.Left;
                int xposB = i - b.BoxCollider.Left;
                int yposA = j - a.BoxCollider.Top;
                int yposB = j - b.BoxCollider.Top;
                //pixel pos on enlarged texture

                int maskAX = (int)MathF.Floor(xposA / ascaleX);
                int maskAY = (int)MathF.Floor(yposA / ascaleY);
                int maskBX = (int)MathF.Floor(xposB / bscaleX);
                int maskBY = (int)MathF.Floor(yposB / bscaleY);
                //convert to normal size position to check for alpha

                bool Aalpha = a.sprite.Region.alphaMask[a.sprite.Region.Width * maskAY + maskAX];
                bool Balpha = b.sprite.Region.alphaMask[b.sprite.Region.Width * maskBY + maskBX];
                if (Aalpha && Balpha)
                {
                    return true;
                }
            }
        }

        return false;
    }

}
