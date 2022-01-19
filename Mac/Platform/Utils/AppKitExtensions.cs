using AppKit;
using CoreGraphics;
using Microsoft.Toolkit.Diagnostics;
using System;

namespace Mac.Platform.Utils
{
    public static class AppKitExtensions
    {
        // FUTURE: top left / top right / bottom … corners to avoid two calls
        // FUTURE: SafeAreaInsets / SafeAreaRect

        public static void AlignTopToParentTop(this NSView view, float padding = 0)
        {
            var parent = view.Superview;
            Guard.IsNotNull(parent, nameof(parent));
            AlignTopToTop(view, parent.Bounds, padding);
        }

        public static void AlignTopToTop(this NSView view, CGRect targetFrame, float padding = 0)
        {
            var newY = targetFrame.Y + targetFrame.Height - padding - view.Frame.Height;
            var newPosition = new CGPoint(view.Frame.X, newY);
            view.Frame = new CGRect(newPosition, view.Frame.Size);
        }

        public static void AlignLeadingToParentLeading(this NSView view, float padding = 0)
        {
            var parent = view.Superview;
            Guard.IsNotNull(parent, nameof(parent));
            AlignLeadingToLeading(view, parent.Bounds, padding);
        }

        public static void AlignLeadingToLeading(this NSView view, CGRect targetFrame, float padding = 0)
        {
            if (view.UserInterfaceLayoutDirection == NSUserInterfaceLayoutDirection.LeftToRight)
            {
                var newX = targetFrame.X + padding;
                var newPosition = new CGPoint(newX, view.Frame.Y);
                view.Frame = new CGRect(newPosition, view.Frame.Size);
            }
            else
            {
                // https://developer.apple.com/library/archive/documentation/MacOSX/Conceptual/BPInternational/SupportingRight-To-LeftLanguages/SupportingRight-To-LeftLanguages.html
                throw new NotImplementedException();
            }
        }
    }
}
