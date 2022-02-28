using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace PopStudio.MAUI
{
	public partial class Page_HomePage : ContentPage
	{
		public Page_HomePage()
		{
			InitializeComponent();
            label_ver.Text = "�汾��3.2";
            label_notice.Text = "1.�������ù��ܣ�\n2.�����˸��������ļ�ѡȡ����";
            this.CheckAndRequestPermissionAsync();
			string settingxml = Permission.GetSettingPath();
			if (!File.Exists(settingxml))
            {
				Dir.NewDir(settingxml, false);
				Setting.SaveAsXml(settingxml);
            }
			Setting.LoadFromXml(settingxml);
		}
    }
}