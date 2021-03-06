using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Hypnosister
{
    public class HypnosisView : UIView
    {
        public HypnosisView ()
        {
            BackgroundColor = UIColor.Clear;
            CircleColor = UIColor.LightGray;
        }
        
        UIColor _CircleColor;
        public UIColor CircleColor { 
            get { return _CircleColor; }
            set { 
                _CircleColor = value;
                SetNeedsDisplay();
            }
        }
        
        public override void Draw (RectangleF rect)
        {
            base.Draw (rect);
            var ctx = UIGraphics.GetCurrentContext();
            
            var center = new PointF(Bounds.X + Bounds.Width / 2.0f, Bounds.Y + Bounds.Height / 2.0f);
            
            var maxRadius = distance(Bounds.Width, Bounds.Height) / 2.0f;
            
            ctx.SetLineWidth(10);
            CircleColor.SetStroke();
            
            for (float currentRadius = maxRadius; currentRadius > 0; currentRadius -= 20.0f) {
                ctx.AddArc(center.X, center.Y, currentRadius, 0.0f, (float)Math.PI * 2.0f, true);
                ctx.StrokePath();
            }
            
            var text = new NSString("You are getting sleepy.");
            var font = UIFont.BoldSystemFontOfSize(28.0f);
            var textSize = text.StringSize(font);
            
            UIColor.Black.SetFill();
            
            var offset = new SizeF(4.0f, 3.0f);
            ctx.SetShadowWithColor(offset, 2.0f, UIColor.DarkGray.CGColor);
            
            text.DrawString(new PointF(center.X - textSize.Width / 2.0f, center.Y - textSize.Height / 2.0f), font);
        }
        
        // In the book they call the C function hypot()
        float distance(float x, float y)
        {
            return (float)Math.Sqrt(x*x + y*y);
        }
        
        public override bool CanBecomeFirstResponder {
            get { return true; }
        }
        
        public override void MotionBegan (UIEventSubtype motion, UIEvent evt)
        {
            base.MotionBegan (motion, evt);
            if (motion != UIEventSubtype.MotionShake) {
                return;
            }
            
            Console.WriteLine("Device started shaking!");
            CircleColor = UIColor.Red;
        }
    }
}

