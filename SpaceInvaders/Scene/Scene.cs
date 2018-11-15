using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Scene
    {
        private SceneState state;

        public Scene()
        {
            this.state = null;
        }

        public void SetState(SceneMan.State inState)
        {
            this.state = SceneMan.GetState(inState);
        }

        public void LoadContent()
        {
            this.state.LoadContent();
        }

        public void Update(float systemTime)
        {
            this.state.Update(systemTime);
        }

        public void Draw()
        {
            this.state.Draw();
        }

        public void Unload()
        {
            this.state.Unload();
        }

        public SceneState GetSceneState()
        {
            return this.state;
        }
    }
}
