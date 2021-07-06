using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FileIndexer
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _textAusgabe;

        public string TextAusgabe
        {
            get 
            {
                return _textAusgabe; 
            }
            set 
            { 
                _textAusgabe = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(TextAusgabe)));
            }
        }

        public ICommand ScanCommand { get; set; } 


        private void InitCommands()
        {
            ScanCommand = new RelayCommand(Print);
        }


        public MainViewModel()
        {
            InitCommands();
        }

        public List<string> Pfade { get; set; } = new List<string>();

        private void Print()
        {
            MessageBox.Show("Klappt");
            //File.WriteAllText("c:", TextAusgabe);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
