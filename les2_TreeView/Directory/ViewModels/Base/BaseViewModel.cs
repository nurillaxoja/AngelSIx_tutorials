using PropertyChanged;
using System.ComponentModel;

namespace les2_TreeView.Directory.ViewModels.Base
{
    /// <summary>
    /// Base view model fires propery changed as needed
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// event fired on child property changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, equals) => { };
    }
}
