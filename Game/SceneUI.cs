using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gum;


//each scene should hold a reference to it's own UI, and handle loading and unloading
public class SceneUI
{
    Scene scene;
    //it should hold a reference to the scene its drawing ontop of? 
    public SceneUI(Scene s)
    {
        scene = s;
    }
    public void ShowUI()
    {
        //this should show all the UI objects
        //this should is for scene loading
    }
    public void HideUI()
    {
        //this should hide all the UI objects
        //mainly for scene unload
    }
}

