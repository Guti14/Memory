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

using System.Reflection;
using System.Windows.Forms;

using MyGameLib;

namespace Memory
{
    public enum GameState { Menu, Playing, End };

    public class Game1 : Microsoft.Xna.Framework.Game
    {


        public static float Time = 0;
        string wynik;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;



        Song song;

        SoundEffect SFX0;

        Map Level;


        private GameState state = GameState.Menu;
        public GameState State
        {
            get { return state; }
            set
            {

                switch (value)
                {

                    case GameState.Menu:
                        {
                            state = GameState.Menu;
                            break;
                        }
                    case GameState.Playing:
                        {
                            Time = -3;
                            state = GameState.Playing;
                            Level = new Map(this);
                            Level.Initialize();

                            break;
                        }

                    case GameState.End:
                        {
                            state = GameState.End;

                            break;
                        }
                }

            }
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 740;
            graphics.PreferredBackBufferHeight = 612;
            Content.RootDirectory = "Content";


        }


        protected override void Initialize()
        {





            base.Initialize();
            Globals.Initialize(spriteBatch, Content, graphics);
            IsMouseVisible = true;





            Level = new Map(this);
            Level.Initialize();

            SFX0 = Globals.contentManager.Load<SoundEffect>("SFX/yep");
            song = Globals.contentManager.Load<Song>("Songs/music");
            font = Globals.contentManager.Load<SpriteFont>("font");

        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);




        }
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                

                Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                KeyboardKeyz.Update();

                switch (State)
                {
                    case GameState.Menu:
                        {

                            if (MediaPlayer.State != MediaState.Playing)
                                MediaPlayer.Play(song);
                            if (KeyboardKeyz.WcisniecieKlawisza(Microsoft.Xna.Framework.Input.Keys.Escape))
                            {
                                Exit();
                            }
                            if (KeyboardKeyz.WcisniecieKlawisza(Microsoft.Xna.Framework.Input.Keys.Space))
                            {
                                State = GameState.Playing;
                                MediaPlayer.Stop();
                                SFX0.Play();
                            }
                            break;
                        }
                    case GameState.Playing:
                        {
                            if (KeyboardKeyz.WcisniecieKlawisza(Microsoft.Xna.Framework.Input.Keys.Escape))
                            {
                                State = GameState.Menu;
                            }

                            Level.Update(gameTime);
                            break;
                        }
                    case GameState.End:
                        {
                            if (KeyboardKeyz.WcisniecieKlawisza(Microsoft.Xna.Framework.Input.Keys.Space))
                            {
                                State = GameState.Menu;
                            }

                            break;
                        }
                }


                base.Update(gameTime);
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            if (IsActive)
            {

                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();


                switch (State)
                {
                    case GameState.Menu:
                        {
                            string enterText = "Press spacebar to PLAY";
                            spriteBatch.DrawString(font, enterText, new Vector2(Window.ClientBounds.Width / 2 - font.MeasureString(enterText).X / 2,
                                Window.ClientBounds.Height / 2 - font.MeasureString(enterText).Y / 2), Color.Black);
                            string endText = "Press escape to QUIT";
                            spriteBatch.DrawString(font, endText, new Vector2(Window.ClientBounds.Width / 2 - font.MeasureString(endText).X / 2,
                              Window.ClientBounds.Height / 2 - font.MeasureString(endText).Y / 2 + 50), Color.Black);
                            break;
                        }
                    case GameState.Playing:
                        {
                            Level.Draw();
                            wynik = String.Format(((Math.Round(Time) == Time) ? "{0:0}" : "{0:0.00}"), Time);

                            if (Time > 0)
                            Globals.spriteBatch.DrawString(font, "Time: " + wynik.ToString(), new Vector2(0, 0), Color.Black);
    

                        break;
                        }
                    case GameState.End:
                        {
                            Globals.spriteBatch.DrawString(font, "Score: " + wynik.ToString(), new Vector2(250, 250), Color.Black);
                            Globals.spriteBatch.DrawString(font, "Press Space",new Vector2(250, 300), Color.Black);


                            break;
                        }
                }


                spriteBatch.End();
                base.Draw(gameTime);
            }
        }
    }
}
