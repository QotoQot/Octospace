using System;
using MvvmCross.Presenters.Attributes;

namespace Mac.MvvmCross.Presenters.Attributes
{
    public class UniqueWindowPresentationAttribute : WindowPresentationAttribute
    {
        public UniqueWindowPresentationAttribute(string controllerStoryboardId) : base(controllerStoryboardId)
        {
            ControllerStoryboardId = controllerStoryboardId;
        }
    }
}
