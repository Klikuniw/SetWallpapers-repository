using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SetWallpapers.Infrastructure;
using SetWallpapers.Model;

namespace SetWallpapers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnExit(object sender, ExitEventArgs e)
        {
            XmlFileService service = new XmlFileService();
            service.WriteClosingTime("settings.xml",DateTime.Now);
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            XmlFileService service = new XmlFileService();

            if (service.ReadTimerStarted("settings.xml"))
            {
                //зчитую час який звлишився до зміни картинки
                TimeSpan remains = DateTime.Now.Subtract(service.ReadClosingTime("settings.xml"));

                //перевірка чи пройшов час до зміни картинки
                if (remains.TotalSeconds - service.ReadRemainsIntervalTime("settings.xml").TotalSeconds >= 0)
                {
                    //змінюю картинку
                    WallpaperCraftParser parser = new WallpaperCraftParser();
                    Wallpaper.Set(new Uri(parser.ParseImage(service.ReadCategories("wallpaperscraftInfo.xml")[0], service.ReadSelectedResolution("settings.xml"))), Wallpaper.Style.Centered);

                    //поновлюю час який залишвся на початковий
                    service.WriteRemainsIntervalTime("settings.xml", ConverterTime.ToTimeSpan(service.ReadInterval("settings.xml")));
                }
                else
                {
                    //поновлюю час який залишвся
                    service.WriteRemainsIntervalTime("settings.xml", new TimeSpan(0, 0, Math.Abs((int)(remains.TotalSeconds - service.ReadRemainsIntervalTime("settings.xml").TotalSeconds))));
                }
            }


        }
    }
}
