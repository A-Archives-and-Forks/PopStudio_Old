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
            CB_CMode.Items.Add("PC");
            CB_CMode.Items.Add("Phone32λ");
            CB_CMode.Items.Add("��׿64λ");
            CB_CMode.Items.Add("iOS64λ");
            CB_CMode.Items.Add("WindowsPhone");
            CB_CMode.Items.Add("TV");
            CB_CMode.Items.Add("TV�ϹŰ汾");
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
            bool? mode = TB_Mode.IsToggled;
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
                    if (mode == true)
                    {
                        //Class.API.ParticlesCompiled(inFile, outFile, cmode);
                    }
                    else
                    {
                        //Class.API.ParticlesDecompiled(inFile, outFile, cmode);
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
    }
}