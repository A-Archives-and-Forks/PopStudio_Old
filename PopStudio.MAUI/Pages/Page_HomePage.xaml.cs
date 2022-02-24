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
            label_ver.Text = "�汾��";// + Class.Const.APPVERSION;
            label_notice.Text = "";// Class.Const.APPNOTICE;
#if ANDROID //��׿Ȩ�޼�飬Ҫ�������ļ�����Ȩ�޼���
            CheckAndRequestPermissionAsync();
		}

        public async void CheckAndRequestPermissionAsync()
        {
            ReadWriteStoragePermission a = new();
            var status = await a.CheckStatusAsync();
            bool HavePermission = true;
            if (status != PermissionStatus.Granted)
            {
                if (await DisplayAlert("Ȩ������", "��Android6������ϵͳ�汾�У����������洢Ȩ�ޣ����������Ȩ��д�ļ���", "ǰ����Ȩ", "ȡ��"))
                {
                    HavePermission = (await a.RequestAsync()) == PermissionStatus.Granted;
                }
                else
                {
                    HavePermission = false;
                }
            }
            if (!HavePermission) return;
            try
            {
                File.Create("/sdcard/REo1cUFQKTE220kiFEmtjh7U1Lr3oS8S");
                File.Delete("/sdcard/REo1cUFQKTE220kiFEmtjh7U1Lr3oS8S");
            }
            catch (Exception)
            {
                //�����ˣ������������Ҫһ��Ҫ��ס���ҿɲ����ٷѾ�����Сʱд�ⶫ��
                if (await DisplayAlert("Ȩ������", "��Android11������ϵͳ�汾�У���������������ļ�����Ȩ�ޣ��������ֻ�ܶ�д�����ڲ��ļ����е��ļ���", "ǰ����Ȩ", "ȡ��"))
                {
                    var bb = new Android.Content.Intent(Android.Provider.Settings.ActionManageAllFilesAccessPermission);
                    bb.SetFlags(Android.Content.ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(bb);
                }
            }
        }

        public class ReadWriteStoragePermission : Permissions.BasePlatformPermission
        {
            public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new (string androidPermission, bool isRuntime)[2]
            {
                (Android.Manifest.Permission.ReadExternalStorage, true),
                (Android.Manifest.Permission.WriteExternalStorage, true)
            };
#endif
        }
    }
}