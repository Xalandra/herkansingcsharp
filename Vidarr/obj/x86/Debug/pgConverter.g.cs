﻿#pragma checksum "D:\Software\GitHub Desktop\herkansingcsharp\Vidarr\pgConverter.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "17BFAEA859CDAE5F6A78E7B1933BC005"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vidarr
{
    partial class pgConverter : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.SuperSmall = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 2:
                {
                    this.Smartphone = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 3:
                {
                    this.Tablet = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 4:
                {
                    this.Desktop = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5:
                {
                    this.FilesInVideosFolderLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.ListConvertedFiles = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 7:
                {
                    this.GoToDownloaderButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 364 "..\..\..\pgConverter.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.GoToDownloaderButton).Click += this.goToDownloaderScreen;
                    #line default
                }
                break;
            case 8:
                {
                    this.DescriptionLabelConverter = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9:
                {
                    this.SelectFileOne = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 242 "..\..\..\pgConverter.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectFileOne).Click += this.selectFile;
                    #line default
                }
                break;
            case 10:
                {
                    this.NameFirstChosenFile = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11:
                {
                    this.SelectFileTwo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 244 "..\..\..\pgConverter.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectFileTwo).Click += this.selectFile;
                    #line default
                }
                break;
            case 12:
                {
                    this.NameSecondChosenFile = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13:
                {
                    this.SelectFileThree = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 246 "..\..\..\pgConverter.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SelectFileThree).Click += this.selectFile;
                    #line default
                }
                break;
            case 14:
                {
                    this.NameThirdChosenFile = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 15:
                {
                    this.ConvertFormatLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 16:
                {
                    this.ComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 17:
                {
                    this.ConvertFilesButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 257 "..\..\..\pgConverter.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.ConvertFilesButton).Click += this.convertSelectedFiles;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

