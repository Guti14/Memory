using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MyGameLib;

namespace Memory
{
    class Obrazek : Animation
    {
        private bool Once = false;
        public bool Alive
        {
            get;
            set;
        }
        public int ID
        {
            get;
            set;
        }
        public Texture2D AlternateTexture
        {
            get;
            set;
        }
        public Texture2D Original
        {
            get;
            set;
        }
        public bool Clicked
        {
            get;
            set;
        }

        public Obrazek(Texture2D texture, Rectangle rectangleSize, Rectangle rectangleCropp,int ID) : base(texture,rectangleSize,rectangleCropp)
        {
            this.ID = ID;
            AlternateTexture = Globals.contentManager.Load<Texture2D>("Images/block");
            Original = texture;
            Clicked = false;
            Alive = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Alive)
            {
                if (Clicked)
                    spriteBatch.Draw(Texture, rectangleSize, color);
                else
                    spriteBatch.Draw(AlternateTexture, rectangleSize, color);
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (Game1.Time<0)
            {
                Clicked = true;
                
            }
            else
            {
                if (!Once)
                {
                    Clicked = false;
                    Once = true;
                }

                base.Update(gameTime);

            }
        }

    }
}
