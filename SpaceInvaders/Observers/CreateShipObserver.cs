using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CreateShipObserver : ColObserver
    {
        IrrKlang.ISoundEngine pSndEngine;
        private bool state;
        private Ship pShip;

        public CreateShipObserver(IrrKlang.ISoundEngine pEng)
        {
            this.pSndEngine = pEng;
            this.state = false;
            this.pShip = null;
        }

        public CreateShipObserver(CreateShipObserver b)
        {
            Debug.Assert(b != null);
            this.pSndEngine = b.pSndEngine;
            this.state = b.state;
        }

        public override void Notify()
        {
            this.pShip = (Ship)this.pSubject.pObjB;

            int mode = SceneStateGame.GetPlayMode();
            if (mode == 1)
            {
                int lifeLeft = SceneStateGame.GetPlayerLife(1);
                SceneStateGame.SetPlayerLife(1, lifeLeft - 1);
                lifeLeft = SceneStateGame.GetPlayerLife(1);
                if (lifeLeft == 0)
                {
                    this.state = false;
                    CreateShipObserver pCreateShipObserver = new CreateShipObserver(this);

                    ChangeScene pChangeScene = new ChangeScene(pCreateShipObserver);

                    TimerMan.Add(TimerEvent.Name.AnimShip, pChangeScene, 0.2f, false);

                    this.pShip.SetState(ShipMan.State.End);
                    //DelayedObjectMan.Attach(pCreateShipObserver);
                }
                else
                {
                    ShipMan.Create(new SndObserver(this.pSndEngine, SndObserver.Name.ShootMissile));
                }
            }
            else if (mode == 2)
            {
                int player = SceneStateGame.GetCurrPlayer();
                int lifeLeft = SceneStateGame.GetPlayerLife(player);
                SceneStateGame.SetPlayerLife(player, lifeLeft - 1);

                int currPlayerLifeLeft = SceneStateGame.GetPlayerLife(player);
                int nextPlayerLifeLeft = SceneStateGame.GetPlayerLife(2 - player / 2);

                if (nextPlayerLifeLeft == 0 && currPlayerLifeLeft == 0)
                {
                    this.state = false;
                    CreateShipObserver pCreateShipObserver = new CreateShipObserver(this);
                    ChangeScene pChangeScene = new ChangeScene(pCreateShipObserver);

                    TimerMan.Add(TimerEvent.Name.AnimShip, pChangeScene, 0.2f, false);
                    this.pShip.SetState(ShipMan.State.End);
                    //DelayedObjectMan.Attach(pCreateShipObserver);
                }
                else if (nextPlayerLifeLeft == 0 && currPlayerLifeLeft != 0)
                {
                    ShipMan.Create(new SndObserver(this.pSndEngine, SndObserver.Name.ShootMissile));
                }
                else if (nextPlayerLifeLeft != 0)
                {
                    this.state = true;
                    SceneStateGame.SetCurrPlayer(2 - player / 2);
                    CreateShipObserver pCreateShipObserver = new CreateShipObserver(this);
                    ChangeScene pChangeScene = new ChangeScene(pCreateShipObserver);

                    TimerMan.Add(TimerEvent.Name.AnimShip, pChangeScene, 0.2f, false);
                    this.pShip.SetState(ShipMan.State.End);
                    //DelayedObjectMan.Attach(pCreateShipObserver);
                }
                else
                {
                    Debug.Assert(false);
                }

                int currLevel = SceneStateGame.GetCurrLevel();
                if (currLevel == 1)
                {
                    SceneStateGame.SetBaseY(600.0f);
                    SceneStateGame.SetMoveRate(1.5f);
                }
                else
                {
                    SceneStateGame.SetBaseY(450.0f);
                    SceneStateGame.SetMoveRate(1.0f);
                }
            }
            else
            {
                Debug.Assert(false);
            }
        }

        public override void Execute()
        {
            GameObject pUFORoot = GameObjectMan.Find(GameObject.Name.UFORoot);
            UFO pUFO = (UFO)Iterator.GetChild(pUFORoot);
            pUFO.StopSound();

            String pScore1 = Int32.Parse(FontMan.Find(Font.Name.Score1).GetMessage()).ToString().PadLeft(4, '0');
            String pScore2 = Int32.Parse(FontMan.Find(Font.Name.Score2).GetMessage()).ToString().PadLeft(4, '0');
            String pScoreHigh = Int32.Parse(FontMan.Find(Font.Name.ScoreHigh).GetMessage()).ToString().PadLeft(4, '0');

            if (this.state == false)
            {
                SceneStateGameover.SetScore1(pScore1);
                SceneStateGameover.SetScore2(pScore2);
                SceneStateGameover.SetScoreHigh(pScoreHigh);
            }

            SceneStateGame.SetScore1(pScore1);
            SceneStateGame.SetScore2(pScore2);
            SceneStateGame.SetScoreHigh(pScoreHigh);

            GameObjectNode pGhostGameObj = GhostGameObjectMan.Find(GameObject.Name.AlienGroup);
            if (pGhostGameObj != null)
            {
                SceneStateGame.SetLoadGhost(true);
            }

            GameObject pGameObj = GameObjectMan.Find(GameObject.Name.AlienGroup);
            GhostGameObjectMan.Attach(pGameObj);

            SceneStateGame.SetStay(this.state);
            Scene pScene = SceneMan.GetScene();

            pScene.Unload();
        }
    }
}