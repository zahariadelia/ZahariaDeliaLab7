using Microsoft.Maui.Controls;

namespace ZahariaDeliaLab7
{
    public class ValidationBehaviour : Behavior<Editor>
    {
        protected override void OnAttachedTo(Editor bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEditorTextChanged;
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEditorTextChanged;
        }

        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            var editor = sender as Editor;
            if (editor == null)
                return;

            // Example validation: Description must not be empty
            editor.BackgroundColor = string.IsNullOrWhiteSpace(editor.Text)
                ? Colors.LightPink
                : Colors.White;
        }
    }
}