using Core.Logging;
using Core.ViewModels.MainWindow;
using Foundation;
using iOS.Platform;
using MvvmCross.Platforms.Ios.Views;
using System;
using System.ComponentModel;
using System.Linq;
using UIKit;

namespace iOS.Views.MainWindow
{
    public partial class SidebarView : MvxCollectionViewController<SidebarViewModel>
    {
        [Export("initWithCoder:")]
        public SidebarView(NSCoder coder) : base(coder) => Initialize();
        public SidebarView(IntPtr handle) : base(handle) => Initialize();
        public SidebarView() : base("SidebarView", NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        enum Sections
        {
            Error,
            Recents,
            Tags
        }

        UICollectionViewDiffableDataSource<NSWrapper<Sections>, NSWrapper<SidebarOutlineItemViewModel>> _dataSource = null!;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CollectionView.CollectionViewLayout = new UICollectionViewCompositionalLayout((section, layoutEnvironment) =>
            {
                // TODO: maybe switch to these for iPad full screen BUT not split screen
                var configuration = new UICollectionLayoutListConfiguration(UICollectionLayoutListAppearance.Sidebar);
                //var configuration = new UICollectionLayoutListConfiguration(UICollectionLayoutListAppearance.SidebarPlain);
                return NSCollectionLayoutSection.GetSection(configuration, layoutEnvironment);
            });

            _dataSource = new(CollectionView, (collectionView, indexPath, item) => {
                var registration = UICollectionViewCellRegistration.GetRegistration(
                    typeof(UICollectionViewListCell),
                    (cell, indexPath, wrappedItem) =>
                    {
                        var listCell = (UICollectionViewListCell)cell;
                        var sidebarItem = ((NSWrapper<SidebarOutlineItemViewModel>)wrappedItem).Item;

                        var configuration = listCell.DefaultContentConfiguration;
                        configuration.Text = sidebarItem.Title;
                        listCell.ContentConfiguration = configuration;

                        // TODO: split into multiple methods for different types
                        if (sidebarItem.HasChildren)
                        {
                            var disclosureOptions = new UICellAccessoryOutlineDisclosure();
                            disclosureOptions.Style = UICellAccessoryOutlineDisclosureStyle.Header;
                            listCell.Accessories = new UICellAccessory[] { disclosureOptions };
                        }
                    }
                );

                // TODO: choose between registrations for different cells
                return collectionView.DequeueConfiguredReusableCell(registration, indexPath, item);
            });

            var recentsSection = new NSWrapper<Sections>(Sections.Recents);
            var tagsSection = new NSWrapper<Sections>(Sections.Tags);
            var sections = new NSWrapper<Sections>[] { recentsSection, tagsSection };

            var rootSnapshot = new NSDiffableDataSourceSnapshot<NSWrapper<Sections>, NSWrapper<SidebarOutlineItemViewModel>>();
            rootSnapshot.AppendSections(sections);

            _dataSource.ApplySnapshot(rootSnapshot, false);

            // Recents
            var recents = ViewModel.Recents.Select(item => new NSWrapper<SidebarOutlineItemViewModel>(item)).ToArray();
            var recentsSnapshot = new NSDiffableDataSourceSectionSnapshot<NSWrapper<SidebarOutlineItemViewModel>>();
            recentsSnapshot.AppendItems(recents);

            AppendChildren(recents[0], recentsSnapshot);
            _dataSource.ApplySnapshot(recentsSnapshot, recentsSection, false);

            // Tags
            var tags = ViewModel.Items.Select(item => new NSWrapper<SidebarOutlineItemViewModel>(item)).ToArray();
            var tagsSnapshot = new NSDiffableDataSourceSectionSnapshot<NSWrapper<SidebarOutlineItemViewModel>>();
            tagsSnapshot.AppendItems(tags);

            AppendChildren(tags[0], tagsSnapshot);
            _dataSource.ApplySnapshot(tagsSnapshot, tagsSection, false);
        }

        void AppendChildren(NSWrapper<SidebarOutlineItemViewModel> parent, NSDiffableDataSourceSectionSnapshot<NSWrapper<SidebarOutlineItemViewModel>> snapshot)
        {
            var children = parent.Item.Children.Select(item => new NSWrapper<SidebarOutlineItemViewModel>(item)).ToArray();
            snapshot.AppendItems(children, parent);

            foreach (var item in children)
            {
                if (item.Item.HasChildren)
                    AppendChildren(item, snapshot);
            }
        }

        //func updateDataSource(for pet: Pet)
        //{
        //  var snapshot = dataSource.snapshot()
        //  let items = snapshot.itemIdentifiers
        //  let petItem = items.first {
        //    item in
        //    item.pet == pet
        //  }
        //  if let petItem = petItem {
        //   snapshot.reloadItems([petItem])
        //   dataSource.apply(snapshot, animatingDifferences: true, completion: nil)
        //  }
        //}

        //extension PetExplorerViewController: PetDetailViewControllerDelegate {
        //  func petDetailViewController(_ petDetailViewController: PetDetailViewController, didAdoptPet pet: Pet)
        //  {
        //    adoptions.insert(pet)
        //    var adoptedPetsSnapshot = dataSource.snapshot(for: .adoptedPets)
        //    let newItem = Item(pet: pet, title: pet.name)
        //    adoptedPetsSnapshot.append([newItem])
        //    dataSource.apply(adoptedPetsSnapshot, to: .adoptedPets, animatingDifferences: true, completion: nil)
        //    updateDataSource(for: pet)
        //  }
        //}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == nameof(ViewModel.AllItems))
            //    OutlineView.ReloadData();
        }


    }
}

