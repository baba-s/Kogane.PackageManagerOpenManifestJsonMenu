using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEditor.PackageManager.UI.Internal;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Kogane.Internal
{
    internal sealed class PackageManagerOpenManifestJsonMenu
        : VisualElement,
          IPackageManagerExtension
    {
        private bool m_isInitialized;

        VisualElement IPackageManagerExtension.CreateExtensionUI()
        {
            m_isInitialized = false;
            return this;
        }

        void IPackageManagerExtension.OnPackageSelectionChange( PackageInfo packageInfo )
        {
            if ( m_isInitialized ) return;

            VisualElement root = this;

            while ( root != null && root.parent != null )
            {
                root = root.parent;
            }

            var templateContainer     = root.Q<TemplateContainer>();
            var packageManagerToolbar = templateContainer.Q<PackageManagerToolbar>();
            var menuDropdownItem      = packageManagerToolbar.toolbarSettingsMenu.AddBuiltInDropdownItem();

            menuDropdownItem.text = "Open manifest.json";
            menuDropdownItem.action = () =>
            {
                var fullPath = Path.GetFullPath( "./Packages/manifest.json" );
                Process.Start( fullPath );
            };

            m_isInitialized = true;
        }

        void IPackageManagerExtension.OnPackageAddedOrUpdated( PackageInfo packageInfo )
        {
        }

        void IPackageManagerExtension.OnPackageRemoved( PackageInfo packageInfo )
        {
        }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod()
        {
            var extension = new PackageManagerOpenManifestJsonMenu();
            PackageManagerExtensions.RegisterExtension( extension );
        }
    }
}