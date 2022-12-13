using EasySave.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.developpement.Commands
{
    public class BrowsePathCommand : CommandBase
    {
        private readonly BackupJobDetailsFormViewModel _backupJobDetailsFormViewModel;
        private readonly bool _isSourcePath;

        public BrowsePathCommand(BackupJobDetailsFormViewModel backupJobDetailsFormViewModel, bool isSourcePath)
        {
            _backupJobDetailsFormViewModel = backupJobDetailsFormViewModel;
            _isSourcePath = isSourcePath;
        }

        public override void Execute(object parameter)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            openFileDlg.ShowDialog();
            if (string.IsNullOrEmpty(openFileDlg.SelectedPath)) return;
            if (_isSourcePath)
            {
                _backupJobDetailsFormViewModel.SourcePath = openFileDlg.SelectedPath;
            }
            else
            {
                _backupJobDetailsFormViewModel.DestinationPath = openFileDlg.SelectedPath;
            }
        }
    }
}

