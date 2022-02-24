using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace PopStudio.MAUI
{
	public partial class Page_Package : ContentPage
	{
		public Page_Package()
		{
			InitializeComponent();
			CB_CMode.Items.Clear();
			CB_CMode.Items.Add("dz����׿����ݮ��");
			CB_CMode.Items.Add("rsb����׿��iOS��PS3��PS4��Xbox360��");
			CB_CMode.Items.Add("pak��Windows��MacOS��PS3��PSV��Xbox360��");
			CB_CMode.SelectedIndex = 0;
		}

		public void Do(object sender, EventArgs e)
        {
			Button b = (Button)sender;
			b.IsEnabled = false;
			bool mode = switchmode.IsToggled;
			string inFile = textbox1.Text;
			string outFile = textbox2.Text;
			int pmode = CB_CMode.SelectedIndex;
			bool c1 = switchchange1.IsToggled;
			bool c2 = switchchange2.IsToggled;
			new Thread(new ThreadStart(() =>
			{
				string err = null;
				try
				{
					if (mode == true)
					{
						if (!Directory.Exists(inFile))
						{
							throw new FileNotFoundException("�ļ���" + inFile + "�����ڣ�");
						}
						API.Pack(inFile, outFile, pmode);
					}
					else
					{
						if (!File.Exists(inFile))
						{
							throw new FileNotFoundException("�ļ�" + inFile + "�����ڣ�");
						}
						API.Unpack(inFile, outFile, pmode, c1, c2);
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

		public void ModeChange(object sender, ToggledEventArgs e)
        {
			if (((Switch)sender).IsToggled)
            {
				label1.Text = "����д��������ļ���·��";
				label2.Text = "����д��������ļ�·��";
				label3.Text = "��ѡ����ģʽ";
				change.IsVisible = false;
			}
            else
            {
				label1.Text = "����д��������ļ�·��";
				label2.Text = "����д��������ļ���·��";
				label3.Text = "��ѡ����ģʽ";
				change.IsVisible = true;
			}
			//�����ı�������
			string temp = textbox1.Text;
			textbox1.Text = textbox2.Text;
			textbox2.Text = temp;
		}
    }
}