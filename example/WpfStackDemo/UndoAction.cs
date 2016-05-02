using System.Windows.Controls;
using System.Windows.Media;

namespace WpfStackDemo
{
    class UndoAction
    {
        public UndoAction(Button button)
        {
            _button = button;
            _brush = button.Background.CloneCurrentValue();
        }

        public void Execute()
        {
            _button.Background = _brush;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", _button.Content, _brush.ToString());
        }

        Button _button;
        Brush _brush;
    }
}
