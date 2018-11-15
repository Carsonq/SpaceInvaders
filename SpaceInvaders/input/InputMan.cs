using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputMan
    {
        private static InputMan pInstance = null;
        private bool privSpaceKeyPrev;
        private bool privTKeyPrev;
        private bool privRKeyPrev;
        private bool priv1KeyPrev;
        private bool priv2KeyPrev;
        private bool privSLashKeyPrev;

        private InputSubject pSubjectArrowRight;
        private InputSubject pSubjectArrowLeft;
        private InputSubject pSubjectSpace;
        private InputSubject pSubjectT;
        private InputSubject pSubjectR;
        private InputSubject pSubject1;
        private InputSubject pSubject2;
        private InputSubject pSubjectSlash;

        private InputMan()
        {
            this.pSubjectArrowLeft = new InputSubject();
            this.pSubjectArrowRight = new InputSubject();
            this.pSubjectSpace = new InputSubject();
            this.pSubjectT = new InputSubject();
            this.pSubjectR = new InputSubject();
            this.pSubject1 = new InputSubject();
            this.pSubject2 = new InputSubject();
            this.pSubjectSlash = new InputSubject();

            this.privSpaceKeyPrev = false;
            this.privTKeyPrev = false;
            this.privRKeyPrev = false;
            this.priv1KeyPrev = false;
            this.priv2KeyPrev = false;
            this.privSLashKeyPrev = false;
        }

        private static InputMan PrivGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InputMan();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        public static void Destroy()
        {
            pInstance = null;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowLeft;
        }

        public static InputSubject GetSpaceSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectSpace;
        }

        public static InputSubject GetTSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectT;
        }

        public static InputSubject GetRSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectR;
        }

        public static InputSubject Get1Subject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubject1;
        }

        public static InputSubject Get2Subject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubject2;
        }

        public static InputSubject GetSlashSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectSlash;
        }

        public static void Update()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // LeftKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                pMan.pSubjectArrowLeft.Notify();
            }

            // RightKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                pMan.pSubjectArrowRight.Notify();
            }

            // SpaceKey: (with key history) -----------------------------------------------------------
            bool spaceKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);
            if (spaceKeyCurr == true && pMan.privSpaceKeyPrev == false)
            {
                pMan.pSubjectSpace.Notify();
            }

            // Toggle the collision boxes ---------------------------------
            bool tKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_T);
            if (tKeyCurr == true && pMan.privTKeyPrev == false)
            {
                pMan.pSubjectT.Notify();
            }

            // Toggle the Shield ---------------------------------
            bool rKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_R);
            if (rKeyCurr == true && pMan.privRKeyPrev == false)
            {
                pMan.pSubjectR.Notify();
            }

            // start game
            bool oneKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1);
            if (oneKeyCurr == true && pMan.priv1KeyPrev == false)
            {
                pMan.pSubject1.Notify();
            }

            bool twoKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2);
            if (twoKeyCurr == true && pMan.priv2KeyPrev == false)
            {
                pMan.pSubject2.Notify();
            }

            bool slashKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SLASH);
            if (slashKeyCurr == true && pMan.privSLashKeyPrev == false)
            {
                pMan.pSubjectSlash.Notify();
            }

            pMan.privSpaceKeyPrev = spaceKeyCurr;
            pMan.privTKeyPrev = tKeyCurr;
            pMan.privRKeyPrev = rKeyCurr;
            pMan.priv1KeyPrev = oneKeyCurr;
            pMan.priv2KeyPrev = twoKeyCurr;
            pMan.privSLashKeyPrev = slashKeyCurr;
        }
    }
}
