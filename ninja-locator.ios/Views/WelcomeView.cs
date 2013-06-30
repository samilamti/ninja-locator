using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using NinjaLocator.Core.iOS;

namespace NinjaLocator.iOS.Views
{
    [Register("WelcomeView")]
    public class WelcomeView : UIView
    {
        public event Action Done;

        public WelcomeView()
        {
            Initialize();
        }

        public WelcomeView(RectangleF bounds)
            : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            AddGestureRecognizer(new UITapGestureRecognizer(recognizer => {
                RemoveGestureRecognizer(recognizer);
                RemoveFromSuperview();
                Done();
            }));

            UIDefinitions userInterfaceDefinitions = new UIDefinitions();
            BackgroundColor = userInterfaceDefinitions.BackgroundColor;
            userInterfaceDefinitions.ForegroundColor.SetColor();
            DrawString("welcome to ninja locator", new PointF(5, 5), UIFont.FromName("Arial", 36));
        }

        public override void Draw(RectangleF rect)
        {
            UIDefinitions userInterfaceDefinitions = new UIDefinitions();
            userInterfaceDefinitions.BackgroundColor.SetFill();
            UIGraphics.RectFill(rect);

            const float padding = 15;
            UIFont headerFont = UIFont.BoldSystemFontOfSize(10);
            UIFont textFont = UIFont.SystemFontOfSize(14);
            UIColor foregroundColor = UIColor.White;
            UIColor titleColor = userInterfaceDefinitions.ForegroundColor;
            
            float verticalPosition = padding;

            foregroundColor.SetColor();
            verticalPosition += DrawString("WELCOME TO", new PointF(padding, verticalPosition), headerFont).Height;                  

            titleColor.SetColor();
            foregroundColor.SetStroke();
            verticalPosition += DrawString("ninja locator",
                                           new RectangleF(padding, verticalPosition, Frame.Width - padding * 2, 36),
                                           UIFont.ItalicSystemFontOfSize(36), UILineBreakMode.Clip,
                                           UITextAlignment.Center).Height;

            foregroundColor.SetColor();
            verticalPosition += padding;
            verticalPosition += DrawString("ABOUT", new PointF(padding, verticalPosition), headerFont).Height;

            const float estimatedNumberOfLines = 10;
            DrawString(@"This app lets you connect to fellow Ninjas, Cowboys or another group of your choosing. 

Simply log in, using Twitter, Facebook, Google or Microsoft, pick a nick name and a group to join.

Everyone in the same group, will share their locations and see eachother on a map, making hooking-up an easy task!",
                new RectangleF(padding, verticalPosition, Frame.Width - padding * 2, estimatedNumberOfLines * textFont.LineHeight),
                textFont, UILineBreakMode.WordWrap);

            titleColor.SetColor();
            DrawString("OK, I get it - let me in!", new RectangleF(padding, Frame.Height - 40, Frame.Width - padding * 2, 40),
                       UIFont.SystemFontOfSize(24), UILineBreakMode.Clip, UITextAlignment.Center);

            base.Draw(rect);
        }
    }
}