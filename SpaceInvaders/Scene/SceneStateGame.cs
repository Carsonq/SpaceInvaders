using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SceneStateGame : SceneState
    {
        public static IrrKlang.ISoundEngine sndEngine = null;
        static int currPlayer = 1;
        static int playMode;
        static int player1Life = 3;
        static int player2Life = 3;
        static int player1Level = 1;
        static int player2Level = 1;
        static float pMoveRate = 1.5f;
        static bool pStay = false;
        static String score1 = "0000";
        static String score2 = "0000";
        static String scoreHigh = "0000";
        static float baseY = 600.0f;
        static bool loadGhost = false;
        public static void Reset()
        {
            SceneStateGame.currPlayer = 1;
            SceneStateGame.player1Life = 3;
            SceneStateGame.player2Life = 3;
            SceneStateGame.player1Level = 1;
            SceneStateGame.player2Level = 1;
            SceneStateGame.pMoveRate = 1.5f;
            SceneStateGame.pStay = false;
            SceneStateGame.baseY = 600.0f;
            SceneStateGame.score1 = "0000";
            SceneStateGame.score2 = "0000";
            SceneStateGame.loadGhost = false;
        }

        public static void SetCurrPlayer(int c)
        {
            SceneStateGame.currPlayer = c;
        }

        public static void SetPlayMode(int p)
        {
            SceneStateGame.playMode = p;
        }

        public static void ChangePlayer(int p)
        {
            SceneStateGame.currPlayer = p;
        }

        public static void SetScore1(String s)
        {
            SceneStateGame.score1 = s;
        }

        public static void SetBaseY(float y)
        {
            SceneStateGame.baseY = y;
        }

        public static void SetPlayerLevel(int p, int y)
        {
            if (p == 1)
            {
                player1Level = y;
            }
            else if (p == 2)
            {
                player2Level = y;
            }
            else
            {
                Debug.Assert(false);
            }
        }

        public static void SetScore2(String s)
        {
            SceneStateGame.score2 = s;
        }

        public static void SetScoreHigh(String s)
        {
            SceneStateGame.scoreHigh = s;
        }

        public static void SetLoadGhost(bool a)
        {
            SceneStateGame.loadGhost = a;
        }

        public static void SetStay(bool s)
        {
            pStay = s;
        }

        public static void SetMoveRate(float m)
        {
            pMoveRate = m;
        }

        public static int GetPlayMode()
        {
            return playMode;
        }

        public static int GetCurrPlayer()
        {
            return currPlayer;
        }

        public static int GetCurrLevel()
        {
            return currPlayer == 1 ? player1Level : player2Level;
        }

        public static int GetPlayerLife(int p)
        {
            return p == 1 ? player1Life : player2Life;
        }

        public static void SetPlayerLife(int p, int l)
        {
            if (p == 1)
            {
                player1Life = l;
            }
            else if (p == 2)
            {
                player2Life = l;
            }
            else
            {
                Debug.Assert(false);
            }
        }

        // state()
        override public void Handle()
        {
            Scene pScene = SceneMan.GetScene();
            if (pStay == false)
            {
                pScene.SetState(SceneMan.State.Gameover);
            }

            pScene.LoadContent();
        }

        // strategy()
        override public void LoadContent()
        {
            AlienGroup.ResetDirection();

            TextureMan.Create(2, 1);
            ImageMan.Create(5, 2);
            GameSpriteMan.Create(4, 2);
            BoxSpriteMan.Create(3, 1);
            SpriteBatchMan.Create(3, 1);
            TimerMan.Create(3, 1);
            ProxySpriteMan.Create(10, 1);
            GameObjectMan.Create(3, 1);
            ColPairMan.Create(1, 1);
            Simulation.Create();
            GlyphMan.Create(3, 1);
            FontMan.Create(1, 1);
            //GhostSpriteBatchMan.Create(2, 1);

            //---------------------------------------------------------------------------------------------------------
            // Sound Experiment
            //---------------------------------------------------------------------------------------------------------

            // start up the engine
            sndEngine = new IrrKlang.ISoundEngine();

            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------

            TextureMan.Add(Texture.Name.Aliens, "newaliens.tga");
            TextureMan.Add(Texture.Name.Shields, "birds_N_shield.tga");
            Texture pTexture = TextureMan.Add(Texture.Name.Consolas20pt, "Consolas20pt.tga");
            FontMan.AddXml(Glyph.Name.Consolas20pt, "Consolas20pt.xml", Texture.Name.Consolas20pt);

            //---------------------------------------------------------------------------------------------------------
            // Load the Images
            //---------------------------------------------------------------------------------------------------------

            ImageMan.Add(Image.Name.SquidA, Texture.Name.Aliens, 547, 15, 250, 135);
            ImageMan.Add(Image.Name.CrabA, Texture.Name.Aliens, 281, 15, 250, 135);
            ImageMan.Add(Image.Name.OctopusA, Texture.Name.Aliens, 15, 15, 250, 135);

            ImageMan.Add(Image.Name.SquidB, Texture.Name.Aliens, 547, 170, 250, 135);
            ImageMan.Add(Image.Name.CrabB, Texture.Name.Aliens, 281, 170, 250, 135);
            ImageMan.Add(Image.Name.OctopusB, Texture.Name.Aliens, 15, 170, 250, 135);

            ImageMan.Add(Image.Name.MissileBombCol, Texture.Name.Aliens, 395, 480, 130, 130);
            ImageMan.Add(Image.Name.AlienDies, Texture.Name.Aliens, 550, 480, 220, 130);

            ImageMan.Add(Image.Name.UFO, Texture.Name.Aliens, 80, 500, 230, 100);
            ImageMan.Add(Image.Name.UFODies, Texture.Name.Aliens, 15, 630, 355, 140);

            ImageMan.Add(Image.Name.Missile, Texture.Name.Aliens, 370, 795, 30, 105);
            ImageMan.Add(Image.Name.MissileBombDies, Texture.Name.Aliens, 685, 790, 110, 130);
            ImageMan.Add(Image.Name.Ship, Texture.Name.Aliens, 50, 325, 190, 125);
            ImageMan.Add(Image.Name.ShipDiesA, Texture.Name.Aliens, 280, 325, 245, 130);
            ImageMan.Add(Image.Name.ShipDiesB, Texture.Name.Aliens, 545, 325, 245, 130);

            ImageMan.Add(Image.Name.BombRoll, Texture.Name.Aliens, 445, 795, 50, 105);
            ImageMan.Add(Image.Name.BombZigZag, Texture.Name.Aliens, 560, 630, 70, 140);
            ImageMan.Add(Image.Name.BombCross, Texture.Name.Aliens, 110, 790, 50, 100);
            ImageMan.Add(Image.Name.BombFork, Texture.Name.Aliens, 520, 790, 50, 100);
            ImageMan.Add(Image.Name.BombStraight, Texture.Name.Aliens, 370, 795, 30, 105);

            ImageMan.Add(Image.Name.Brick, Texture.Name.Shields, 20, 210, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top0, Texture.Name.Shields, 15, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top1, Texture.Name.Shields, 15, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Shields, 35, 215, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top0, Texture.Name.Shields, 75, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top1, Texture.Name.Shields, 75, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Bottom, Texture.Name.Shields, 55, 215, 10, 5);

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------

            GameSpriteMan.Add(GameSprite.Name.Squid, Image.Name.SquidA, 100, 600, 35, 30, 255, 255, 255, 1);
            GameSpriteMan.Add(GameSprite.Name.Crab, Image.Name.CrabA, 100, 550, 35, 30, 255, 255, 255, 1);
            GameSpriteMan.Add(GameSprite.Name.Octopus, Image.Name.OctopusA, 100, 500, 35, 30, 255, 255, 255, 1);
            GameSpriteMan.Add(GameSprite.Name.AlienDies, Image.Name.AlienDies, 0, 0, 35, 30, 255, 255, 255, 1);
            GameSpriteMan.Add(GameSprite.Name.UFO, Image.Name.UFO, 100, 500, 35, 30, 255, 0, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.UFODies, Image.Name.UFODies, 0, 0, 35, 30, 255, 0, 0, 1);

            GameSpriteMan.Add(GameSprite.Name.Missile, Image.Name.Missile, 100, 200, 10, 20, 255, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.MissileDies, Image.Name.MissileBombDies, 0, 0, 10, 20, 255, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.MissileBombCol, Image.Name.MissileBombCol, 0, 0, 10, 20, 255, 255, 255, 1);
            GameSpriteMan.Add(GameSprite.Name.Ship, Image.Name.Ship, 300, 30, 40, 20, 255, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.ShipDies, Image.Name.ShipDiesA, 0, 0, 50, 25, 255, 255, 0, 1);

            GameSpriteMan.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, 0, 0, 10, 20, 255, 0, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.BombRolling, Image.Name.BombRoll, 0, 0, 10, 20, 255, 0, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.BombDagger, Image.Name.BombCross, 0, 0, 10, 20, 255, 0, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.BombStraight, Image.Name.BombStraight, 0, 0, 10, 20, 255, 0, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.BombFork, Image.Name.BombFork, 0, 0, 12, 24, 255, 0, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.BombDies, Image.Name.MissileBombDies, 0, 0, 10, 20, 255, 0, 0, 1);

            GameSpriteMan.Add(GameSprite.Name.Brick, Image.Name.Brick, 50, 25, 10, 5, 0, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, 10, 5, 0, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, 10, 5, 0, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, 10, 5, 0, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, 10, 5, 0, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, 10, 5, 0, 255, 0, 1);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, 10, 5, 0, 255, 0, 1);

            SpriteBatch pSB_Aliens = SpriteBatchMan.Add(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Box = SpriteBatchMan.Add(SpriteBatch.Name.Boxes);
            SpriteBatch pSB_Shields = SpriteBatchMan.Add(SpriteBatch.Name.Shields);
            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);

            //SpriteBatch pSB_GhostAliens = GhostSpriteBatchMan.Add(SpriteBatch.Name.Aliens);
            //SpriteBatch pSB_GhostBox = GhostSpriteBatchMan.Add(SpriteBatch.Name.Boxes);
            //SpriteBatch pSB_GhostShields = GhostSpriteBatchMan.Add(SpriteBatch.Name.Shields);

            //---------------------------------------------------------------------------------------------------------
            // GameObject
            //---------------------------------------------------------------------------------------------------------

            AnimationSprite pAnimSpriteSquid = new AnimationSprite(GameSprite.Name.Squid);
            AnimationSprite pAnimSpriteCrab = new AnimationSprite(GameSprite.Name.Crab);
            AnimationSprite pAnimSpriteOctopus = new AnimationSprite(GameSprite.Name.Octopus);

            pAnimSpriteSquid.Attach(Image.Name.SquidB);
            pAnimSpriteSquid.Attach(Image.Name.SquidA);
            pAnimSpriteCrab.Attach(Image.Name.CrabB);
            pAnimSpriteCrab.Attach(Image.Name.CrabA);
            pAnimSpriteOctopus.Attach(Image.Name.OctopusB);
            pAnimSpriteOctopus.Attach(Image.Name.OctopusA);

            TimerMan.Add(TimerEvent.Name.SquidAnimation, pAnimSpriteSquid, pMoveRate);
            TimerMan.Add(TimerEvent.Name.CrabAnimation, pAnimSpriteCrab, pMoveRate);
            TimerMan.Add(TimerEvent.Name.OctopusAnimation, pAnimSpriteOctopus, pMoveRate);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------

            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateGameSprite(pSB_Aliens);
            pWallGroup.ActivateCollisionSprite(pSB_Box);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, GameSprite.Name.NullObject, 336, 35, 750, 10);
            pWallBottom.ActivateCollisionSprite(pSB_Box);
            pWallGroup.Add(pWallBottom);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, GameSprite.Name.NullObject, 722, 384, 120, 765);
            pWallRight.ActivateCollisionSprite(pSB_Box);
            pWallGroup.Add(pWallRight);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, GameSprite.Name.NullObject, -45, 384, 120, 765);
            pWallLeft.ActivateCollisionSprite(pSB_Box);
            pWallGroup.Add(pWallLeft);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, GameSprite.Name.NullObject, 336, 685, 750, 10);
            pWallTop.ActivateCollisionSprite(pSB_Box);
            pWallGroup.Add(pWallTop);

            GameObjectMan.Attach(pWallGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Bumper
            //---------------------------------------------------------------------------------------------------------

            BumperGroup pBumperGroup = new BumperGroup(GameObject.Name.BumperGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pBumperGroup.ActivateGameSprite(pSB_Aliens);
            pBumperGroup.ActivateCollisionSprite(pSB_Box);

            BumperRight pBumperRight = new BumperRight(GameObject.Name.BumperRight, GameSprite.Name.NullObject, 650, 55, 15, 20);
            pBumperRight.ActivateCollisionSprite(pSB_Box);
            pBumperGroup.Add(pBumperRight);

            BumperLeft pBumperLeft = new BumperLeft(GameObject.Name.BumperLeft, GameSprite.Name.NullObject, 20, 55, 15, 20);
            pBumperLeft.ActivateCollisionSprite(pSB_Box);
            pBumperGroup.Add(pBumperLeft);

            GameObjectMan.Attach(pBumperGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Ship
            //---------------------------------------------------------------------------------------------------------

            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pShipRoot.ActivateCollisionSprite(pSB_Box);
            GameObjectMan.Attach(pShipRoot);
            ShipMan.Create(new SndObserver(sndEngine, SndObserver.Name.ShootMissile));

            Ship pShip = new Ship(GameObject.Name.Ship, GameSprite.Name.Ship, 50, 20, null);
            pShip.ActivateCollisionSprite(pSB_Box);
            pShip.ActivateGameSprite(pSB_Aliens);
            pShipRoot.Add(pShip);

            //---------------------------------------------------------------------------------------------------------
            // Bomb
            //---------------------------------------------------------------------------------------------------------

            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pBombRoot.ActivateCollisionSprite(pSB_Box);

            GameObjectMan.Attach(pBombRoot);

            //---------------------------------------------------------------------------------------------------------
            // Explosion
            //---------------------------------------------------------------------------------------------------------

            ExplosionFactory explosionFactory = new ExplosionFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);
            ExplosionGroup pExplosionGroup = (ExplosionGroup)explosionFactory.Create(GameObject.Name.ExplosionGroup, GameSprite.Name.NullObject);
            GameObjectMan.Attach(pExplosionGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Missile
            //---------------------------------------------------------------------------------------------------------

            MissileGroup pMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, GameSprite.Name.NullObject, 0, 0.0f, 0.0f);
            pMissileGroup.ActivateGameSprite(pSB_Aliens);
            pMissileGroup.ActivateCollisionSprite(pSB_Box);
            GameObjectMan.Attach(pMissileGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------
            AlienGroup pAlienGroup;

            if (SceneStateGame.loadGhost == false)
            {
                GameObject pGameObj;
                AlienMan.Create();

                AlienFactory alienFactory = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);
                pAlienGroup = (AlienGroup)alienFactory.Create(GameObject.Name.AlienGroup, AlienCategory.Type.Group);

                for (int i = 0; i < 11; i++)
                {
                    float x = 100.0f + 35 * (i % 11);

                    GameObject pGameObjCol = alienFactory.Create(GameObject.Name.AlienColumn, AlienCategory.Type.Column, pAlienGroup);

                    pGameObj = alienFactory.Create(GameObject.Name.Squid, AlienCategory.Type.Squid, pGameObjCol, x, baseY);
                    pGameObj = alienFactory.Create(GameObject.Name.Crab, AlienCategory.Type.Crab, pGameObjCol, x, baseY - 30);
                    pGameObj = alienFactory.Create(GameObject.Name.Crab, AlienCategory.Type.Crab, pGameObjCol, x, baseY - 30 * 2);
                    pGameObj = alienFactory.Create(GameObject.Name.Octopus, AlienCategory.Type.Octopus, pGameObjCol, x, baseY - 30 * 3);
                    pGameObj = alienFactory.Create(GameObject.Name.Octopus, AlienCategory.Type.Octopus, pGameObjCol, x, baseY - 30 * 4);
                }
            }
            else
            {
                GameObjectNode pGhostGameObjNode = GhostGameObjectMan.Find(GameObject.Name.AlienGroup);

                pAlienGroup = (AlienGroup)pGhostGameObjNode.pGameObj;
                ForwardIterator pFor = new ForwardIterator(pAlienGroup);

                Component pNode = pFor.First();
                while (!pFor.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;

                    pGameObj.ActivateGameSprite(pSB_Aliens);
                    pGameObj.ActivateCollisionSprite(pSB_Box);

                    pNode = pFor.Next();
                }
                GhostGameObjectMan.Remove(pGhostGameObjNode);
            }

            GameObjectMan.Attach(pAlienGroup);

            MovementSprite pMvSprite = new MovementSprite(pAlienGroup);
            pMvSprite.Attach(10.0f, 20.0f, new SndObserver(sndEngine, SndObserver.Name.AlienMove4, 0.5f));
            pMvSprite.Attach(10.0f, 20.0f, new SndObserver(sndEngine, SndObserver.Name.AlienMove3, 0.5f));
            pMvSprite.Attach(10.0f, 20.0f, new SndObserver(sndEngine, SndObserver.Name.AlienMove2, 0.5f));
            pMvSprite.Attach(10.0f, 20.0f, new SndObserver(sndEngine, SndObserver.Name.AlienMove1, 0.5f));
            TimerMan.Add(TimerEvent.Name.AlienMovement, pMvSprite, pMoveRate);

            //---------------------------------------------------------------------------------------------------------
            // UFO 
            //---------------------------------------------------------------------------------------------------------
            UFORoot pUFORoot = new UFORoot(GameObject.Name.UFORoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pUFORoot.ActivateCollisionSprite(pSB_Box);
            GameObjectMan.Attach(pUFORoot);
            UFOMan.Create(new SndObserver(sndEngine, SndObserver.Name.UFOFlyHigh, 0.2f, true));

            //---------------------------------------------------------------------------------------------------------
            // Shield 
            //---------------------------------------------------------------------------------------------------------
            
            ShieldFactory shieldFactory = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes);
            ShieldRoot pShieldRoot = (ShieldRoot)shieldFactory.Create(ShieldCategory.Type.Root, GameObject.Name.NullObject);
            GameObjectMan.Attach(pShieldRoot);

            // load by column
            for (int i = 0; i < 4; i++)
            {
                int j = 0;
                float start_x = 86.0f + i * 146;
                float start_y = 100.0f;
                float off_x = 0;
                float brickWidth = 10.0f;
                float brickHeight = 5.0f;

                ShieldGrid pShieldGrid = (ShieldGrid)shieldFactory.Create(ShieldCategory.Type.Grid, GameObject.Name.NullObject, pShieldRoot);

                GameObject pColumn;
                pColumn = shieldFactory.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++, pShieldGrid);

                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x, start_y);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + 2 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + 3 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + 4 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + 5 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + 6 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + 7 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + 8 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, pColumn, start_x, start_y + 9 * brickHeight);

                pColumn = shieldFactory.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++, pShieldGrid);

                off_x += brickWidth;
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 2 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 3 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 4 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 5 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 6 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 7 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 8 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = shieldFactory.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++, pShieldGrid);

                off_x += brickWidth;
                shieldFactory.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 2 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 3 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 4 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 5 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 6 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 7 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 8 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = shieldFactory.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++, pShieldGrid);

                off_x += brickWidth;
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 3 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 4 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 5 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 6 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 7 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 8 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = shieldFactory.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++, pShieldGrid);

                off_x += brickWidth;
                shieldFactory.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 2 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 3 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 4 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 5 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 6 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 7 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 8 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = shieldFactory.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++, pShieldGrid);

                off_x += brickWidth;
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 0 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 1 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 2 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 3 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 4 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 5 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 6 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 7 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 8 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = shieldFactory.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++, pShieldGrid);

                off_x += brickWidth;
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 0 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 1 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 2 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 3 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 4 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 5 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 6 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 7 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 8 * brickHeight);
                shieldFactory.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, pColumn, start_x + off_x, start_y + 9 * brickHeight);
            }
            
            //---------------------------------------------------------------------------------------------------------
            // ColPair 
            //---------------------------------------------------------------------------------------------------------

            // associate in a collision pair
            ColPair pColPair = ColPairMan.Add(ColPair.Name.Alien_Wall, pAlienGroup, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new GridObserver());
            //pColPair.Attach(new SndObserver(sndEngine, SndObserver.Name.Alien_Wall));

            // Missile vs Wall
            pColPair = ColPairMan.Add(ColPair.Name.Missile_Wall, pMissileGroup, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new ExplosionObserver(explosionFactory, GameObject.Name.MissileDies, GameSprite.Name.MissileDies, pExplosionGroup, 1));


            // Bomb vs Bottom
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Wall, pBombRoot, pWallGroup);
            pColPair.Attach(new BombObserver(1));
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new ExplosionObserver(explosionFactory, GameObject.Name.BombDies, GameSprite.Name.BombDies, pExplosionGroup, 1));

            // UFO vs Wall
            pColPair = ColPairMan.Add(ColPair.Name.UFO_Wall, pUFORoot, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveUFOObserver());
            pColPair.Attach(new UFOReadyObserver(sndEngine));

            // Missle vs UFO
            pColPair = ColPairMan.Add(ColPair.Name.Missile_UFO, pMissileGroup, pUFORoot);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new SndObserver(sndEngine, SndObserver.Name.Missile_Shield));
            pColPair.Attach(new RemoveUFOObserver2());
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new UFOReadyObserver(sndEngine));
            pColPair.Attach(new ScoreObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new ExplosionObserver(explosionFactory, GameObject.Name.UFODies, GameSprite.Name.UFODies, pExplosionGroup, 2));

            // missile vs alien
            pColPair = ColPairMan.Add(ColPair.Name.Alien_Missile, pMissileGroup, pAlienGroup);
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new ScoreObserver());
            pColPair.Attach(new ExplosionObserver(explosionFactory, GameObject.Name.AlienDies, GameSprite.Name.AlienDies, pExplosionGroup, 2));
            pColPair.Attach(new GridRemoveAlienObserver());
            pColPair.Attach(new SndObserver(sndEngine, SndObserver.Name.Missile_Alien));
            pColPair.Attach(new AlienNumObserver(sndEngine));

            // Missile vs Shield
            pColPair = ColPairMan.Add(ColPair.Name.Misslie_Shield, pMissileGroup, pShieldRoot);
            pColPair.Attach(new SndObserver(sndEngine, SndObserver.Name.Missile_Shield));
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new RemoveBrickObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new ExplosionObserver(explosionFactory, GameObject.Name.MissileDies, GameSprite.Name.MissileDies, pExplosionGroup, 1));

            // Bomb vs Shield
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Shield, pBombRoot, pShieldRoot);
            pColPair.Attach(new SndObserver(sndEngine, SndObserver.Name.Bomb_Shield));
            pColPair.Attach(new BombObserver(1));
            pColPair.Attach(new RemoveBrickObserver());
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new ExplosionObserver(explosionFactory, GameObject.Name.BombDies, GameSprite.Name.BombDies, pExplosionGroup, 1));

            // Bomb vs Ship pay attention to the order
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Ship, pBombRoot, pShipRoot);
            pColPair.Attach(new SndObserver(sndEngine, SndObserver.Name.Bomb_Ship));
            pColPair.Attach(new BombObserver(1));
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new RemoveShipObserver());
            pColPair.Attach(new LifeObserver());
            pColPair.Attach(new AnimExplosionObserver(explosionFactory, GameObject.Name.ShipDies, GameSprite.Name.ShipDies, pExplosionGroup, 2));
            pColPair.Attach(new CreateShipObserver(sndEngine));

            // Ship vs Bumper
            pColPair = ColPairMan.Add(ColPair.Name.Ship_Bumper, pShipRoot, pBumperGroup);
            pColPair.Attach(new ShipMoveObserver());


            // Missle vs Bomb
            pColPair = ColPairMan.Add(ColPair.Name.Missile_Bomb, pMissileGroup, pBombRoot);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new BombObserver(2));
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new RemoveBomb2Observer());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new ExplosionObserver(explosionFactory, GameObject.Name.MissileBombCol, GameSprite.Name.MissileBombCol, pExplosionGroup, 1));

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------

            InputSubject pInputSubject;
            pInputSubject = InputMan.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputMan.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputMan.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());

            pInputSubject = InputMan.GetTSubject();
            pInputSubject.Attach(new ToggleObserver());

            pInputSubject = InputMan.GetRSubject();
            pInputSubject.Attach(new ToggleShieldObserver());

            Simulation.SetState(Simulation.State.Realtime);

            //---------------------------------------------------------------------------------------------------------
            // Font
            //---------------------------------------------------------------------------------------------------------
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.Consolas20pt, 100, 730);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas20pt, 300, 730);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.Consolas20pt, 500, 730);
            FontMan.Add(Font.Name.Score1, SpriteBatch.Name.Texts, score1, Glyph.Name.Consolas20pt, 100, 700);
            FontMan.Add(Font.Name.ScoreHigh, SpriteBatch.Name.Texts, scoreHigh, Glyph.Name.Consolas20pt, 300, 700);
            FontMan.Add(Font.Name.Score2, SpriteBatch.Name.Texts, score2, Glyph.Name.Consolas20pt, 500, 700);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "X", Glyph.Name.Consolas20pt, 80, 17);
            FontMan.Add(Font.Name.Life, SpriteBatch.Name.Texts, GetPlayerLife(GetCurrPlayer()).ToString(), Glyph.Name.Consolas20pt, 100, 17);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Player " + currPlayer.ToString(), Glyph.Name.Consolas20pt, 480, 17);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, currPlayer == 1 ? "Level " + player1Level.ToString() : "Level " + player2Level.ToString(), Glyph.Name.Consolas20pt, 570, 17);

            //---------------------------------------------------------------------------------------------------------
            // State settings
            //---------------------------------------------------------------------------------------------------------
            SceneStateGame.SetLoadGhost(false);
        }
        override public void Update(float systemTime)
        {
            // Snd update - keeps everything moving and updating smoothly
            sndEngine.Update();

            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            InputMan.Update();

            // Run based on simulation stepping
            if (Simulation.GetTimeStep() > 0.0f)
            {
                // Fire off the timer events
                TimerMan.Update(Simulation.GetTotalTime());

                // walk through all objects and push to flyweight
                GameObjectMan.Update();

                // Do the collision checks
                ColPairMan.Process();

                // Delete any objects here...
                DelayedObjectMan.Process();
            }
        }
        override public void Draw()
        {
            SpriteBatchMan.Draw();
        }

        override public void Unload()
        {
            SpriteBatchMan.Destroy();
            TextureMan.Destroy();
            GlyphMan.Destroy();
            FontMan.Destroy();
            ImageMan.Destroy();
            GameSpriteMan.Destroy();
            BoxSpriteMan.Destroy();
            ProxySpriteMan.Destroy();
            GameObjectMan.Destroy();
            TimerMan.Destroy();
            ColPairMan.Destroy();
            Simulation.Destroy();
            InputMan.Destroy();

            this.Handle();
        }
    }
}
