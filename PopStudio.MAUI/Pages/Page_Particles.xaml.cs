using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace PopStudio.MAUI
{
	public partial class Page_Particles : ContentPage
	{
		public Page_Particles()
		{
			InitializeComponent();
            CB_CMode.Items.Clear();
            CB_CMode.Items.Add("Windows,MacOS");
            CB_CMode.Items.Add("Android,iOS,Bada,BlackBerry");
            CB_CMode.Items.Add("Android,iOS");
            CB_CMode.Items.Add("WindowsPhone");
            CB_CMode.Items.Add("PS3,PSV,Xbox360");
            CB_CMode.Items.Add("AndroidTV");
            CB_CMode.SelectedIndex = 0;
        }

        private void ToggleButton_Checked(object sender, EventArgs e)
        {
            if (((Switch)sender).IsToggled)
            {
                text1.Text = "����д��������ļ�·��";
                text2.Text = "����д���������ļ����·��";
                text3.Text = "��ѡ�����ģʽ";
                string temp = textbox1.Text;
                textbox1.Text = textbox2.Text;
                textbox2.Text = temp;
            }
            else
            {
                text1.Text = "����д��������ļ�·��";
                text2.Text = "����д���������ļ����·��";
                text3.Text = "��ѡ�����ģʽ";
                string temp = textbox1.Text;
                textbox1.Text = textbox2.Text;
                textbox2.Text = temp;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.IsEnabled = false;
            text4.Text = "ִ����......";
            bool mode = TB_Mode.IsToggled;
            string inFile = textbox1.Text;
            string outFile = textbox2.Text;
            int cmode = CB_CMode.SelectedIndex;
            new Thread(new ThreadStart(() =>
            {
                string err = null;
                try
                {
                    if (!File.Exists(inFile))
                    {
                        throw new FileNotFoundException("�ļ�" + inFile + "�����ڣ�");
                    }
                    if (mode)
                    {
                        API.EncodeParticles(inFile, outFile, cmode);
                    }
                    else
                    {
                        API.DecodeParticles(inFile, outFile, cmode);
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (err == null)
                    {
                        text4.Text = "ִ�����";
                    }
                    else
                    {
                        text4.Text = "ִ���쳣��" + err;
                    }
                    b.IsEnabled = true;
                });
            }))
            { IsBackground = true }.Start();
        }

		public async Task<string> ChooseOpenFile()
		{
#if ANDROID
			string file = "/sdcard";
			string createnew = "�½��ļ���\0";
			string back = "�����ϼ�Ŀ¼\0";
			string ok = "ȷ��\0";
			string choosenow = "����\0";
			string cancel = "ȡ��\0";
			while (true)
			{
				string[] rawary = Directory.GetDirectories(file);
				string[] rawary2 = Directory.GetFiles(file);
				Array.Sort(rawary);
				Array.Sort(rawary2);
				string[] showary;
				int ary1 = rawary.Length;
				int ary2 = rawary2.Length;
				if (file.Length <= 7)
				{
					showary = new string[ary1 + ary2 + 1];
					showary[0] = createnew;
					for (int i = 0; i < ary1; i++)
					{
						showary[i + 1] = Path.GetFileName(rawary[i]);
					}
					for (int i = 0; i < ary2; i++)
					{
						showary[i + ary1 + 1] = Path.GetFileName(rawary2[i]);
					}
				}
				else
				{
					showary = new string[ary1 + rawary2.Length + 2];
					showary[0] = createnew;
					showary[1] = back;
					for (int i = 0; i < ary1; i++)
					{
						showary[i + 2] = Path.GetFileName(rawary[i]);
					}
					for (int i = 0; i < ary2; i++)
					{
						showary[i + ary1 + 2] = Path.GetFileName(rawary2[i]);
					}
				}
				string ans = await DisplayActionSheet(file + Const.PATHSEPARATOR, choosenow, cancel, showary);
				if (string.IsNullOrEmpty(ans) || ans == cancel || ans == choosenow)
				{
					return null;
				}
				else if (ans == createnew)
				{
					string newname = await DisplayPromptAsync("�½��ļ���", "�������ļ�����", ok, cancel, initialValue: "�½��ļ���");
					if (!string.IsNullOrEmpty(newname))
					{
						try
						{
							Directory.CreateDirectory(file + Const.PATHSEPARATOR + newname);
						}
						catch (Exception)
						{
							await DisplayAlert("��������", "�½��ļ���ʧ��", ok, cancel);
						}
					}
				}
				else if (ans == back)
				{
					if (file.Length > 7) file = Path.GetDirectoryName(file);
				}
				else
				{
					file += Const.PATHSEPARATOR + ans;
					if (File.Exists(file))
					{
						return file;
					}
				}
			}
#elif WINDOWS
			return await new PopStudio.MAUI.Platforms.Windows.OpenFilePicker().PickFile();
#else
return null;
#endif
		}

		public async Task<string> ChooseSaveFile()
		{
#if ANDROID
			string file = "/sdcard";
			string createnew = "�½��ļ���\0";
			string back = "�����ϼ�Ŀ¼\0";
			string ok = "ȷ��\0";
			string choosenow = "���浽��Ŀ¼\0";
			string cancel = "ȡ��\0";
			while (true)
			{
				string[] rawary = Directory.GetDirectories(file);
				string[] rawary2 = Directory.GetFiles(file);
				Array.Sort(rawary);
				Array.Sort(rawary2);
				string[] showary;
				int ary1 = rawary.Length;
				int ary2 = rawary2.Length;
				if (file.Length <= 7)
				{
					showary = new string[ary1 + ary2 + 1];
					showary[0] = createnew;
					for (int i = 0; i < ary1; i++)
					{
						showary[i + 1] = Path.GetFileName(rawary[i]);
					}
					for (int i = 0; i < ary2; i++)
					{
						showary[i + ary1 + 1] = Path.GetFileName(rawary2[i]);
					}
				}
				else
				{
					showary = new string[ary1 + rawary2.Length + 2];
					showary[0] = createnew;
					showary[1] = back;
					for (int i = 0; i < ary1; i++)
					{
						showary[i + 2] = Path.GetFileName(rawary[i]);
					}
					for (int i = 0; i < ary2; i++)
					{
						showary[i + ary1 + 2] = Path.GetFileName(rawary2[i]);
					}
				}
				string ans = await DisplayActionSheet(file + Const.PATHSEPARATOR, choosenow, cancel, showary);
				if (string.IsNullOrEmpty(ans) || ans == cancel)
				{
					return null;
				}
				else if (ans == choosenow)
				{
					string val = await DisplayPromptAsync("�����ļ�", "�������ļ���", "ȷ��", "ȡ��");
					if (string.IsNullOrEmpty(val)) return null;
					return file + Const.PATHSEPARATOR + val;
				}
				else if (ans == createnew)
				{
					string newname = await DisplayPromptAsync("�½��ļ���", "�������ļ�����", ok, cancel, initialValue: "�½��ļ���");
					if (!string.IsNullOrEmpty(newname))
					{
						try
						{
							Directory.CreateDirectory(file + Const.PATHSEPARATOR + newname);
						}
						catch (Exception)
						{
							await DisplayAlert("��������", "�½��ļ���ʧ��", ok, cancel);
						}
					}
				}
				else if (ans == back)
				{
					if (file.Length > 7) file = Path.GetDirectoryName(file);
				}
				else
				{
					file += Const.PATHSEPARATOR + ans;
					if (File.Exists(file))
					{
						return file;
					}
				}
			}
#elif WINDOWS
			return await new PopStudio.MAUI.Platforms.Windows.SaveFilePicker().PickFile();
#else
return null;
#endif
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			try
			{
				string val = await ChooseOpenFile();
				if (!string.IsNullOrEmpty(val)) textbox1.Text = val;
			}
			catch (Exception)
			{
			}
		}

		private async void Button2_Clicked(object sender, EventArgs e)
		{
			try
			{
				string val = await ChooseSaveFile();
				if (!string.IsNullOrEmpty(val)) textbox2.Text = val;
			}
			catch (Exception)
			{
			}
		}
	}
}