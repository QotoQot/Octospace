using System;
using MvvmCross.Presenters.Attributes;

namespace Mac.MvvmCross.Presenters.Attributes
{
    public class WindowPresentationAttribute : MvxBasePresentationAttribute
    {
        public WindowPresentationAttribute(string controllerStoryboardId)
        {
            ControllerStoryboardId = controllerStoryboardId;
        }

        public string ControllerStoryboardId { get; protected set; }
        public string? WindowIdentifier { get; set; }

        //public bool MovableByBackground { get; set; } = true;
        public bool ShouldCascadeWindows { get; set; } = true;
    }
}
