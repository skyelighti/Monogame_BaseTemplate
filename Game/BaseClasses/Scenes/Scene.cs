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
    List<GameObject> toAdd = new();
    List<ICollidable> sceneColliders = new List<ICollidable>();
    List<ICollidable> staticSceneColliders = new();
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
        CollisionManager.Instance.AddCollidableList(staticSceneColliders, true);
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
        CollisionManager.Instance.RemoveColliderList(staticSceneColliders);
        isActive = false;
    }

    public void AddObject(GameObject obj, bool isStatic = false)
    {
        if (!sceneObjects.Contains(obj) && !toAdd.Contains(obj))
        {
            toAdd.Add(obj);
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

    public void AddCollider(ICollidable col, bool isStatic = false)
    {
        if (!sceneColliders.Contains(col)&& !staticSceneColliders.Contains(col))
        {
            if (isStatic)
            {
                staticSceneColliders.Add(col);
            }
            else
            {
                sceneColliders.Add(col);
            }
            if (isActive)
            {
                CollisionManager.Instance.AddCollidable(col, isStatic);
            }
        }
    }

    public void RemoveCollider(ICollidable col)
    {
        if (sceneColliders.Contains(col) || staticSceneColliders.Contains(col))
        {
            sceneColliders.Remove(col);
            staticSceneColliders.Remove(col);
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
            g.OnDestroy();
        }
        toRemove.Clear();
        foreach (GameObject obj in toAdd)
        {
            obj.Init();
            sceneObjects.Add(obj);
        }

        toAdd.Clear();
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
    public virtual void ResetScene() { 
    //reset everything
    }

    
}
