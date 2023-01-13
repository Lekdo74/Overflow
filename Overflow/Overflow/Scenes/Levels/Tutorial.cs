using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Overflow.src;
using System.Collections.Generic;
using MonoGame.Extended.Sprites;

namespace Overflow.Scenes
{
    public class Tutorial : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public Tutorial(Main game) : base(game) { }

        private Map mapTuto;
        private Room currentRoom;

        private bool challenge;
        private bool challenge2;

        private static float remainingTimeBeforeLoadingLevelOne = 3f;

        public override void Initialize()
        {
            base.Initialize();
            Sound.ChangeBackgroundMusic(Sound.tutorial);

            Player.Tutorial = true;
            mapTuto = new Map(3, new int[] { 3, 3 }, Art.tilesetLevel1, Art.enemysetLevel1, Sound.tutorial, true);

            currentRoom = mapTuto.Rooms[mapTuto.CurrentRoom[0], mapTuto.CurrentRoom[1]];

            Player.InitializePlayer();
            Player.Position = currentRoom.SpawnPoint;


            Player.CanPassThroughDoor = true;
            Player.PreviousTile = currentRoom.GetPlayerTile();
            Player.CurrentTile = currentRoom.GetPlayerTile();
            Player.PastTutorial = false;


            challenge = true;
            challenge2 = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            PlayerInputs.KeyBoardState = Keyboard.GetState();
            PlayerInputs.MouseState = Mouse.GetState();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Player.CurrentTile = currentRoom.GetPlayerTile();

            Player.Update(gameTime, currentRoom);
            Player.TakeDamage(Player.CheckDamage(currentRoom));

            if (mapTuto.CurrentRoom[0] == 0 && currentRoom.Enemies.Count == 0)
            {
                Player.PastTutorial = true;
                Player.Tutorial = false;
            }

            if (!challenge2)
            {
                remainingTimeBeforeLoadingLevelOne -= deltaTime;
            }


            ChangeRoom();
            mapTuto.Update(gameTime, Game);



            if (Player.CurrentTile != Player.PreviousTile)
            {
                foreach (Enemy enemy in currentRoom.Enemies)
                {
                    enemy.CalculPath = true;
                }
            }

            Player.PreviousTile = Player.CurrentTile;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Main._renderTarget);
            Game.GraphicsDevice.Clear(Color.Black);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            mapTuto.Draw(gameTime, Main._spriteBatch);
            Player.Draw(gameTime, Main._spriteBatch);
            PlayerHealthBar.Draw(Main._spriteBatch);
            Main._spriteBatch.DrawString(Art.fontLittle, "Clic gauche pour attaquer \nZ : Haut\nQ : Gauche\nS : Bas\nD : Droite\nShift ou Espace : Dash", new Vector2(20, 30), Color.White);
            
            if (mapTuto.CurrentRoom[0] == 2)
                Main._spriteBatch.DrawString(Art.fontLittle, "N'oublie pas, le dash te rend invincible", new Vector2(20, 215), Color.White);
            
            if (mapTuto.CurrentRoom[0] == 1)
            {
                if (currentRoom.Enemies.Count <= 0)
                    Main._spriteBatch.DrawString(Art.fontLittle, "Bravo ! Tu as tue tous les ennemis\nTu peux passer a la prochaine salle", new Vector2(283, 40), Color.White);
                else
                    Main._spriteBatch.DrawString(Art.fontLittle, "Tue tous les ennemis pour\nacceder a la prochaine salle", new Vector2(300, 40), Color.White);
            }

            if (challenge2)
            {
                if (currentRoom.Enemies.Count > 0 && mapTuto.CurrentRoom[0] == 0)
                {
                    Main._spriteBatch.DrawString(Art.fontLittle, $"Te voila dans la derniere salle\nTue tous les ennemis : {4 - currentRoom.Enemies.Count}/4 pour completer le tutoriel", new Vector2(260, 220), Color.White);
                }
                else if (currentRoom.Enemies.Count <= 0 && mapTuto.CurrentRoom[0] == 0)
                {
                    challenge2 = false;
                }
            }
            else
            {
                Main._spriteBatch.DrawString(Art.fontLittle, "Felicitations !", new Vector2(180, 180), Color.Gold);
                if(remainingTimeBeforeLoadingLevelOne <= 0)
                    Game.LoadLevel1();
            }
            if (mapTuto.CurrentRoom[0] == 1)
            {
                if (challenge)
                {
                    Main._spriteBatch.DrawString(Art.fontLittle, "Essaye d'eviter un \nprojectile avec ton dash : 0/1", new Vector2(300, 230), Color.White);


                    if (currentRoom.Projectiles.Count > 0)
                    {
                        Projectile closestProjectile = currentRoom.Projectiles[0];
                        foreach (Projectile projectile in currentRoom.Projectiles)
                        {
                            if (Vector2.Distance(projectile.Position, Player.Position) < Vector2.Distance(closestProjectile.Position, Player.Position))
                            {
                                closestProjectile = projectile;
                            }
                        }
                        Rectangle closestProjectileAugmantedRectangle = new Rectangle((int)closestProjectile.Position.X - 7, (int)closestProjectile.Position.Y - 7, closestProjectile.Texture.Width + 14, closestProjectile.Texture.Height + 14);
                        if (Player.DashTimeRemaining > 0 && closestProjectileAugmantedRectangle.Intersects(Player.Rectangle))
                        {
                            challenge = false;
                        }

                    }
                }
                else
                {
                    Main._spriteBatch.DrawString(Art.fontLittle, "La classe ! \nEviter une attaque\navec ton dash : 1/1", new Vector2(300, 230), Color.White);
                }
            }

            if (Player.TimeBeforeNextDash > 0)
            {
                Main._spriteBatch.DrawString(Art.fontLittle, "Recharge du dash", new Vector2(360, 15), Color.Blue);
            }
            if (Player.TimeBeforeNextAttack > 0)
            {
                Main._spriteBatch.DrawString(Art.fontLittle, "Recharge de l'attaque", new Vector2(360, 30), Color.Red);
            }


            Main._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Main._spriteBatch.Draw(Main._renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Main._spriteBatch.End();
        }

        private void ChangeRoom()
        {
            if (!Player.CanPassThroughDoor && (Player.PreviousTile.Type == "DoorTop" || Player.PreviousTile.Type == "DoorRight" || Player.PreviousTile.Type == "DoorBottom" || Player.PreviousTile.Type == "DoorLeft") && Player.CurrentTile.Type == "Grass")
            {
                Player.CanPassThroughDoor = true;
            }

            if (Player.CanPassThroughDoor)
            {
                switch (Player.CurrentTile.Type)
                {
                    case "DoorTop":
                        mapTuto.CurrentRoom = new int[] { mapTuto.CurrentRoom[0], mapTuto.CurrentRoom[1] - 1 };
                        currentRoom = mapTuto.Rooms[mapTuto.CurrentRoom[0], mapTuto.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[2];
                        ChangedRoom();
                        break;
                    case "DoorRight":
                        mapTuto.CurrentRoom = new int[] { mapTuto.CurrentRoom[0] + 1, mapTuto.CurrentRoom[1] };
                        currentRoom = mapTuto.Rooms[mapTuto.CurrentRoom[0], mapTuto.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[3];
                        ChangedRoom();
                        break;
                    case "DoorBottom":
                        mapTuto.CurrentRoom = new int[] { mapTuto.CurrentRoom[0], mapTuto.CurrentRoom[1] + 1 };
                        currentRoom = mapTuto.Rooms[mapTuto.CurrentRoom[0], mapTuto.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[0];
                        ChangedRoom();
                        break;
                    case "DoorLeft":
                        mapTuto.CurrentRoom = new int[] { mapTuto.CurrentRoom[0] - 1, mapTuto.CurrentRoom[1] };
                        currentRoom = mapTuto.Rooms[mapTuto.CurrentRoom[0], mapTuto.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[1];
                        ChangedRoom();
                        break;
                }
            }
        }

        private void ChangedRoom()
        {
            Sound.PlaySound(Sound.changeRoom);
            Player.Position -= new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2);
            Player.GhostEffectDashes = new List<GhostEffectDash>();
            Player.CanPassThroughDoor = false;
        }
    }
}
