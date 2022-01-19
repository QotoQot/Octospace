using Core.Model.State;
using System;

namespace Core.ViewModels.Content.Documents
{
    public interface IBlockViewModel
    {
        IState State { get; }
        bool IsSelected { get; set; }
    }
}
