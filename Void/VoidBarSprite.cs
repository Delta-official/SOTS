using System;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SOTS.Void
{
    class VoidBarSprite : UIElement
    {
        public Color backgroundColor = Color.White;
        internal static Texture2D _backgroundTexture;

        public VoidBarSprite()
        {
            if (_backgroundTexture == null)
                _backgroundTexture = ModContent.GetTexture("SOTS/Void/VoidBarSprite");
			
        }
		protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            if (_backgroundTexture != null)
                spriteBatch.Draw(_backgroundTexture, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
        }
    }
}