using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ActivityButton.Controls
{
    public partial class ActivityButton : ContentView
    {
        private string buttonText;

        public event EventHandler Clicked = delegate { };

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ActivityButton), propertyChanged: TextUpdated);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ActivityButton), propertyChanged: CommandUpdated);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ActivityButton), propertyChanged: CommandParameterUpdated);
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(ActivityButton), propertyChanged: IsBusyUpdated);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(ActivityButton), propertyChanged: CornerRadiusUpdated);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ActivityButton));

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ActivityButton));

        public ActivityButton()
        {
            this.InitializeComponent();
            this.InnerButton.BindingContext = this;
            this.InnerButton.Clicked += this.OnClicked;
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public bool IsBusy
        {
            get => (bool)this.GetValue(IsBusyProperty);
            set => this.SetValue(IsBusyProperty, value);
        }

        public int CornerRadius
        {
            get => (int)this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        private void OnClicked(object sender, EventArgs args)
        {
            this.Clicked?.Invoke(sender, args);
        }

        private static void TextUpdated(object sender, object oldValue, object newValue)
        {
            if (sender is ActivityButton activityButton && newValue is string newString)
            {
                activityButton.buttonText = newString;
                activityButton.InnerButton.Text = newString;
            }
        }

        private static void CommandUpdated(object sender, object oldValue, object newValue)
        {
            if (sender is ActivityButton activityButton && newValue is ICommand newCommand)
            {
                activityButton.InnerButton.Command = newCommand;
            }
        }

        private static void CommandParameterUpdated(object sender, object oldValue, object newValue)
        {
            if (sender is ActivityButton activityButton && newValue != null)
            {
                activityButton.InnerButton.CommandParameter = newValue;
            }
        }

        private static async void IsBusyUpdated(object sender, object oldValue, object newValue)
        {
            if (sender is ActivityButton activityButton && newValue is bool newBool)
            {
                activityButton.InnerButton.IsEnabled = !newBool;
                activityButton.InnerActivityIndicator.IsRunning = newBool;
                activityButton.InnerActivityIndicator.IsEnabled = newBool;
                activityButton.InnerActivityIndicator.IsVisible = newBool;

                var opacity = newBool ? 1 : 0;
                await activityButton.InnerActivityIndicator.FadeTo(opacity, 200);

                activityButton.InnerButton.Text = newBool ? string.Empty : activityButton.buttonText;
            }
        }

        private static void CornerRadiusUpdated(object sender, object oldValue, object newValue)
        {
            if (sender is ActivityButton activityButton && newValue is int newInt)
            {
                activityButton.InnerButton.CornerRadius = newInt;
            }
        }
    }
}
