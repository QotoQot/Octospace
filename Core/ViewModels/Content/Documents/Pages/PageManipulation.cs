using MvvmCross.Plugin.Messenger;
using System;
namespace Core.ViewModels.Content.Documents
{
    public enum PageManipulation
    {
        Error,
        DeselectAll,
        GoToBlockAbove,
        GoToBlockBelow,
    }

    public class PageManipulationRequestMessage : MvxMessage
    {
        public PageManipulationRequestMessage(object sender, PageManipulation pageManipulation) : base(sender)
        {
            PageManipulation = pageManipulation;
        }

        public PageManipulation PageManipulation { get; }
    }
}
