﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Font : DLink
    {
        public Name name;
        public FontSprite pFontSprite;
        static private String pNullString = "null";

        public enum Name
        {
            TestMessage,
            Score1,
            Score2,
            ScoreHigh,
            TitleString,
            ScoreTableString,
            Life,
            NullObject,
            Uninitialized
        };

        public Font()
            : base()
        {
            this.name = Name.Uninitialized;
            this.pFontSprite = new FontSprite();
        }

        ~Font()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Font():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.pFontSprite = null;
        }

        public void UpdateMessage(String pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(this.pFontSprite != null);
            this.pFontSprite.UpdateMessage(pMessage);
        }

        public String GetMessage()
        {
            return this.pFontSprite.GetMessage();
        }

        public void Set(Font.Name name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            this.pFontSprite.Set(name, pMessage, glyphName, xStart, yStart);
        }

        public void Wash()
        {
            this.name = Name.Uninitialized;
            this.pFontSprite.Set(Font.Name.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        public void Dump()
        {
        }
    }
}
