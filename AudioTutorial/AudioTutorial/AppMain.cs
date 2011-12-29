using System;
using System.Collections.Generic;

using Sce.Pss.Core;
using Sce.Pss.Core.Environment;
using Sce.Pss.Core.Graphics;
using Sce.Pss.Core.Input;
using Sce.Pss.Core.Audio;
using Sce.Pss.HighLevel.UI;

namespace AudioTutorial
{
	public class AppMain
	{
		private static GraphicsContext graphics;
		private static Scene scene;
		
		
		// Load in our MP3
		private static Bgm bgm = new Bgm("cheering.mp3");
		private static BgmPlayer player = bgm.CreatePlayer();
		
		public static void Main (string[] args)
		{
			Initialize ();
			
			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}

		public static void Initialize ()
		{
			// Set up the graphics system
			graphics = new GraphicsContext ();
			UISystem.Initialize(graphics);
			
			// Initiliaze a new scene
			scene = new Scene();
			
			// Build a play button
			Button play_btn = new Button();
			play_btn.X = 50f;
			play_btn.Y = 50f;
			play_btn.Width = 50f;
			play_btn.Height = 50f;
			play_btn.ButtonAction += HandlePlay_btnButtonAction;
			CustomButtonImageSettings play_btn_bg = new CustomButtonImageSettings();
			ImageAsset play_image = new ImageAsset("play.png");
			play_btn_bg.BackgroundNormalImage = play_image;
			play_btn_bg.BackgroundDisabledImage = play_image;
			play_btn_bg.BackgroundPressedImage = play_image;
			play_btn.CustomImage = play_btn_bg;
			play_btn.Style = ButtonStyle.Custom;
			// Add the button to the scene			
			scene.RootWidget.AddChildFirst(play_btn);
			
			
			// Build a Pause button
			Button pause_btn = new Button();
			pause_btn.X = 120f;
			pause_btn.Y = 50f;
			pause_btn.Width = 50f;
			pause_btn.Height = 50f;
			pause_btn.ButtonAction += HandlePause_btnButtonAction;
			CustomButtonImageSettings pause_btn_bg = new CustomButtonImageSettings();
			ImageAsset pause_image = new ImageAsset("pause.png");
			pause_btn_bg.BackgroundNormalImage = pause_image;
			pause_btn_bg.BackgroundDisabledImage = pause_image;
			pause_btn_bg.BackgroundPressedImage = pause_image;
			pause_btn.CustomImage = pause_btn_bg;
			pause_btn.Style = ButtonStyle.Custom;
			// Add the button to the scene			
			scene.RootWidget.AddChildFirst(pause_btn);
			
			
			// Build a Stop button
			Button stop_btn = new Button();
			stop_btn.X = 190f;
			stop_btn.Y = 50f;
			stop_btn.Width = 50f;
			stop_btn.Height = 50f;
			stop_btn.ButtonAction += HandleStop_btnButtonAction;
			CustomButtonImageSettings stop_btn_bg = new CustomButtonImageSettings();
			ImageAsset stop_image = new ImageAsset("stop.png");
			stop_btn_bg.BackgroundNormalImage = stop_image;
			stop_btn_bg.BackgroundDisabledImage = stop_image;
			stop_btn_bg.BackgroundPressedImage = stop_image;
			stop_btn.CustomImage = stop_btn_bg;
			stop_btn.Style = ButtonStyle.Custom;
			// Add the button to the scene			
			scene.RootWidget.AddChildFirst(stop_btn);
			
			// Set the scene
			UISystem.SetScene(scene, null);
		}

		static void HandlePlay_btnButtonAction (object sender, TouchEventArgs e)
		{
			// Check that the BgmPlayer is not already playing. If it is and we re-call Play(), it will crash the app.
			if(player.Status != BgmStatus.Playing)
			{
				player.Play();
			}
		}
		
		static void HandlePause_btnButtonAction (object sender, TouchEventArgs e)
		{
			if(player.Status == BgmStatus.Playing)
			{
				player.Pause();
			}
		}
		
		static void HandleStop_btnButtonAction (object sender, TouchEventArgs e)
		{
			player.Stop();
		}
		
		public static void Update ()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			
			// Query touch for current state
            List<TouchData> touchDataList = Touch.GetData (0);

            // Update UI Toolkit
            UISystem.Update(touchDataList);
		}

		public static void Render ()
		{
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();
			UISystem.Render();
			// Present the screen
			graphics.SwapBuffers ();
		}
	}
}
