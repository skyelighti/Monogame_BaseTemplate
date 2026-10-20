using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase.Assets;

namespace GameBase;
public abstract class Scene
{
    List<GameObject> sceneObjects = new List<GameObject>();
    List<GameObject> toRemove = new List<GameObject>();
    List<ICollidable> sceneColliders = new List<ICollidable>();
    public TextureAtlas atlas;
    public SceneUI sceneUI { get; protected set; }
    public bool isActive { get; protected set; } = false;

    public Scene(ContentManager content)
    { //, SceneUI sUI) {
        //idk
        //sceneUI = sUI;
    }
    public virtual void EnterScene()
    {
        if (isActive) { return; }
        isActive = true;
        sceneUI?.SetActive(true);
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
        if (!isActive) { return; }
        sceneUI?.SetActive(false);
        //unload Scene UI
        foreach (GameObject g in sceneObjects)
        {
            g.SetActive(false);
        }
        CollisionManager.Instance.RemoveColliderList(sceneColliders);
        isActive = false;
    }

    public void AddObject(GameObject obj)
    {
        if (!sceneObjects.Contains(obj))
        {
            sceneObjects.Add(obj);
            if (obj.GetComponents<ICollidable>() != null)
            {
                var cols = obj.GetComponents<ICollidable>();
                foreach (IComponent c in cols) {
                    AddCollider(c as ICollidable);
                }
            }
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if (sceneObjects.Contains(obj))
        {
            obj.SetActive(false);
            toRemove.Add(obj);
        }
    }

    public void AddCollider(ICollidable col)
    {
        if (!sceneColliders.Contains(col))
        {
            sceneColliders.Add(col);
            if (isActive)
            {
                CollisionManager.Instance.AddCollidable(col);
            }
        }
    }

    public void RemoveCollider(ICollidable col)
    {
        if (sceneColliders.Contains(col))
        {
            sceneColliders.Remove(col);
            CollisionManager.Instance.RemoveCollider(col);
        }
    }


    public virtual void Update(GameTime gameTime)
    {
        foreach (GameObject g in sceneObjects)
        {
            if (g.IsActive)
            {
                g.Update(gameTime);
            }
        }
        foreach (GameObject g in toRemove)
        {
            sceneObjects.Remove(g);
            if (g.GetComponents<ICollidable>() != null)
            {
                var cols = g.GetComponents<ICollidable>();
                foreach (IComponent c in cols)
                {
                    RemoveCollider(c as ICollidable);
                }
            }
        }
        toRemove.Clear();
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
