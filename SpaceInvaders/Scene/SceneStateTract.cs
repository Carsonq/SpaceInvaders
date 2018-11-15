using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SceneStateTract : SceneState
    {
        private static String scoreHigh;

        public static void SetScoreHigh(String s)
        {
            if (SceneStateTract.scoreHigh == null || Int32.Parse(s) > Int32.Parse(SceneStateTract.scoreHigh))
            {
                SceneStateTract.scoreHigh = s;
            }
        }

        // state()
        override public void Handle()
        {
            Scene pScene = SceneMan.GetScene();
            pScene.SetState(SceneMan.State.Game);
            SceneStateGame.Reset();
            pScene.LoadContent();
        }

        // strategy()
        override public void LoadContent()
        {
            SpriteBatchMan.Create(3, 1);
            TextureMan.Create(2, 1);
            GlyphMan.Create(3, 1);
            FontMan.Create(1, 1);
            ImageMan.Create(5, 2);
            GameSpriteMan.Create(4, 2);
            ProxySpriteMan.Create(10, 1);
            BoxSpriteMan.Create(3, 1);
            GameObjectMan.Create(3, 1);
            GhostGameObjectMan.Create(3, 1);

            //ImageMan.Create(5, 2);
            //GameSpriteMan.Create(4, 2);
            //ImageMan.Create(5, 2);
            //GameSpriteMan.Create(4, 2);
            //GameObjectMan.Create(3, 1);
            //ProxySpriteMan.Create(10, 1);


            TextureMan.Add(Texture.Name.Aliens, "newaliens.tga");
            Texture pTexture = TextureMan.Add(Texture.Name.Consolas20pt, "Consolas20pt.tga");

            FontMan.AddXml(Glyph.Name.Consolas20pt, "Consolas20pt.xml", Texture.Name.Consolas20pt);

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);
            SpriteBatch pSB_Aliens = SpriteBatchMan.Add(SpriteBatch.Name.Aliens);

            // Font
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.Consolas20pt, 100, 730);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas20pt, 300, 730);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.Consolas20pt, 500, 730);
            FontMan.Add(Font.Name.Score1, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas20pt, 100, 700);

            if (scoreHigh == null)
            {
                scoreHigh = "0000";
            }
            FontMan.Add(Font.Name.ScoreHigh, SpriteBatch.Name.Texts, scoreHigh, Glyph.Name.Consolas20pt, 300, 700);
            FontMan.Add(Font.Name.Score2, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas20pt, 500, 700);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PLAY", Glyph.Name.Consolas20pt, 320, 550);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SPACE    INVADERS", Glyph.Name.Consolas20pt, 260, 500);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PUSH <1> OR <2> PLAYERS BUTTON", Glyph.Name.Consolas20pt, 200, 450);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "*SCORE    ADVANCE    TABLE*", Glyph.Name.Consolas20pt, 220, 400);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "  =  200 POINTS", Glyph.Name.Consolas20pt, 270, 350);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "  =  30 POINTS", Glyph.Name.Consolas20pt, 270, 300);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "  =  20 POINTS", Glyph.Name.Consolas20pt, 270, 250);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "  =  10 POINTS", Glyph.Name.Consolas20pt, 270, 200);


            // Alien
            ImageMan.Add(Image.Name.SquidA, Texture.Name.Aliens, 547, 15, 250, 135);
            ImageMan.Add(Image.Name.CrabA, Texture.Name.Aliens, 281, 15, 250, 135);
            ImageMan.Add(Image.Name.OctopusA, Texture.Name.Aliens, 15, 15, 250, 135);
            ImageMan.Add(Image.Name.UFO, Texture.Name.Aliens, 80, 500, 230, 100);

            GameSpriteMan.Add(GameSprite.Name.Squid, Image.Name.SquidA, 100, 600, 35, 30, 255, 255, 255, 1);
            GameSpriteMan.Add(GameSprite.Name.Crab, Image.Name.CrabA, 100, 550, 35, 30, 255, 255, 255, 1);
            GameSpriteMan.Add(GameSprite.Name.Octopus, Image.Name.OctopusA, 100, 500, 35, 30, 255, 255, 255, 1);
            GameSpriteMan.Add(GameSprite.Name.UFO, Image.Name.UFO, 100, 500, 35, 30, 255, 0, 0, 1);

            GameObject pAlienGroup = new AlienGroup(GameObject.Name.AlienGroup, GameSprite.Name.NullObject, 100, 100);
            pAlienGroup.ActivateGameSprite(pSB_Aliens);

            GameObject pAlienColumn = new AlienGroup(GameObject.Name.AlienColumn, GameSprite.Name.NullObject, 100, 100);
            pAlienGroup.Add(pAlienColumn);
            pAlienColumn.ActivateGameSprite(pSB_Aliens);

            GameObject pSquid = new Squid(GameObject.Name.Squid, GameSprite.Name.Squid, 250, 300);
            pAlienColumn.Add(pSquid);
            pSquid.ActivateGameSprite(pSB_Aliens);

            GameObject pCrab = new Crab(GameObject.Name.Crab, GameSprite.Name.Crab, 250, 250);
            pAlienColumn.Add(pCrab);
            pCrab.ActivateGameSprite(pSB_Aliens);

            GameObject pOctopus = new Octopus(GameObject.Name.Octopus, GameSprite.Name.Octopus, 250, 200);
            pAlienColumn.Add(pOctopus);
            pOctopus.ActivateGameSprite(pSB_Aliens);

            UFO pUFO = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 250, 350, null);
            pAlienColumn.Add(pUFO);
            pUFO.ActivateGameSprite(pSB_Aliens);

            GameObjectMan.Attach(pAlienGroup);


            // Input
            InputSubject pInputSubject;
            pInputSubject = InputMan.Get1Subject();
            pInputSubject.Attach(new StartGameObserver(1));

            pInputSubject = InputMan.Get2Subject();
            pInputSubject.Attach(new StartGameObserver(2));
        }
        override public void Update(float systemTime)
        {
            //Font pTestMessage = FontMan.Find(Font.Name.TitleString);
            //Debug.Assert(pTestMessage != null);
            //pTestMessage.UpdateMessage("dog " + count++);
            GameObjectMan.Update();
            InputMan.Update();
        }

        override public void Draw()
        {
            //Scene pScene = SceneMan.GetScene();
            //if (SceneMan.GetScene().GetSceneState() == this)
            //{
                SpriteBatchMan.Draw();
            //}
        }
        override public void Unload()
        {
            //SpriteBatchMan.Remove(pSB_Texts);
            //SpriteBatchMan.Remove(pSB_Texts);

            SpriteBatchMan.Destroy();
            TextureMan.Destroy();
            GlyphMan.Destroy();
            FontMan.Destroy();
            ImageMan.Destroy();
            GameSpriteMan.Destroy();
            BoxSpriteMan.Destroy();
            ProxySpriteMan.Destroy();
            GameObjectMan.Destroy();
            InputMan.Destroy();

            this.Handle();
        }
    }
}
