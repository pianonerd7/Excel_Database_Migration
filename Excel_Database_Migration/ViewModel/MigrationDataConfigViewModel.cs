﻿using Excel_Database_Migration.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Excel_Database_Migration.ViewModel
{
    class MigrationDataConfigViewModel : ViewModelBase
    {

        #region Private Declarations

        private Page _mainWindow;
        private string _migrationFilePath;
        private string _attributeFilePath;
        private readonly ICommand _selectFilePathCommand;
        private readonly ICommand _selectAttributeFilePathCommand;
        private readonly ICommand _continueConfigCommand;

        #endregion

        #region Constructor

        public MigrationDataConfigViewModel(Page mainWindow)
        {
            _mainWindow = mainWindow;
            _selectFilePathCommand = new DelegateCommand(ExecuteSelectFilePathCommand, CanExecuteCommand);
            _selectAttributeFilePathCommand = new DelegateCommand(ExecuteSelectAttributeFilePathCommand, CanExecuteCommand);
            _continueConfigCommand = new DelegateCommand(ExecuteContinueConfigCommand, CanExecuteContinueConfigCommand);
        }

        #endregion 

        #region

        public string MigrationFilePath
        {
            get
            {
                return _migrationFilePath;
            }
            set
            {
                _migrationFilePath = value;
                OnPropertyChanged("MigrationFilePath");
            }
        }

        public string AttributeFilePath
        {
            get
            {
                return _attributeFilePath;
            }
            set
            {
                _attributeFilePath = value;
                OnPropertyChanged("AttributeFilePath");
            }
        }

        #endregion

        #region Public Commands

        public ICommand SelectFilePathCommand
        {
            get
            {
                return _selectFilePathCommand;
            }
        }

        public ICommand SelectAttributeFilePathCommands
        {
            get
            {
                return _selectAttributeFilePathCommand;
            }
        }

        public ICommand ContinueConfigCommand
        {
            get
            {
                return _continueConfigCommand;
            }
        }
        #endregion

        #region Private Methods

        private bool CanExecuteCommand(object obj)
        {
            return true;
        }

        private void ExecuteSelectFilePathCommand(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Excel Files|*.xls;*xlsx;*xlsm";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                MigrationFilePath = dialog.FileName;
            }
        }

        private void ExecuteSelectAttributeFilePathCommand(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "CSV Files|*.csv";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                AttributeFilePath = dialog.FileName;
            }
        }


        private bool CanExecuteContinueConfigCommand(object obj)
        {
            //return _migrationFilePath == null;
            //TODO
            return true;
        }

        private void ExecuteContinueConfigCommand(object obj)
        {

            SQLGeneration.SQLGenerator.generate(MigrationFilePath);
            _mainWindow.Content = new ConfirmationControl();
        }
        #endregion

    }
}
