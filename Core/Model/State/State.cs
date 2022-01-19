using System;
namespace Core.Model.State
{
    public interface IState
    {
        TextSettings Text { get; }
    }

    public class State : IState
    {
        public State()
        {
            Text = new();
        }

        public TextSettings Text { get; }
    }
}
