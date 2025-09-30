using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using PopStudio.Language.Languages;
using System.Diagnostics;
using PopStudio.Platform;

namespace PopStudio.Avalonia.Pages
{
    public partial class Control_Compress : UserControl
    {
        public Control_Compress()
        {
            InitializeComponent();
            LoadControl();
            LoadFont();
            CB_CMode.Items.Add("Zlib");
            CB_CMode.Items.Add("Gzip");
            CB_CMode.Items.Add("Deflate");
            CB_CMode.Items.Add("Brotli");
            CB_CMode.Items.Add("Lzma");
            CB_CMode.Items.Add("Lz4");
            CB_CMode.Items.Add("Bzip2");
            CB_CMode.SelectedIndex = 0;
            MAUIStr.OnLanguageChanged += LoadFont;
        }

        ~Control_Compress()
        {
            MAUIStr.OnLanguageChanged -= LoadFont;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        void LoadControl()
        {
            label_batch1 = this.Get<TextBlock>("label_batch1");
            label_batch2 = this.Get<TextBlock>("label_batch2");
            batch_mode = this.Get<ToggleSwitch>("batch_mode");
            label_introduction = this.Get<TextBlock>("label_introduction");
            label_choosemode = this.Get<TextBlock>("label_choosemode");
            label_mode1 = this.Get<TextBlock>("label_mode1");
            label_mode2 = this.Get<TextBlock>("label_mode2");
            text1 = this.Get<TextBlock>("text1");
            text2 = this.Get<TextBlock>("text2");
            text3 = this.Get<TextBlock>("text3");
            button1 = this.Get<Button>("button1");
            button2 = this.Get<Button>("button2");
            button_run = this.Get<Button>("button_run");
            label_statue = this.Get<TextBlock>("label_statue");
            text4 = this.Get<TextBlock>("text4");
            textbox1 = this.Get<TextBox>("textbox1");
            textbox2 = this.Get<TextBox>("textbox2");
            TB_Mode = this.Get<ToggleSwitch>("TB_Mode");
            CB_CMode = this.Get<ComboBox>("CB_CMode");
        }

        void LoadFont()
        {
            label_batch1.Text = MAUIStr.Obj.Share_SingleMode;
            label_batch2.Text = MAUIStr.Obj.Share_BatchMode;
            label_introduction.Text = MAUIStr.Obj.Compress_Introduction;
            label_choosemode.Text = MAUIStr.Obj.Share_ChooseMode;
            if (batch_mode.IsChecked == true)
            {
                label_mode1.Text = MAUIStr.Obj.Compress_Mode1_Batch;
                label_mode2.Text = MAUIStr.Obj.Compress_Mode2_Batch;
            }
            else
            {
                label_mode1.Text = MAUIStr.Obj.Compress_Mode1;
                label_mode2.Text = MAUIStr.Obj.Compress_Mode2;
            }
            LoadFont_Checked(TB_Mode.IsChecked == true);
            button1.Content = MAUIStr.Obj.Share_Choose;
            button2.Content = MAUIStr.Obj.Share_Choose;
            button_run.Content = MAUIStr.Obj.Share_Run;
            label_statue.Text = MAUIStr.Obj.Share_RunStatue;
            text4.Text = MAUIStr.Obj.Share_Waiting;
        }

        void LoadFont_Checked(bool v)
        {
            if (batch_mode.IsChecked == true)
            {
                if (v)
                {
                    text1.Text = MAUIStr.Obj.Compress_Choose4_Batch;
                    text2.Text = MAUIStr.Obj.Compress_Choose5_Batch;
                    text3.Text = MAUIStr.Obj.Compress_Choose6_Batch;
                }
                else
                {
                    text1.Text = MAUIStr.Obj.Compress_Choose1_Batch;
                    text2.Text = MAUIStr.Obj.Compress_Choose2_Batch;
                    text3.Text = MAUIStr.Obj.Compress_Choose3_Batch;
                }
            }
            else
            {
                if (v)
                {
                    text1.Text = MAUIStr.Obj.Compress_Choose4;
                    text2.Text = MAUIStr.Obj.Compress_Choose5;
                    text3.Text = MAUIStr.Obj.Compress_Choose6;
                }
                else
                {
                    text1.Text = MAUIStr.Obj.Compress_Choose1;
                    text2.Text = MAUIStr.Obj.Compress_Choose2;
                    text3.Text = MAUIStr.Obj.Compress_Choose3;
                }
            }
        }

        private void Switch_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch s)
            {
                LoadFont_Checked(s.IsChecked == true);
                (textbox1.Text, textbox2.Text) = (textbox2.Text, textbox1.Text);
            }
        }

        private void Switch_Batch_Checked(object sender, RoutedEventArgs e)
        {
            LoadFont();
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string val = batch_mode.IsChecked == false ? (await new OpenFileDialog { AllowMultiple = false }.ShowAsync(MainWindow.Singleten))?[0] : (await new OpenFolderDialog().ShowAsync(MainWindow.Singleten));
                if (!string.IsNullOrEmpty(val)) textbox1.Text = val;
            }
            catch (Exception)
            {
            }
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string val = batch_mode.IsChecked == false ? (await new SaveFileDialog().ShowAsync(MainWindow.Singleten)) : (await new OpenFolderDialog().ShowAsync(MainWindow.Singleten));
                if (!string.IsNullOrEmpty(val)) textbox2.Text = val;
            }
            catch (Exception)
            {
            }
        }

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            b.IsEnabled = false;
            text4.Text = MAUIStr.Obj.Share_Running;
            bool mode = TB_Mode.IsChecked == true;
            bool batchmode = batch_mode.IsChecked == true;
            string inFile = textbox1.Text;
            string outFile = textbox2.Text;
            int cmode = CB_CMode.SelectedIndex;
            new Thread(new ThreadStart(() =>
            {
                string err = null;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                try
                {
                    if (batchmode)
                    {
                        if (!Directory.Exists(inFile))
                        {
                            throw new Exception(string.Format(MAUIStr.Obj.Share_FolderNotFound, inFile));
                        }
                        inFile = YFAPI.FormatPath(inFile);
                        int length = inFile.Length;
                        string[] files = YFAPI.GetFiles(inFile);
                        YFAPI.NewDir(outFile);
                        foreach (string mfile in files)
                        {
                            string newPath = YFAPI.FormatPath(outFile + mfile[length..] + ".out");
                            YFAPI.NewDir(newPath, false);
                            try
                            {
                                if (mode)
                                {
                                    YFAPI.Compress(mfile, newPath, cmode);
                                }
                                else
                                {
                                    YFAPI.Decompress(mfile, newPath, cmode);
                                }
                            }
                            catch (Exception)
                            {
                                File.Delete(newPath);
                            }
                        }
                    }
                    else
                    {
                        if (!File.Exists(inFile))
                        {
                            throw new Exception(string.Format(MAUIStr.Obj.Share_FileNotFound, inFile));
                        }
                        if (Directory.Exists(outFile))
                        {
                            outFile += "/" + Path.GetFileName(inFile) + ".out";
                            outFile = YFAPI.FormatPath(outFile);
                        }
                        YFAPI.NewDir(outFile, false);
                        if (mode)
                        {
                            YFAPI.Compress(inFile, outFile, cmode);
                        }
                        else
                        {
                            YFAPI.Decompress(inFile, outFile, cmode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
                sw.Stop();
                decimal time = sw.ElapsedMilliseconds / 1000m;
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (err == null)
                    {
                        text4.Text = string.Format(MAUIStr.Obj.Share_Finish, time);
                    }
                    else
                    {
                        text4.Text = string.Format(MAUIStr.Obj.Share_Wrong, err);
                    }
                    b.IsEnabled = true;
                });
            }))
            { IsBackground = true }.Start();
        }
    }
}
