using Microsoft.Toolkit.Diagnostics;
using System;
using UIKit;

namespace iOS.Platform
{
    public static class UiKitExtensions
    {
        public static void AddChild(this UIViewController parent, UIViewController child, bool bindToMargins = true)
        {
            Guard.IsNotNull(parent, nameof(parent));
            Guard.IsNotNull(parent.View, nameof(parent.View));
            Guard.IsNotNull(child, nameof(child));
            Guard.IsNotNull(child.View, nameof(child.View));

            parent.AddChildViewController(child);
            parent.View.AddSubview(child.View);

            if (bindToMargins)
                child.View.BindEdgesToSuperviewMargins();
            else
                child.View.BindEdgesToSuperviewEdges();

            child.DidMoveToParentViewController(parent);
        }

        public static void RemoveFromParent(this UIViewController child)
        {
            Guard.IsNotNull(child, nameof(child));
            Guard.IsNotNull(child.View, nameof(child.View));

            child.WillMoveToParentViewController(null);
            child.View.RemoveFromSuperview();
            child.RemoveFromParentViewController();
        }

        public static void BindEdgesToSuperviewMargins(this UIView view)
        {
            Guard.IsNotNull(view, nameof(view));
            Guard.IsNotNull(view.Superview, nameof(view.Superview));

            view.TranslatesAutoresizingMaskIntoConstraints = false;
            view.LeadingAnchor.ConstraintEqualTo(view.Superview.LayoutMarginsGuide.LeadingAnchor).Active = true;
            view.TrailingAnchor.ConstraintEqualTo(view.Superview.LayoutMarginsGuide.TrailingAnchor).Active = true;
            view.TopAnchor.ConstraintEqualTo(view.Superview.LayoutMarginsGuide.TopAnchor).Active = true;
            view.BottomAnchor.ConstraintEqualTo(view.Superview.LayoutMarginsGuide.BottomAnchor).Active = true;
        }

        public static void BindEdgesToSuperviewEdges(this UIView view)
        {
            Guard.IsNotNull(view, nameof(view));
            Guard.IsNotNull(view.Superview, nameof(view.Superview));

            view.TranslatesAutoresizingMaskIntoConstraints = false;
            view.LeadingAnchor.ConstraintEqualTo(view.Superview.LeadingAnchor).Active = true;
            view.TrailingAnchor.ConstraintEqualTo(view.Superview.TrailingAnchor).Active = true;
            view.TopAnchor.ConstraintEqualTo(view.Superview.TopAnchor).Active = true;
            view.BottomAnchor.ConstraintEqualTo(view.Superview.BottomAnchor).Active = true;
        }
    }
}
