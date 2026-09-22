using Homework2.Game;
using Homework2.Game.GameObjects;
using Homework2.Game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Scene
{
    List<GameObject> sceneObjects = new List<GameObject>();
    List<GameObject> toRemove = new List<GameObject>();
    List<ICollidable> sceneColliders = new List<ICollidable>();
    public TextureAtlas atlas;
    internal Player player;
    public SceneUI sceneUI { get; private set; }

    public void AddObject(GameObject obj)
    {
        if (!sceneObjects.Contains(obj))
        {
            sceneObjects.Add(obj);
            UpdateColliders();
        }
    }
    public Scene(ContentManager content)
    { //, SceneUI sUI) {
        //idk
        //sceneUI = sUI;
    }
    public virtual void EnterScene()
    {
        //load scene UI
        foreach (GameObject g in sceneObjects)
        {
            if (g.ActiveOnSceneEnter)
            {
                g.SetActive(true);
            }
            else
            {
                g.SetActive(false);
            }
            //this might need modification, ex. object pools should not be active at enter.
        }
        CollisionManager.Instance.AddCollidableList(sceneColliders);
    }
    public virtual void ExitScene()
    {
        //unload Scene UI
        foreach (GameObject g in sceneObjects)
        {
            g.SetActive(false);
        }
        CollisionManager.Instance.RemoveColliderList(sceneColliders);
    }
    public void RemoveObject(GameObject obj)
    {
        if (sceneObjects.Contains(obj))
        {
            obj.SetActive(false);
            toRemove.Add(obj);
        }
    }
    public void UpdateColliders()
    {
        sceneColliders.Clear();
        //empty slate, so removing is easy
        foreach (GameObject o in sceneObjects)
        {
            if (o is ICollidable col && !sceneColliders.Contains(col))
            {
                sceneColliders.Add(col);
            }
        }
        //refacto
    }
    public virtual void Update(GameTime gameTime)
    {
        foreach (GameObject g in sceneObjects)
        {
            if (toRemove.Contains(g))
            {
                sceneObjects.Remove(g);
                toRemove.Remove(g);
                UpdateColliders();
                continue;
            }
            if (g.IsActive)
            {
                g.Update(gameTime);
            }
        }
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (GameObject g in sceneObjects)
        {
            if (g.IsActive)
            {
                g.Draw(spriteBatch);
            }
        }
    }
}
