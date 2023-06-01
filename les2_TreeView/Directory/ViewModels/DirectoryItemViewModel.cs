using les2_TreeView.Directory.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace les2_TreeView.Directory.ViewModels
{
    /// <summary>
    /// View model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        public DirectoryItemType Type { get; set; }
        public string FullPath { get; set; }
        public string Name { get { return Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileFolderName(FullPath); } }

        /// <summary>
        /// List of all children in this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates this can be expanded or not
        /// </summary>
        public bool CanExpand { get { return Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if the current item is expanded
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return Children?.Count(f => f != null) > 0;
            }
            set
            {
                //if ui tells to expand
                if (value == true)
                    Expand();
                else
                    ClearChildren();

            }
        }

        #endregion

        #region Public Commands
        /// <summary>
        /// command to expand item 
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            //Create a commands 
            ExpandCommand = new RelayCommand(Expand);

            //set path and type
            FullPath = fullPath;
            Type = type;

            //set up the children as needed
            this.ClearChildren();
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Removes all childrend and ads dummy items
        /// </summary>
        private void ClearChildren()
        {
            Children = new ObservableCollection<DirectoryItemViewModel>();
            //Show expand if not file
            if (Type != DirectoryItemType.File)
                Children.Add(null);

        }

        #endregion

        /// <summary>
        /// Expand this directory and finds all children
        /// </summary>
        private void Expand()
        {
            if (Type == DirectoryItemType.File)
                return;

            // find all children 
            Children = new ObservableCollection<DirectoryItemViewModel>(
                DirectoryStructure.GetDirectoryContents(FullPath).
                Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }

    }
}
