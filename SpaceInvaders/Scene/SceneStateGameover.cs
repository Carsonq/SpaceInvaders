using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SceneStateGameover : SceneState
    {
        private static String score1;
        private static String score2;
        private static String scoreHigh;

        public static void SetScore1(String s)
        {
            SceneStateGameover.score1 = s;
        }

        public static void SetScore2(String s)
        {
            SceneStateGameover.score2 = s;
        }

        public static void SetScoreHigh(String s)
        {
            if (SceneStateGameover.scoreHigh == null || Int32.Parse(s) > Int32.Parse(SceneStateGameover.scoreHigh))
            {
                SceneStateGameover.scoreHigh = s;
            }
        }

        // state()
        override public void Handle()
        {
            Scene pScene = SceneMan.GetScene();
            pScene.SetState(SceneMan.State.Tract);
            pScene.LoadContent();
        }

        private void Reset()
        {
            SceneStateGame.SetScore1("0000");
            SceneStateGame.SetScore2("0000");

            SceneStateGame.SetBaseY(600.0f);
            SceneStateGame.SetPlayerLevel(1, 1);
            SceneStateGame.SetPlayerLevel(2, 1);

            SceneStateGame.SetMoveRate(1.5f);

            AlienGroup.ResetDirection();
        }

        // strategy()
        override public void LoadContent()
        {
            Reset();

            SpriteBatchMan.Create(3, 1);
            TextureMan.Create(2, 1);
            GlyphMan.Create(3, 1);
            FontMan.Create(1, 1);

            Texture pTexture = TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            FontMan.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            pTexture = TextureMan.Add(Texture.Name.Consolas20pt, "Consolas20pt.tga");
            FontMan.AddXml(Glyph.Name.Consolas20pt, "Consolas20pt.xml", Texture.Name.Consolas20pt);

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);

            // Font
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Game Over", Glyph.Name.Consolas36pt, 250, 550);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas20pt, 200, 450);
            FontMan.Add(Font.Name.ScoreHigh, SpriteBatch.Name.Texts, SceneStateGameover.scoreHigh, Glyph.Name.Consolas20pt, 400, 450);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PLAYER <1>", Glyph.Name.Consolas20pt, 200, 400);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, SceneStateGameover.score1, Glyph.Name.Consolas20pt, 400, 400);


            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PLAYER <2>", Glyph.Name.Consolas20pt, 200, 350);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, SceneStateGameover.score2, Glyph.Name.Consolas20pt, 400, 350);

            // Input
            InputSubject pInputSubject;
            pInputSubject = InputMan.GetSlashSubject();
            pInputSubject.Attach(new SelectGameObserver());
        }

        override public void Update(float systemTime)
        {
            InputMan.Update();
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
            InputMan.Destroy();
            GhostGameObjectMan.Destroy();

            this.Handle();
        }
    }
}
