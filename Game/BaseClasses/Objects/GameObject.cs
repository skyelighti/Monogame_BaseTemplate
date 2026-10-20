using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Assets;
using System.Diagnostics;
namespace GameBase;

public class GameObject
{
    public TransformComponent transformC { get; }
    public Transform transform => transformC.transform;

    public bool IsActive { get; private set; } = true;
    //readonly mainly for pooling
    public string name { get; protected set; }
    // identifier 
    public SpriteComponent spriteC { get; }
    public Sprite sprite => spriteC.sprite;
    public Scene ownerScene { get; protected set; }
    public bool ActiveOnSceneEnter = true;
    public Layer layer { get; protected set; } = Layer.Default; 
    private readonly List<IComponent> components = new();


    //public bool spriteVisible = true;
    //float invisibilityTimer = 0f;
    public GameObject(string spriteName, Scene owner, string name = null)
    {

        var trans = new TransformComponent(Vector2.Zero, 0, new Vector2(64, 64), this);
        transformC = trans;
        components.Add(trans);

        this.name = name;
        var s = new SpriteComponent(spriteName, this);
        spriteC = s;
        components.Add(spriteC);
        ownerScene = owner;

    }


    public virtual void Update(GameTime gameTime)
    {
        //whereever handles updates should check if each gameobject is active before updating them
        //collision handler
        foreach (IComponent component in components)
        {
            if (component.IsActive)
            {
                component.Update(gameTime);
            }
        }
    }
    public void UpdateName(string newName)
    {
        //updates gameobject name
        name = newName;
    }
    public void UpdateSprite(Sprite newSprite)
    {
        spriteC.UpdateSprite(newSprite);
    }
    public void UpdateLocationX(float x)
    {
        //updates gameobject location x direction
        transform.position = new Vector2(x, transform.position.Y);
        //reduces update calls? 
    }
    public void UpdateLocationY(float y)
    {
        //updates gameobject location y direction
        transform.position = new Vector2(transform.position.X, y);
    }
    public void UpdateLocation(float x, float y)
    {
        //updates gameobject location x and y direction
        transform.position = new Vector2(x, y);
    }
    public void UpdateSize(int x, int y)
    {
        transform.scale = new Vector2(x, y);
        //updates gameobject pixel size
        spriteC.UpdateSize(x, y);

    }

    public void Draw(SpriteBatch spriteBatch)
    {

        // first rect draws location and size, second rectangle is spritesheet location and size, color is tinting

        //should the object that inherits from this handle draw? or should the base handle?
        spriteC.Draw(spriteBatch);
    }

    public void SetActive(bool state)
    {
        IsActive = state;
    }
    public void AddComponent(IComponent c)
    {
        if (c.owner == this && !components.Contains(c))
        {
            components.Add(c);
        }
        else {
            Debug.WriteLine("component owner is not correct, or already added.");
        }
    }
    public T GetComponent<T>() where T : class
    {
        foreach (IComponent component in components)
        {
            if (component is T match)
            {
                return match;
            }
        }

        return null;
    }
    public List<T> GetComponents<T>() where T : class
    {
        List<T> c = new();
        foreach (IComponent component in components)
        {
            if (component is T match)
            {
                c.Add(match);
            }
        }

        return c;
    }
}
