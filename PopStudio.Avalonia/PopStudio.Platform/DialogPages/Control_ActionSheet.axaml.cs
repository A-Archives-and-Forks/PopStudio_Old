using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PopStudio.Platform
{
    public partial class Control_ActionSheet : UserControl
    {
        public void SetInfo(string title, string cancel, string ok, params string[] items)
        {
            int l = items.Length;
            this.title.Text = title;
            this.items = items;
            okstring = ok;
            cancelstring = cancel;
            this.ok.Content = ok.Replace("\0", string.Empty);
            this.cancel.Content = cancel.Replace("\0", string.Empty);
            list.Items.Clear();
            for (int i = 0; i < l; i++)
            {
                list.Items.Add(items[i]);
            }
            list.SelectedIndex = 0;
        }

        public Control_ActionSheet()
        {
            InitializeComponent();
            LoadControl();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void LoadControl()
        {
            title = this.Get<TextBlock>("title");
            list = this.Get<ListBox>("list");
            ok = this.Get<Button>("ok");
            cancel = this.Get<Button>("cancel");
        }

        public string result;
        string[] items;
        string cancelstring;
        string okstring;
        public event Action Close;
        public string Result => result;

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex < 0 || items.Length <= 0)
            {
                result = okstring;
            }
            else
            {
                result = items[list.SelectedIndex];
            }
            Close?.Invoke();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            result = cancelstring;
            Close?.Invoke();
        }
    }
}
