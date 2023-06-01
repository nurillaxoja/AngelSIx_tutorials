using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls.Primitives;

namespace les2_TreeView.Directory.ViewModels
{
    /// <summary>
    /// View model for applications main directory view
    /// </summary>
    public class DirectoryStructureViewModel
    {
        #region Public Properties 
        /// <summary>
        /// List of All on comp.
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        #endregion

        #region Constructor
        public DirectoryStructureViewModel()
        {
            this.Items = new ObservableCollection<DirectoryItemViewModel>(DirectoryStructure.GetLogicalDrives().
                Select(drive => new DirectoryItemViewModel(drive.FullPath , DirectoryItemType.Drive)));
        }
        #endregion

    }
}
