using System;
using System.Net.Http.Headers;
using System.Windows.Input;
using System.Windows.Navigation;

namespace les2_TreeView.Directory.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        #region Private Members
        private Action mAction;
        #endregion

        #region Public events
        /// <summary>
        /// event fire when <see cref="CanExecute(object?)"/> method value has changed
        /// </summary>
        public event EventHandler? CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action action)
        {
            mAction = action;
        }
        #endregion

        #region Command methods

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            mAction();
        }

        #endregion
    }
}
