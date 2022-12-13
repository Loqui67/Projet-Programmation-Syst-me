using IHM_Client.ViewModels;
using IHM_Client.Command;
using IHM_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IHM_Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public static bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
                OnPropertyChanged(nameof(IsNotConnected));
            }
        }
        public bool IsNotConnected => !IsConnected;

        private bool _isPaused;
        public bool IsPaused
        {
            get { return _isPaused; }
            set
            {
                _isPaused = value;
                OnPropertyChanged(nameof(IsPaused));
                OnPropertyChanged(nameof(IsNotPaused));
                OnPropertyChanged(nameof(IsSavingAndNotPausing));
                OnPropertyChanged(nameof(IsSavingAndPausing));
            }
        }

        private bool _isSaving;

        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                _isSaving = value;
                OnPropertyChanged(nameof(IsSaving));
                OnPropertyChanged(nameof(IsSavingAndNotPausing));
                OnPropertyChanged(nameof(IsSavingAndPausing));
            }
        }

        private float _progress;
        public float Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private string _fileNumber;
        public string FileNumber
        {
            get { return _fileNumber; }
            set
            {
                _fileNumber = value;
                OnPropertyChanged(nameof(FileNumber));
            }
        }

        public bool IsSavingAndNotPausing => IsSaving && !IsPaused;
        public bool IsSavingAndPausing => IsSaving && IsPaused;

        public bool IsNotPaused => !IsPaused;

        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ResumeCommand { get; }

        public ICommand StopCommand { get; }

        static Thread t; 
        public MainViewModel()
        {
            t = new(() =>
            {
                while (true)
                {
                    if (ServerCommunication.isConnect)
                    {
                        ObjectReceived? receive = ServerCommunication.Receive();
                        if (receive != null)
                        {
                            switch (receive.State)
                            {
                                case "save":
                                    IsSaving = true;
                                    IsPaused = false;
                                    FileNumber = receive.FileNumber ?? "";
                                    Progress = receive.Progress ?? 0;
                                    break;

                                case "pause":
                                    IsPaused = true;
                                    break;

                                case "resume":
                                    IsPaused = false;
                                    break;

                                case "stop":
                                case "saveDone":
                                    IsSaving = false;
                                    IsPaused = false;
                                    FileNumber = "";
                                    Progress = 0;
                                    break;

                                case "progress":
                                    Progress = receive.Progress ?? 0;
                                    FileNumber = receive.FileNumber ?? "";
                                    break;

                                default:
                                    break;
                            }
                        }
                    } else Thread.Sleep(1000);
                } 
            });
            t.Start();
            ConnectCommand = new ConnectCommand(this);
            DisconnectCommand = new DisconnectCommand(this);
            PauseCommand = new PauseCommand();
            ResumeCommand = new ResumeCommand();
            StopCommand = new StopCommand();
        }
    }
}
