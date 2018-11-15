using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneMan
    {
        private static SceneMan instance = null;

        // Active
        private Scene pScene;

        // Reference
        private SceneStateTract pStateTract;
        private SceneStateGame pStateGame;
        private SceneStateGameover pStateGameover;

        public enum State
        {
            Tract,
            Game,
            Gameover
        }

        private SceneMan()
        {
            // Store the states
            this.pStateTract = new SceneStateTract();
            this.pStateGame = new SceneStateGame();
            this.pStateGameover = new SceneStateGameover();

            // set active
            this.pScene = null;
        }

        public static void Create()
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new SceneMan();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            instance.pScene = ActivateScene();
            instance.pScene.SetState(SceneMan.State.Tract);
        }

        private static SceneMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Scene GetScene()
        {
            SceneMan pSceneMan = SceneMan.PrivInstance();

            Debug.Assert(pSceneMan != null);
            Debug.Assert(pSceneMan.pScene != null);

            return pSceneMan.pScene;
        }

        public static SceneState GetState(State state)
        {
            SceneMan pSceneMan = SceneMan.PrivInstance();
            Debug.Assert(pSceneMan != null);

            SceneState pSceneState = null;

            switch (state)
            {
                case SceneMan.State.Tract:
                    pSceneState = pSceneMan.pStateTract;
                    break;

                case SceneMan.State.Game:
                    pSceneState = pSceneMan.pStateGame;
                    break;

                case SceneMan.State.Gameover:
                    pSceneState = pSceneMan.pStateGameover;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pSceneState;
        }

        private static Scene ActivateScene()
        {
            SceneMan pSceneMan = SceneMan.PrivInstance();
            Debug.Assert(pSceneMan != null);

            Scene pScene = new Scene();
            pSceneMan.pScene = pScene;

            return pSceneMan.pScene;
        }

        public static void ChangeSceneInternal(GameObject pGameObject)
        {
            ForwardIterator pFor = new ForwardIterator(pGameObject);

            Component pNode = pFor.First();
            pFor.Next();
            if (pFor.IsDone())
            {
                GameObject pUFORoot = GameObjectMan.Find(GameObject.Name.UFORoot);
                UFO pUFO = (UFO)Iterator.GetChild(pUFORoot);
                pUFO.StopSound();

                int mode = SceneStateGame.GetPlayMode();
                int currLevel = SceneStateGame.GetCurrLevel();

                String pScore1 = Int32.Parse(FontMan.Find(Font.Name.Score1).GetMessage()).ToString().PadLeft(4, '0');
                SceneStateGame.SetScore1(pScore1);

                String pScore2 = Int32.Parse(FontMan.Find(Font.Name.Score2).GetMessage()).ToString().PadLeft(4, '0');
                SceneStateGame.SetScore2(pScore2);

                String pScoreHigh = Int32.Parse(FontMan.Find(Font.Name.ScoreHigh).GetMessage()).ToString().PadLeft(4, '0');
                SceneStateGame.SetScoreHigh(pScoreHigh);

                SceneStateGame.SetStay(true);

                int currPlayer = SceneStateGame.GetCurrPlayer();

                if (currLevel == 1)
                {
                    SceneStateGame.SetPlayerLevel(currPlayer, 2);
                    SceneStateGame.SetBaseY(450.0f);
                    SceneStateGame.SetMoveRate(1.0f);
                }
                else
                {
                    SceneStateGame.SetPlayerLevel(currPlayer, 1);
                    SceneStateGame.SetBaseY(600.0f);
                    SceneStateGame.SetMoveRate(1.5f);
                    //no need to change to the next player, when finish level 2, same player, back to level 1
                    //SceneStateGame.SetCurrPlayer(currPlayer == mode ? 1 : 2);
                }

                //currLevel = SceneStateGame.GetCurrLevel();
                //if (currLevel == 1)
                //{
                //    SceneStateGame.SetBaseY(600.0f);
                //    SceneStateGame.SetMoveRate(1.5f);
                //}
                //else
                //{
                //    SceneStateGame.SetBaseY(450.0f);
                //    SceneStateGame.SetMoveRate(1.0f);
                //}

                Scene pScene = SceneMan.GetScene();
                pScene.Unload();
            }
        }
    }
}