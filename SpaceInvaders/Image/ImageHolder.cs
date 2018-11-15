using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageHolder : SLink
    {
        private Image pImange;

        public ImageHolder(Image image)
            :base()
        {
            this.pImange = image;
        }

        public Image GetpImange()
        {
            return this.pImange;
        }
    }
}
