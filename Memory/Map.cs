using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
    class Map
    {
        Game1 game;
        int[] array = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        List<Obrazek> lista = new List<Obrazek>();
        Random RandomNumber = new Random();
        Obrazek o1,o2;
        MouseState Prevstate;
        MouseState state;

        SoundEffect good = Globals.contentManager.Load<SoundEffect>("SFX/good");
        SoundEffect bad = Globals.contentManager.Load<SoundEffect>("SFX/bad");
        SoundEffect click = Globals.contentManager.Load<SoundEffect>("SFX/click");


        int waitingtime = 1000;
        int liczba = 0;
        int time = 0;

        // ROZMIAR OBRAZKA
        const int ImageSize = 128;
        //ILOSC OBRAZKOW W RZĘDZIE
        const int Row = 5;

        public Map(Game1 game)
        {
            this.game = game;
        }
      public void Initialize()
        {
            InitList();
        }
        public void Draw()
        {
            foreach (Obrazek image in lista)
                image.Draw(Globals.spriteBatch);
        }
        public void Update(GameTime gametime)
        {
            Prevstate = state;
            if (liczba != 2)
                state = Mouse.GetState();

           
           

                if (time >= waitingtime)
                {
                    time = 0;
                    liczba = 0;
                    foreach (Obrazek image in lista)
                    {
                        image.Clicked = false;
                    }
                }
            

            if (EndGame(lista))
            {
                game.State = GameState.End;
            }

            if (liczba > 1)
            {
                time += (int)gametime.ElapsedGameTime.TotalMilliseconds;
            }
          
            foreach (Obrazek obrazek in lista)
            {
                obrazek.Update(gametime);
                if (obrazek.rectangleSize.Contains(new Point(state.X, state.Y)))
                {
                    if (state.LeftButton == ButtonState.Pressed && Prevstate.LeftButton == ButtonState.Released && obrazek.Clicked == false && obrazek.Alive)
                    {
                        o2 = o1;
                        o1 = obrazek;

                        obrazek.Texture = obrazek.Original;
                        obrazek.Clicked = true;
                        click.Play();
                        liczba += 1;
                    }
                    if (time > waitingtime)
                    {
                        
                        if (o1.Texture==o2.Texture)
                        {
                            var i1 = lista.IndexOf(o1);
                            var i2 = lista.IndexOf(o2);

                            lista.ElementAt(i1).Alive = false;
                            lista.ElementAt(i2).Alive = false;
                            good.Play();
                            break;
                        }
                        else
                        {
                            bad.Play();
                        }
                    }



                }
            }
        }

        private bool EndGame(List<Obrazek> lista)
        {
            foreach(Obrazek obraz in lista)
            {
                if (obraz.Alive == true)
                    return false;
            }
            return true;

        }
        private void InitList()
        {
            int licznik = 0;
            int X = 0, Y = 0;



            int[] MyRandomArray = array.OrderBy(x => RandomNumber.Next()).ToArray();


            for (int i = 0; i < 20; i++)
            {

                if (licznik % Row == 0)
                {

                    Y += licznik * 25;
                    X = 0;
                    licznik = 0;
                }
                X = licznik * ImageSize;
                licznik++;


                Texture2D texture = Losowanie(MyRandomArray[i]);

                Obrazek obrazek = new Obrazek(texture, new Rectangle(50 + X, 50 + Y, ImageSize, ImageSize), new Rectangle(0, 0, ImageSize, ImageSize), i);
                obrazek.AlternateTexture = Globals.contentManager.Load<Texture2D>("Images/block");

                lista.Add(obrazek);
            }
        }

        private Texture2D Losowanie(int rng)
        {
            Texture2D texture;

            switch (rng)
            {
                case 0:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/bike");
                        break;
                    }
                case 1:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/book");
                        break;
                    }
                case 2:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/computer");
                        break;
                    }
                case 3:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/face");
                        break;
                    }
                case 4:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/headphones");
                        break;
                    }
                case 5:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/house");
                        break;
                    }
                case 6:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/milk");
                        break;
                    }
                case 7:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/phone");
                        break;
                    }
                case 8:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/pumpkin");
                        break;
                    }
                case 9:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/user");
                        break;
                    }
                default:
                    {
                        texture = Globals.contentManager.Load<Texture2D>("Images/block");
                        break;
                    }
                    
            }
            return texture;
        }   
    
    }
}
