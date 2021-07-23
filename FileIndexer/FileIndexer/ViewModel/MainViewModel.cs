 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FileIndexer.ViewModel
{
    public class MainViewModel
    {
        public ICommand SuchenCommand { get; set; }

        public string DatenEingabe { get; set; }

        public string DatenAusgabe { get; set; }


        public MainViewModel()
        {
            SuchenCommand = new RelayCommand(SucheDaten);

        }
        

        public void SucheDaten()
        {

            DriveInfo[] allDrives = DriveInfo.GetDrives();

        }

        public List<string> AusgabeListe = new List<string>();
    }
}
