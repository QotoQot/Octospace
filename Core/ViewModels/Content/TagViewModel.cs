using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmCross.ViewModels;

namespace Core.ViewModels.Content
{
    public class TagViewModel : MvxViewModel
    {
        public TagViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; }

        List<TagViewModel> _children = new();
        public ReadOnlyCollection<TagViewModel> Children => _children.AsReadOnly();

        public void SetupChildren(List<TagViewModel> children)
        {
            _children.Clear();
            _children.AddRange(children);
        }
    }
}